using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard
{
    public class AsyncFunction<TOut, TIn0, TIn1>
    {
        private readonly SemaphoreSlim sem;
        private TOut result;

        public Task<TOut> Task { get; }

        public TIn0 Input0 { get; }

        public TIn1 Input1 { get; }

        public TOut Result => Task.Result;

        public AsyncFunction(TIn0 input0, TIn1 input1)
        {
            Input0 = input0;
            Input1 = input1;

            sem = new SemaphoreSlim(0);
            Task = GetValue();
        }

        private async Task<TOut> GetValue()
        {
            await sem.WaitAsync();

            return result;
        }

        public void SetValue(TOut value)
        {
            result = value;

            sem.Release();
        }
    }
}
