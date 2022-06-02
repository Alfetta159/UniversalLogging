using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meyer.Logging.Client
{
    [ProviderAlias("Universal")]
    public class UniversalLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
