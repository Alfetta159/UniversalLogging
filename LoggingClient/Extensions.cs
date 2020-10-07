using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.ExceptionServices;

namespace Meyer.Logging.Client
{
	public static class Extensions
	{
		public static ILoggerFactory AddLogger(this ILoggerFactory loggerFactory, LoggerConfiguration config)
		{
			loggerFactory.AddProvider(new Provider(config));

			return loggerFactory;
		}

		public static ILoggerFactory AddLogger(this ILoggerFactory loggerFactory)
		{
			var config = new LoggerConfiguration();

			return loggerFactory.AddLogger(config);
		}

		public static ILoggerFactory AddLogger(this ILoggerFactory loggerFactory, Action<LoggerConfiguration> configure)
		{
			var config = new LoggerConfiguration();
			configure(config);

			return loggerFactory.AddLogger(config);
		}

		public static IServiceCollection AddMeyerLoggingForAzureFunctions(this IServiceCollection services)
		{
			var application = TryGetEnvironmentVariable("LoggingApplication");

			var configuration = new LoggerConfiguration
			{
				ApiKey = TryGetEnvironmentVariable("LoggingApiKey"),
				Application = application,
				BaseAddress = new Uri(TryGetEnvironmentVariable("LoggingBaseAddress")),
				LogLevel = (LogLevel)Int32.Parse(TryGetEnvironmentVariable("LoggingLogLevel")),
			};

			services
				.AddLogging();

			var factory = new LoggerFactory();

			factory
				.AddProvider(new Provider(configuration));

			var logger = factory
				.CreateLogger(application);

			logger.LogInformation("Add Meyer Logging.");

			return services;
		}

		private static string TryGetEnvironmentVariable(string key)
		{
			var result = Environment.GetEnvironmentVariable(key);

			if (String.IsNullOrEmpty("key"))
				throw new InvalidOperationException($"'{key}' application setting must be present. Use empty string if no value is needed.");
			else
				return result;
		}
	}
}