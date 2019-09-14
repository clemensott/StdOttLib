using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.AsyncResult
{
    public class AsyncResultS<TInOut>
    {
        private readonly SemaphoreSlim sem;
        private TInOut result;

        public Task<TInOut> Task { get; }

        public TInOut Input { get; }

        public TInOut Result => Task.Result;

        public AsyncResultS(TInOut input)
        {
            Input = input;

            sem = new SemaphoreSlim(0);
            Task = GetValue();
        }

        private async Task<TInOut> GetValue()
        {
            await sem.WaitAsync();

            return result;
        }

        public void SetValue(TInOut value)
        {
            result = value;

            sem.Release();
        }
    }
}
