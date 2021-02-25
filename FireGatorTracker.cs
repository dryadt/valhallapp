using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace valhallappweb.Handler
{
    public class FireGatorTracker
    {
        public event Func<Task> OnThursday;

        private CancellationTokenSource cancellation;
        private bool isThursday;

        public void Start()
        {
            Cancel();
            cancellation = new CancellationTokenSource();
            isThursday = ((DateTime.Now.DayOfWeek == DayOfWeek.Thursday) && (DateTime.Now.Hour == 12));
            _ = CheckForTheHolidays(cancellation.Token);
        }

        public void Cancel()
        {
            cancellation?.Cancel();
        }

        private async Task CheckForTheHolidays(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (isThursday)
                {
                    isThursday = ((DateTime.Now.DayOfWeek == DayOfWeek.Thursday) && (DateTime.Now.Hour == 12));
                }
                else if ((DateTime.Now.DayOfWeek == DayOfWeek.Thursday)&&(DateTime.Now.Hour==12))
                {
                    isThursday = true;
                    if (OnThursday != null)
                        await OnThursday();
                }

                await Task.Delay(10000, token);
            }
        }
    }
}
