using Meyer.Logging.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

[assembly: FunctionsStartup(typeof(LoggingFunctionsDemo.Startup))]

namespace LoggingFunctionsDemo
{
	public class Startup : FunctionsStartup
	{
		private ILoggerFactory _loggerFactory;

		public override void Configure(IFunctionsHostBuilder builder)
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			builder.Services.AddLogging();

			_loggerFactory = new LoggerFactory();
			_loggerFactory.AddProvider(new Provider(new LoggerConfiguration
			{
				ApiKey = string.Empty,
				Application = "LOGGINGFUNCTIONSDEMO",
				BaseAddress = new Uri("http://localhost:7071"),
				//EventId = "",
				LogLevel = LogLevel.Information
			}));

			var logger = _loggerFactory.CreateLogger("Startup");
			logger.LogInformation("Got Here in Startup");
			//Do something with builder    
		}
	}
}
