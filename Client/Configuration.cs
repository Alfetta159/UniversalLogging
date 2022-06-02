using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Meyer.Logging.Client
{
    public class Configuration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new Dictionary<LogLevel, ConsoleColor>()
        {
            [LogLevel.Information] = ConsoleColor.Green
        };
    }
}