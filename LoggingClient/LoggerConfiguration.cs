using Microsoft.Extensions.Logging;
using System;

namespace Meyer.Logging.Client
{
	public class LoggerConfiguration
	{
		public LogLevel LogLevel { get; set; }
		public int EventId { get; set; } = 0;
		public Uri BaseAddress { get; set; }
		public string ApiKey { get; set; }
		public string Application { get; set; }
	}
}