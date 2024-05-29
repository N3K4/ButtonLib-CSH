using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ButtonLib
{
    class AccurateTimer
    {
        //Used for most accurate time delaying
        public static void Delay(int ms)
        {
            var res1 = (uint)(ms * 10000);
            NtSetTimerResolution(res1, true, ref res1);

            var interval = -ms * 10000L;
            NtDelayExecution(false, ref interval);
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);

        [DllImport("ntdll.dll")]
        private static extern bool NtDelayExecution(bool Alertable, ref long DelayInterval);
    }
}
