using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard
{
    public class ResetTimer
    {
        private readonly SemaphoreSlim sem;

        public event EventHandler RanDown;

        public long LastCount { get; private set; }

        private TimeSpan Timeout { get; }

        private ResetTimer(TimeSpan timeout)
        {
            sem = new SemaphoreSlim(0);

            LastCount = 0;
            Timeout = timeout;
        }

        public static ResetTimer Start(TimeSpan timout)
        {
            ResetTimer timer = new ResetTimer(timout);
            timer.Reset();

            return timer;
        }

        public async Task<bool> Reset()
        {
            long thisCount = ++LastCount;
            await Task.Delay(Timeout);

            if (thisCount != LastCount) return false;

            RanDown?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }
}
