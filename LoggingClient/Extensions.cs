using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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
	}
}
