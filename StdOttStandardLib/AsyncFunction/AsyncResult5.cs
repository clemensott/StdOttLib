using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard
{
    public class AsyncResult<TOut, TIn0, TIn1, TIn2, TIn3, TIn4>
    {
        private readonly SemaphoreSlim sem;
        private TOut result;

        public Task<TOut> Task { get; }

        public TIn0 Input0 { get; }

        public TIn1 Input1 { get; }

        public TIn2 Input2 { get; }

        public TIn3 Input3 { get; }

        public TIn4 Input4 { get; }

        public TOut Result => Task.Result;

        public AsyncResult(TIn0 input0, TIn1 input1, TIn2 input2, TIn3 input3, TIn4 input4)
        {
            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
            Input3 = input3;
            Input4 = input4;

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
