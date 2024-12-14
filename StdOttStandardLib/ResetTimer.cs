using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard
{
    public class ResetTimer
    {
        private CancellationTokenSource cancellationTokenSource;

        public event EventHandler RanDown;

        private TimeSpan Timeout { get; }

        public ResetTimer(TimeSpan timeout)
        {
            Timeout = timeout;
        }

        public static ResetTimer Start(TimeSpan timout)
        {
            ResetTimer timer = new ResetTimer(timout);
            timer.TriggerReset();

            return timer;
        }

        public async Task<bool> Reset()
        {
            this.cancellationTokenSource?.Cancel();
            CancellationTokenSource cancellationTokenSource = this.cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(Timeout, cancellationTokenSource.Token);
            }
            catch
            {
                return false;
            }

            RanDown?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public async void TriggerReset()
        {
            await Reset();
        }

        public void Cancel()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = null;
        }
    }
}
