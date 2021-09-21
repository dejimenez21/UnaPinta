using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Contracts
{
    public interface ISendNotificationEmailProcessingService
    {
        Task DoWork();
    }
}
