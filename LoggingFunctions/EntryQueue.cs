using Meyer.Logging.Data.Context;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Meyer.Logging
{
	public class EntryQueue
	{
		readonly InfrastructureDevContext _Data;

		public EntryQueue(InfrastructureDevContext data) { _Data = data; }

		[FunctionName("EntryQueue")]
		public async System.Threading.Tasks.Task RunAsync([QueueTrigger("entryqueue", Connection = "StorageConnection")] string item, ILogger log)
		{
			log.LogInformation($"EntryQueue trigger function processed: {item}");
			try
			{
				var itemobject = JsonConvert.DeserializeObject<EntryItem>(item);

				var clientapplication = CheckClientApplicationName(itemobject.ClientApplicationName);
				var severity = CheckSeverity(itemobject.Severity);

				await _Data.AddAsync(new Entry
				{
					ClientApplication = clientapplication,
					SeverityName = severity,
					UserId = itemobject.UserId,
					Body = itemobject.Entry.ToString(),
					Created = DateTime.UtcNow,
				}, default);

				await _Data.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				await ex.QueueLoggingErrorAsync();
				throw;
			}
		}

		private ClientApplication CheckClientApplicationName(string clientApplicationName)
		{
			var item = _Data.ClientApplication.SingleOrDefault(ca => ca.DisplayName == clientApplicationName);

			if (item == null)
				return _Data.Add(new ClientApplication
				{
					DisplayName = clientApplicationName,
					IsArchived = false,
					NormalizedName = clientApplicationName.ToUpperInvariant()
				}).Entity;
			else
				return item;
		}

		private string CheckSeverity(string severity)
		{
			var item = _Data.Severity.SingleOrDefault(ca => ca.DisplayName == severity);

			if (item == null)
				return _Data.Add(new Severity
				{
					Description = severity,
					DisplayName = severity,
				}).Entity.DisplayName;
			else
				return item.DisplayName;
		}
	}
}