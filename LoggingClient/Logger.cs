using Microsoft.Extensions.Logging;
using System;

namespace LoggingClient
{
    public class Logger<T> : ILogger<T>
    {        /// <summary>
             /// Constructor.
             /// <para>CAUTION: You never create a logger directly. This is a responsibility of the logging framework by calling the provider's CreateLogger().</para>
             /// </summary>
        public Logger(Provider Provider, string Category)
        {
            this.Provider = Provider;
            this.Category = Category;
        }

        /// <summary>
        /// The logger provider who created this instance
        /// </summary>
        public Provider Provider { get; private set; }

        /// <summary>
        /// The category this instance serves.
        /// <para>The category is usually the fully qualified class name of a class asking for a logger, e.g. MyNamespace.MyClass </para>
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Begins a logical operation scope. Returns an IDisposable that ends the logical operation scope on dispose.
        /// </summary>
        public IDisposable BeginScope<TState>(TState state)
        {
            return Provider.ScopeProvider.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
