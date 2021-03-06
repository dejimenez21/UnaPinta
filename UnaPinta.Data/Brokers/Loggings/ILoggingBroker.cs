using System;
using System.Collections.Generic;
using System.Text;

namespace UnaPinta.Data.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogInfo(string message);
        void LogTrace(string message);
        void LogWarn(string message); 
        void LogDebug(string message); 
        void LogError(Exception message);
        void LogCritical(Exception exception);
    }
}
