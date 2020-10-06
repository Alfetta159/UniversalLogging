using Azure.Storage.Queues;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Meyer.Logging
{
	public static class Extensions
	{
		public static string Base64Encode(this string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			return Convert.ToBase64String(plainTextBytes);
		}

		public static async Task QueueLoggingErrorAsync(this Exception ex)
		{
			const string _ErrorQueueName = "errorqueue";

			var queue = new QueueClient(EnvironmentVariables.AzureWebJobsStorage, _ErrorQueueName);
			var createtask = queue.CreateIfNotExistsAsync();

			var entrybase64 = JsonConvert
				.SerializeObject(ex)
				.Base64Encode();

			await createtask;
			await queue.SendMessageAsync(entrybase64);
		}
	}
}
