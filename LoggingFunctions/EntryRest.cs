using Azure.Storage.Queues;
using Meyer.Logging.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Meyer.Logging
{
	public class EntryRest
	{
		const string _EntryQueueName = "entryqueue";
		readonly InfrastructureDevContext _Data;

		public EntryRest(InfrastructureDevContext data) { _Data = data; }


		[FunctionName("logentries")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "delete", Route = null)] HttpRequest req, ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			try
			{
				switch (req.Method)
				{
					case "GET":
						return GetEntries(req.GetQueryParameterDictionary());
					case "POST":
						await QueueEntryAsync(req);
						return new CreatedResult("", null);
					case "DELETE":
						return EnvironmentVariables.Environment == "Production"
							? new ForbidResult()
							: await DeleteEntryAsync(req.GetQueryParameterDictionary());
					default:
						return new ObjectResult("Unsupported method") { StatusCode = 405, };
				}
			}
			catch (Exception ex)
			{
				await ex.QueueLoggingErrorAsync();
				throw;
			}
		}

		private async Task QueueEntryAsync(HttpRequest req)
		{
			var queue = new QueueClient(EnvironmentVariables.AzureWebJobsStorage, _EntryQueueName);
			var createtask = queue.CreateIfNotExistsAsync();
			var parameters = req.GetQueryParameterDictionary();

			var entryitem = new EntryItem
			{
				ClientApplicationName = parameters["clientapplication"],
				Severity = parameters["severity"],
				Entry = JObject.Parse(await new StreamReader(req.Body).ReadToEndAsync()),
				UserId = parameters.ContainsKey("user") ? parameters["user"] : null,
			};

			var entrybase64 = JsonConvert
				.SerializeObject(entryitem)
				.Base64Encode();

			await createtask;
			await queue.SendMessageAsync(entrybase64);
		}

		private IActionResult GetEntries(IDictionary<string, string> parameters)
		{
			return new OkObjectResult(QueryEntries(parameters)
				.Select(p => new Entry
				{
					Body = p.Body,
					ClientApplication = new ClientApplication
					{
						DisplayName = p.ClientApplication.DisplayName,
						Id = p.ClientApplication.Id,
						IsArchived = p.ClientApplication.IsArchived,
						NormalizedName = p.ClientApplication.NormalizedName,
					},
					ClientApplicationId = p.ClientApplicationId,
					Created = p.Created,
					SeverityName = p.SeverityName,
					Severity = new Severity
					{
						Description = p.Severity.Description,
						DisplayName = p.Severity.DisplayName,
					},
					UserId = p.UserId,
				}));
		}

		private IQueryable<Entry> QueryEntries(IDictionary<string, string> parameters)
		{
			return _Data
				.Entry
				.Where(e => !parameters.ContainsKey("clientapplication") || e.ClientApplication.NormalizedName == parameters["clientapplication"]
					&& !parameters.ContainsKey("created") || e.Created == DateTime.Parse(parameters["created"])
					&& !parameters.ContainsKey("severity") || e.SeverityName == parameters["severity"]
					&& !parameters.ContainsKey("userid") || e.UserId == parameters["userid"])
				.Skip(parameters.ContainsKey("skip") ? Int32.Parse(parameters["skip"]) : 0)
				.Take(parameters.ContainsKey("top") ? Int32.Parse(parameters["top"]) : 0);
		}

		private async Task<IActionResult> DeleteEntryAsync(IDictionary<string, string> parameters)
		{
			var entries = QueryEntries(parameters);

			_Data.RemoveRange(entries);
			await _Data.SaveChangesAsync();

			return new NoContentResult();
		}
	}
}
