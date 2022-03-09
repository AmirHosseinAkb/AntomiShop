using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Extensions
{
    public static class DateConvertor
    {
        public static string ConvertToShamsi(this DateTime value)
        {
            System.Globalization.PersianCalendar persian = new System.Globalization.PersianCalendar();
            return persian.GetYear(value).ToString() + "/" + persian.GetMonth(value).ToString("00")+"/"+persian.GetDayOfMonth(value).ToString("00");
        }
    }
}
