using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Meyer.Logging.Client
{
	public class Provider : ILoggerProvider
	{
		private bool disposedValue;
		private readonly LoggerConfiguration _config;
		private readonly ConcurrentDictionary<string, Logger> _loggers = new ConcurrentDictionary<string, Logger>();

		public Provider(LoggerConfiguration config)
		{
			_config = config;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return _loggers.GetOrAdd(categoryName, name => new Logger(_config));
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_loggers.Clear();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~Provider()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
