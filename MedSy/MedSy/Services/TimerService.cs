using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace MedSy.Services
{
    public class TimerService 
    {
        private Timer timer;
        private int interval;
        public delegate void TimerEventHandler();
        public event TimerEventHandler OnTimerElapsed;
        public TimerService()
        {
            int dueTime = CalculatedueTime();
            interval = 3600000;
            timer = new Timer(TimerCallback, null, dueTime, interval);
        }
        private void TimerCallback(object state)
        {
            OnTimerElapsed?.Invoke();
        }
        public void Dispose()
        {
            timer.Dispose();
        }
        private int CalculatedueTime()
        {
            int result = 0;
            DateTime now = DateTime.Now;
            int minuteValue = now.Minute;
            int minuteStartValue = 45;
            if (minuteValue > minuteStartValue)
            {
                result = 60 - minuteValue + minuteStartValue;
            }
            else
            {
                result = minuteStartValue - minuteValue;
            }

            return result*60*1000;
        }
    }
}
