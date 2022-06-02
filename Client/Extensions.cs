using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meyer.Logging.Client
{
    public static class Extensions
    {
        public static ILoggingBuilder AddUniversalLogger(this ILoggingBuilder builder)
        {
            //builder.AddConfiguration();

            builder
                .Services
                .TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, UniversalLoggerProvider>());

            //LoggerProviderOptions.RegisterProviderOptions<Configuration, UniversalLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddUniversalLogger(this ILoggingBuilder builder, Action<Configuration> configure)
        {
            builder.AddUniversalLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
