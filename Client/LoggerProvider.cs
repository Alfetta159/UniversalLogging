using System;
using System.Collections.Concurrent;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Meyer.Logging.Client
{
    //[UnsupportedOSPlatform("browser")]
    [ProviderAlias("Universal")]
    public sealed class LoggerProvider : ILoggerProvider
    {
        private readonly IDisposable OnChangeToken;
        private Configuration CurrentConfig;
        private readonly ConcurrentDictionary<string, Logger> _loggers = new ConcurrentDictionary<string, Logger>(StringComparer.OrdinalIgnoreCase);

        public LoggerProvider(
            IOptionsMonitor<Configuration> config)
        {
            CurrentConfig = config.CurrentValue;
            OnChangeToken = config.OnChange(updatedConfig => CurrentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new Logger(name, GetCurrentConfig));

        private Configuration GetCurrentConfig() => CurrentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            OnChangeToken.Dispose();
        }
    }
}