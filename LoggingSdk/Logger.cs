using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Meyer.Logging.Client
{
	public class Logger : ILogger
	{
		private readonly string _CategoryName;

		public Logger(string categoryName)
		{
			_CategoryName = categoryName;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			throw new NotImplementedException();
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			throw new NotImplementedException();
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			using (var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				Content = new StringContent(JsonConvert.SerializeObject(new
				{
					ApplicationName = "LOGGINGTEST",
					Description = $"This is a {logLevel} test message. {Guid.NewGuid().ToString()}",
					TypeName = logLevel,
					EnvironmentName = "DEVELOPMENT",
				}), Encoding.UTF8, "application/json"),
				RequestUri = new Uri("https://localhost:44371/api/logger"),
			})
			using (var client = new HttpClient())
			{
				var response = Task.Run(() => client.SendAsync(request));

				response.Wait();
			}
		}
	}
}{