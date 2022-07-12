using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace Meyer.Logging.Client
{
    public static class Extensions
    {
        public static ILoggingBuilder AddUniversalLogger(this ILoggingBuilder builder, Action<Configuration> configure)
        {
            builder
                .AddUniversalLogger();

            builder
                .Services
                .Configure(configure);

            return builder;
        }

        public static ILoggingBuilder AddUniversalLogger(this ILoggingBuilder builder)
        {
            builder
                .AddConfiguration();

            builder
                .Services
                .TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());

            LoggerProviderOptions
                .RegisterProviderOptions<Configuration, LoggerProvider>(builder.Services);

            return builder;
        }
    }
}