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
		const string _UserId = "userid";
		const string _ClientApplication = "clientapplication";
		const string _Code = "code";
		const string _Severity = "severity";
		const string _Created = "created";
		const string _Top = "top";
		const string _Skip = "skip";

		readonly InfrastructureContext _Data;

		public EntryRest(InfrastructureContext data) { _Data = data; }

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
						//TODO:determine environment
						return EnvironmentVariables.Environment == "Production"
							? new ForbidResult()
							: await DeleteEntryAsync(req.GetQueryParameterDictionary());
					default:
						return new ObjectResult("Unsupported method") { StatusCode = 405, };
				}
			}
			catch (ArgumentException ex)
			{
				if (ex.HResult == 2)
					return new BadRequestObjectResult(ex.Message);
				else
					throw;
			}
			catch (Exception ex)
			{
				await ex.QueueLoggingErrorAsync();
				throw;
			}
		}

		private async Task QueueEntryAsync(HttpRequest req)
		{
			var parameters = req.GetQueryParameterDictionary();

			CheckParameters(parameters);

			var queue = new QueueClient(EnvironmentVariables.AzureWebJobsStorage, _EntryQueueName);
			var createtask = queue.CreateIfNotExistsAsync();

			var entryitem = new EntryItem
			{
				ClientApplicationName = parameters[_ClientApplication].ToUpperInvariant(),
				Severity = parameters[_Severity],
				Entry = JObject.Parse(await new StreamReader(req.Body).ReadToEndAsync()),
				UserId = parameters.ContainsKey(_UserId) ? parameters[_UserId] : null,
			};

			var entrybase64 = JsonConvert
				.SerializeObject(entryitem)
				.Base64Encode();

			await createtask;
			await queue.SendMessageAsync(entrybase64);
		}

		static void CheckParameters(IDictionary<string, string> dictionaries)
		{
			var acceptedparameternames = new string[] { _UserId, _Severity, _ClientApplication, _Code };

			foreach (var key in dictionaries.Keys)
			{
				if (!acceptedparameternames.Contains(key))
				{
					throw new ArgumentException($"Acceptable parameters are: '{_UserId}','{_Severity}', & '{_ClientApplication}'. The parameter names are case-sensitive.")
					{
						HResult = 2,
					};
				}
			}

			if (!dictionaries.Keys.Contains(_Severity) || !dictionaries.Keys.Contains(_ClientApplication))
			{
				throw new ArgumentException($"Parameters must include: '{_Severity}', & '{_ClientApplication}'. The parameter names are case-sensitive.")
				{
					HResult = 2,
				};
			}
		}

		private IActionResult GetEntries(IDictionary<string, string> parameters)
		{
			var count = parameters.Count;
			var skip = IntOrNegativeOne(parameters, "skip");
			var take = IntOrNegativeOne(parameters, "take");

			var check = count switch
			{
				2 => _Data
					.Entries
					.Skip(skip)
					.Take(take)
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
						UserId = p.UserId,
					})
					.ToArray(),
				_ => throw new NotImplementedException(),
			};

			return new OkObjectResult(check);
		}

		private int IntOrNegativeOne(IDictionary<string, string> parameters, string key)
		{
			if (String.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Parameter cannot be null or whitespace.", nameof(key));
			else
			{
				var value = parameters[key];

				if (String.IsNullOrWhiteSpace(value))
					return -1;
				else
				{
					return Int32.Parse(value);
				}
			}
		}

		private IQueryable<Entry> QueryEntries(IDictionary<string, string> parameters)
		{
			return _Data
				.Entries
				.Where(e => !parameters.ContainsKey(_ClientApplication) || e.ClientApplication.NormalizedName == parameters[_ClientApplication]
					&& !parameters.ContainsKey(_Created) || e.Created == DateTime.Parse(parameters[_Created])
					&& !parameters.ContainsKey(_Severity) || e.SeverityName == parameters[_Severity]
					&& !parameters.ContainsKey(_UserId) || e.UserId == parameters[_UserId])
				.Skip(parameters.ContainsKey(_Skip) ? Int32.Parse(parameters[_Skip]) : 0)
				.Take(parameters.ContainsKey(_Top) ? Int32.Parse(parameters[_Top]) : 0);
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
