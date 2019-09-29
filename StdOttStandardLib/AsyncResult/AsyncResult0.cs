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

        public AsyncResult()
        {
            sem = new SemaphoreSlim(0);
            Task = GetValue();
        }

        private async Task<TOut> GetValue()
        {
            await sem.WaitAsync();

            return Result;
        }

        public void SetValue(TOut value)
        {
            Result = value;
            HasResult = true;

            sem.Release();
        }
    }
}
