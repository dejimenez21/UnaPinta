using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStringSP(this DateTime dateTime)
        {
            string format = "MMMM dd, yyyy";
            return dateTime.ToString(format, new CultureInfo("es-ES"));
        }
    }
}
