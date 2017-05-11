using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Converter
{
   public class DateTimeConverter
    {
        public static DateTime DateTimeOffsetAndTimeSetToDateTime(DateTimeOffset date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }
}
