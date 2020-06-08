using System;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.Dispatch
{
    class DispatchHandler : IDisposable
    {
        private readonly SemaphoreSlim runTriggerSem, resultTriggerSem;
        private DispatcherHandle currentHandle;

        private DispatchHandler()
        {
            runTriggerSem = new SemaphoreSlim(0);
            resultTriggerSem = new SemaphoreSlim(0);
        }

        public static DispatchHandler Create()
        {
            DispatchHandler handler = new DispatchHandler();
            handler.Start();
            return handler;
        }

        private async void Start()
        {
            while (true)
            {
                await runTriggerSem.WaitAsync();

                if (currentHandle == null)
                {
                    runTriggerSem.Dispose();
                    resultTriggerSem.Dispose();
                    return;
                }

                currentHandle.Run();
                resultTriggerSem.Release();
            }
        }

        public async Task Run(DispatcherHandle handle)
        {
            try
            {
                currentHandle = handle;
                runTriggerSem.Release();
                await resultTriggerSem.WaitAsync().ConfigureAwait(false);

                if (currentHandle.Exception != null) throw currentHandle.Exception;
            }
            finally
            {
                currentHandle = null;
            }
        }

        public void Dispose()
        {
            runTriggerSem.Release();
        }
    }
}
