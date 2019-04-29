using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard
{
    public class SetableValue<T>
    {
        private readonly object lockObj;
        private T result;

        public Task<T> Task { get; }

        public T Result { get { return Task.Result; } }

        public SetableValue()
        {
            lockObj = new object();
            Task = GetValue();
        }

        private async Task<T> GetValue()
        {
            await Utils.WaitAsync(lockObj);

            return result;
        }

        public void SetValue(T value)
        {
            result = value;

            lock (lockObj) Monitor.Pulse(lockObj);
        }
    }
}
