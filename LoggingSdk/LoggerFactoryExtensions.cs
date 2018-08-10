﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingSdk
{
	public static class LoggerFactoryExtensions
	{
		/// <summary>
		/// Adds a file logger named 'File' to the factory.
		/// </summary>
		/// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
		public static ILoggingBuilder AddMeyerLogger(this ILoggingBuilder builder)
		{
			builder.Services.AddSingleton<ILoggerProvider, LoggerProvider>();
			return builder;
		}

		/// <summary>
		/// Adds a file logger named 'File' to the factory.
		/// </summary>
		/// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
		/// <param name="configure">Configure an instance of the <see cref="FileLoggerOptions" /> to set logging options</param>
		public static ILoggingBuilder AddMeyerLogger(this ILoggingBuilder builder, Action<LoggerProvider> configure)
		{
			if (configure == null)
			{
				throw new ArgumentNullException(nameof(configure));
			}
			builder.AddMeyerLogger();
			builder.Services.Configure(configure);

			return builder;
		}
	}
}