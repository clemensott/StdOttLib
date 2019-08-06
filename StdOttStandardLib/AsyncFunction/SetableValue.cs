using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard
{
    public class SetableValue<T>
    {
        private readonly SemaphoreSlim sem;
        private T result;

        public Task<T> Task { get; }

        public T Result => Task.Result;

        public SetableValue()
        {
            sem = new SemaphoreSlim(0);
            Task = GetValue();
        }

        private async Task<T> GetValue()
        {
            await sem.WaitAsync();

            return result;
        }

        public void SetValue(T value)
        {
            result = value;

            sem.Release();
        }
    }
}
