﻿// See https://aka.ms/new-console-template for more information
using Meyer.Logging.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var config = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
   .Build();

using IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureLogging(builder =>
        builder
            .ClearProviders()
            .AddUniversalLogger(configuration =>
            {
                // Replace warning value from appsettings.json of "Cyan"
                configuration.LogLevels[LogLevel.Warning] = ConsoleColor.DarkCyan;
                // Replace warning value from appsettings.json of "Red"
                configuration.LogLevels[LogLevel.Error] = ConsoleColor.DarkRed;
            }))
            .Build();

var logger = host
    .Services
    .GetRequiredService<ILogger<Program>>();

logger.LogDebug(1, "Does this line get hit?");    // Not logged
logger.LogInformation(3, "Nothing to see here."); // Logs in ConsoleColor.DarkGreen
logger.LogWarning(5, "Warning... that was odd."); // Logs in ConsoleColor.DarkCyan
logger.LogError(7, "Oops, there was an error.");  // Logs in ConsoleColor.DarkRed
logger.LogTrace(5!, "== 120.");                   // Not logged

await host.RunAsync();