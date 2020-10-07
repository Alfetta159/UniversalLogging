using Microsoft.Extensions.Logging;
using Microsoft.Rest.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace Meyer.Logging.Client
{
	public class Logger : ILogger
	{
		private readonly LoggerConfiguration _Configuration;

		public Logger(LoggerConfiguration config)
		{
			_Configuration = config;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel == _Configuration.LogLevel;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			if (_Configuration.EventId == 0 || _Configuration.EventId == eventId.Id)
			{
				using var client = GetClient();
				var fullmessage = formatter.Invoke(state, exception);
				var content = SetupContent(fullmessage, exception);
				var user = GetUser(fullmessage);
				var code = GetCode();

				try
				{
					var result = client
						.PostAsync($"api/logentries?severity={logLevel}&clientapplication={_Configuration.Application}{code}{user}", content)
						.GetAwaiter()
						.GetResult();
				}
				catch (HttpRequestException)
				{ //TODO: Send an email, but not too many emails.
					throw;
				}
			}
		}

		private string GetCode()
		{
			return String.IsNullOrWhiteSpace(_Configuration.ApiKey)
				? String.Empty
				: $"&code={_Configuration.ApiKey}";
		}

		private object GetUser(string something)
		{
			var regex = new Regex("(?<=\\[User: )(.*?)(?=\\])");

			var user = regex.Match(something).Value;

			return String.IsNullOrWhiteSpace(user)
				? String.Empty
				: $"&user={user}";
		}

		HttpClient GetClient()
		{
			var client = new HttpClient
			{
				BaseAddress = _Configuration.BaseAddress,
			};

			var defaultRequestHeaders = client.DefaultRequestHeaders;

			if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			}

			return client;
		}

		public static HttpContent SetupContent(object item, Exception exception)
		{
			var content = SafeJsonConvert.SerializeObject(new
			{
				Item = item,
				Exception = exception
			});

			return new StringContent(content, Encoding.UTF8, "application/json");
		}
	}
}
