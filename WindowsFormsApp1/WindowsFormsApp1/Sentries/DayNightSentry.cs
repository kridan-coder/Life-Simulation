using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DayNightSentry
    {
        public bool Day = true;
        public int DayOrNightLastsFor = 0;
        public int UntilDayOrNight;

        public TimesOfDay TimeOfDay;

        MainSentry mainSentry;
        public void NextTick()
        {
            if (DayOrNightLastsFor >= UntilDayOrNight)
            {
                DayOrNightLastsFor = 0;
                Day = !Day;
            }
            else
            {
                DayOrNightLastsFor++;
            }
        }

        public DayNightSentry(int untilDayOrNight, MainSentry mainSentry)
        {
            UntilDayOrNight = untilDayOrNight;
            this.mainSentry = mainSentry;
        }

    }
}
