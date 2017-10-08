using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backuper
{
    static class Time
    {
        public static String GetYearMonthDayHourMinute()
        {
            return DateTime.Now.ToString("yyMMdd_HHmm");
        }
    }
}
