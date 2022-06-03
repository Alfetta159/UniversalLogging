using Microsoft.Extensions.Logging;
using System;

namespace Meyer.Logging.Client
{
    public sealed class Logger : ILogger
    {
        readonly string Name;
        readonly Func<Configuration> GetCurrentConfig;

        public Logger(string name, Func<Configuration> getCurrentConfig) => (Name, GetCurrentConfig) = (name, getCurrentConfig);

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => GetCurrentConfig().LogLevels.ContainsKey(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var config = GetCurrentConfig();

            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                ConsoleColor originalColor = Console.ForegroundColor;

                Console.ForegroundColor = config.LogLevels[logLevel];
                Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

                Console.ForegroundColor = originalColor;
                Console.Write($"     {Name} - ");

                Console.ForegroundColor = config.LogLevels[logLevel];
                Console.Write($"{formatter(state, exception)}");

                Console.ForegroundColor = originalColor;
                Console.WriteLine();
            }
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}