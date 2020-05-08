using System;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.AsyncResult
{
    public class AsyncResult<TOut>
    {
        private readonly SemaphoreSlim sem;

        public Task<TOut> Task { get; }

        public TOut Result { get; private set; }

        public bool HasResult { get; private set; }

        public Exception Exception { get; private set; }

        public AsyncResult()
        {
            sem = new SemaphoreSlim(0);
            Task = GetValue();
        }

        private async Task<TOut> GetValue()
        {
            await sem.WaitAsync().ConfigureAwait(false);

            if (Exception != null) throw Exception;

            return Result;
        }

        public void SetValue(TOut value)
        {
            Result = value;
            HasResult = true;

            sem.Release();
        }

        public void ThrowException(Exception exception)
        {
            Exception = exception;

            sem.Release();
        }
    }
}
