using System.Threading.Tasks;
using Windows.Foundation;

namespace StdOttStandard
{
    public class DeferralProvider
    {
        private int count;
        private readonly TaskCompletionSource<bool> tcs;

        public DeferralProvider()
        {
            count = 0;
            tcs = new TaskCompletionSource<bool>();
        }

        public Deferral GetDeferral()
        {
            count++;
            return new Deferral(() =>
            {
                count--;
                if (count == 0) tcs.SetResult(true);
            });
        }

        public async Task Await()
        {
            if (count > 0) await tcs.Task;
            else if (!tcs.Task.IsCompleted) tcs.SetResult(true);
        }
    }
}
