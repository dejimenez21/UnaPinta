using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnaPinta.Data.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public void LogCritical(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(Exception exception)
        {
            _logger.Error(exception, exception.Message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogTrace(string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}
