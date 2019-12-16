using Meyer.Api.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LoggingClient.Services
{
    public class LoggingService : HttpClientService, ILoggingService
    {
        public LoggingService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, string scope, string baseAddress) :
            base(tokenAcquisition, httpClient, scope, baseAddress)
        {
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) { }
    }
}
