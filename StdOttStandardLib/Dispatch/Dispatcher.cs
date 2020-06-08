using System;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.Dispatch
{
    public class Dispatcher : IDisposable
    {
        private readonly SemaphoreSlim runQueueSem;
        private DispatchHandler handler;

        public Dispatcher()
        {
            runQueueSem = new SemaphoreSlim(0);
        }

        public static Dispatcher Create()
        {
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.Start();
            return dispatcher;
        }

        public void Start()
        {
            DispatchHandler oldHandler = handler;
            handler = DispatchHandler.Create();

            if (oldHandler == null) runQueueSem.Release();
            else oldHandler.Dispose();
        }

        public async Task Stop()
        {
            DispatchHandler oldHandler = handler;
            if (oldHandler != null) await runQueueSem.WaitAsync();

            handler = null;
            oldHandler?.Dispose();
        }

        public async Task Run(Action action)
        {
            DispatcherHandle handle = new DispatcherHandle(action);
            await Run(handle);
        }

        public async Task<TResult> Run<TResult>(Func<TResult> func)
        {
            DispatcherHandle handle = new DispatcherHandle(() => func());
            await Run(handle);
            return (TResult)handle.Result;
        }

        public async Task<TResult> Run<TResult>(Func<Task<TResult>> func)
        {
            DispatcherHandle handle = new DispatcherHandle(() => func());
            await Run(handle);
            return await (Task<TResult>)handle.Result;
        }

        private async Task Run(DispatcherHandle handle)
        {
            await runQueueSem.WaitAsync().ConfigureAwait(false);
            try
            {
                await handler.Run(handle);
            }
            finally
            {
                runQueueSem.Release();
            }
        }

        public void Dispose()
        {
            runQueueSem.Dispose();
        }
    }
}
