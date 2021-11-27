using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Data.Brokers.DateTimes
{
    public interface IDateTimeBroker
    {
        DateTime GetCurrentDateTime();
    }
}
