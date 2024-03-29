//using Meyer.Logging.Data.Context;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Meyer.Logging
//{
//	public class EntryQueue
//	{
//		readonly InfrastructureContext _Data;

//		public EntryQueue(InfrastructureContext data) { _Data = data; }

//		[FunctionName("EntryQueue")]
//		public async Task RunAsync([QueueTrigger("entryqueue", Connection = "AzureWebJobsStorage")] string item, ILogger log)
//		{
//			log.LogInformation($"EntryQueue trigger function processed: {item}");
//			try
//			{
//				var itemobject = JsonConvert.DeserializeObject<EntryItem>(item);

//				var clientapplication = CheckClientApplicationName(itemobject.ClientApplicationName);
//				var severity = CheckSeverity(itemobject.Severity);

//				await _Data.AddAsync(new Entry
//				{
//					ClientApplication = clientapplication,
//					SeverityName = severity,
//					UserId = itemobject.UserId,
//					Body = itemobject.Entry.ToString(),
//					Created = DateTime.UtcNow,
//				}, default);

//				await _Data.SaveChangesAsync();
//			}
//			catch (Exception ex)
//			{
//				await ex.QueueLoggingErrorAsync();
//				throw;
//			}
//		}

//		private ClientApplication CheckClientApplicationName(string clientApplicationName)
//		{
//			var item = _Data.ClientApplications.SingleOrDefault(ca => ca.NormalizedName == clientApplicationName);

//			if (item == null)
//				return _Data.Add(new ClientApplication
//				{
//					DisplayName = clientApplicationName,
//					IsArchived = false,
//					NormalizedName = clientApplicationName.ToUpperInvariant()
//				}).Entity;
//			else
//				return item;
//		}

//		private string CheckSeverity(string severity)
//		{
//			var item = _Data.Severities.SingleOrDefault(ca => ca.DisplayName == severity);

//			if (item == null)
//				return _Data.Add(new Severity
//				{
//					Description = severity,
//					DisplayName = severity,
//				}).Entity.DisplayName;
//			else
//				return item.DisplayName;
//		}
//	}
//}