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
        private readonly IDisposable _onChangeToken;
        private Configuration _currentConfig;
        private readonly ConcurrentDictionary<string, Logger> _loggers = new ConcurrentDictionary<string, Logger>(StringComparer.OrdinalIgnoreCase);

        public LoggerProvider(
            IOptionsMonitor<Configuration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new Logger(name, GetCurrentConfig));

        private Configuration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}