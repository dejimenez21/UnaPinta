using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Brokers.Loggings;

namespace UnaPinta.Api.HostedServices
{
    public abstract class BaseHostedService<T> : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggingBroker _logger;
        private Timer _timer;

        protected string Name { get; private set; }
        private readonly string _description;
        protected readonly TimeSpan _dueTime;
        protected readonly TimeSpan _period;
        protected readonly bool _isEnabled;
  

        protected BaseHostedService(IConfiguration configuration, ILoggingBroker logger)
        {
            _configuration = configuration;
            _logger = logger;

            Name = GetName();
            _description = GetDescription();
            _dueTime = GetDueTime();
            _period = GetPeriod();
            _isEnabled = IsEnabled();
        }

        public abstract Task DoWork(object state);

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_isEnabled)
            {
                _logger.LogInfo($"El servicio hospedado {Name} está deshabilitado");
                return Task.CompletedTask;
            }

            _logger.LogInfo($"Iniciando el servicio hospedado {Name}");
            _timer = new Timer(BaseDoWork, null, _dueTime, _period);
            return Task.CompletedTask;
        }

        private async void BaseDoWork(object state)
        {
            try
            {
                await DoWork(state);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el servicio hospedado {Name}: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo($"Deteniendo el servicio hospedado {Name}");

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private string GetDescription()
        {
            var description = _configuration[$"AppSettings:HostedServices:{Name}:Description"];
            if (!string.IsNullOrWhiteSpace(description))
            {
                return description;
            }

            _logger.LogWarn($"Advertencia en el servicio hospedado {Name}: el parámetro \"Description\" no se encuentra en el archivo de configuración");
            return string.Empty;
        }

        private string GetName()
        {
            var name = typeof(T).Name;
            if (name.EndsWith("HostedService")) name = name.Substring(0, name.Length - 13);
            return name;
        }

        private TimeSpan GetDueTime()
        {
            if (TimeSpan.TryParse(_configuration[$"AppSettings:HostedServices:{Name}:Delay"], out TimeSpan dueTime))
            {
                return dueTime;
            }

            _logger.LogWarn($"Advertencia en el servicio hospedado {Name}: el parámetro \"Delay\" no se encuentra en el archivo de configuración. Por defecto se iniciará inmediatamente.");
            return TimeSpan.Zero;
        }

        private TimeSpan GetPeriod()
        {
            if (TimeSpan.TryParse(_configuration[$"AppSettings:HostedServices:{Name}:Schedule"], out TimeSpan period))
            {
                return period;
            }

            _logger.LogWarn($"Advertencia en el servicio hospedado {Name}: el parámetro \"Schedule\" no se encuentra en el archivo de configuración. Por defecto se ha colocado 1 día.");
            return TimeSpan.FromDays(1);
        }

        private bool IsEnabled()
        {
            if (bool.TryParse(_configuration[$"AppSettings:HostedServices:{Name}:Enabled"], out bool isEnabled))
            {
                return isEnabled;
            }

            _logger.LogWarn($"Advertencia en el servicio hospedado {Name}: el parámetro \"Enabled\" no se encuentra en el archivo de configuración. Por defecto se ha habilitado.");
            return true;
        }
    }
}
