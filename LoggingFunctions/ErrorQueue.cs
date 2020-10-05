using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meyer.Logging
{
	public class ErrorQueue
	{
		readonly ISendGridClient _SendGrid;

		public ErrorQueue(ISendGridClient sendGrid)
		{
			_SendGrid = sendGrid;
		}

		[FunctionName("ErrorQueue")]
		public async Task RunAsync([QueueTrigger("ErrorQueue", Connection = "StorageConnection")] string item, ILogger log)
		{
			log.LogInformation($"EntryQueue trigger function processed: {item}");

			try
			{
				var getEmailMessage = GetEmailMessage(item);

				await _SendGrid.SendEmailAsync(getEmailMessage);
			}
			catch (Exception ex)
			{
				log.LogCritical(ex, ex.Message);
				throw;
			}
		}

		private SendGridMessage GetEmailMessage(string message)
		{
			return new SendGridMessage()
			{
				From = new EmailAddress
				{
					Name = "Meyer Cloud Services",
					Email = "no-reply_cloud@meyer.com",

				},
				Subject = "[APPDEV] Universal Logging Error",
				PlainTextContent =JObject.Parse (message).ToString(),
				Personalizations = new List<Personalization>
				{
					new Personalization
					{
						 Tos = new List<EmailAddress>
						 {
							  new EmailAddress
							  {
								   Email = System.Environment.GetEnvironmentVariable("ToEmail"),
								   Name = System.Environment.GetEnvironmentVariable("ToName"),
								}
						 }
					}
				}
			};
		}
	}
}