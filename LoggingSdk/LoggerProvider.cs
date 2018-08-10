using Microsoft.Extensions.Logging;
using System;

namespace Meyer.Logging.Client
{
	public class LoggerProvider : ILoggerProvider
	{
		public ILogger CreateLogger(string categoryName) { return new Logger(categoryName); }

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
