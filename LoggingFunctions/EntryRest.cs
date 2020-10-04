using Azure.Storage.Queues;
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
using System.Text;
using System.Threading.Tasks;

namespace Meyer.Logging
{
	public class EntryRest
	{
		[FunctionName("logentry")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "delete", Route = null)] HttpRequest req, ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			//var connectionstring = Environment.GetEnvironmentVariable("StorageConnection");
			//var queue = new QueueClient(connectionstring, "EntryQueue");

			//await queue.CreateAsync();
			//await queue.SendMessageAsync(item);


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
						return DeleteEntry(req.GetQueryParameterDictionary());
					default:
						return new ObjectResult("Unsupported method") { StatusCode = 405, };
				}

				string name = req.Query["name"];

				//dynamic data = JsonConvert.DeserializeObject(requestBody);

				//name = name ?? data?.name;

				//return name != null
				//	? (ActionResult)new OkObjectResult($"Hello, {name}")
				//	: new BadRequestObjectResult("Please pass a name on the query string or in the request body");
				//throw new NotImplementedException();
			}
			catch (Exception ex) { throw; }
		}

		private async Task QueueEntryAsync(HttpRequest req)
		{
			var connectionstring = Environment.GetEnvironmentVariable("StorageConnection");
			var queue = new QueueClient(connectionstring, "entryqueue");
			var createtask = queue.CreateIfNotExistsAsync();

			var parameters = req.GetQueryParameterDictionary();
			var entryitem = new EntryItem
			{
				ClientApplicationName = parameters["clientapplication"],
				EnvironmentName = parameters["environment"],
				Severity = parameters["severity"],
				UserId = parameters["userid"],
				Entry = JObject.Parse(await new StreamReader(req.Body).ReadToEndAsync()),
			};

			var entryjson = JsonConvert.SerializeObject(entryitem);
			var entrybase64 = Base64Encode(entryjson);

			await createtask;
			await queue.SendMessageAsync(entrybase64);

		}
		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

		private IActionResult GetEntries(IDictionary<string, string> getQueryParameterDictionary)
		{
			throw new NotImplementedException();
		}

		private IActionResult DeleteEntry(IDictionary<string, string> getQueryParameterDictionary)
		{
			throw new NotImplementedException();
		}

		async Task<IActionResult> CreateEntryAsync(IDictionary<string, string> getQueryParameterDictionary, string requestBody)
		{
			var clientapplicationkey = getQueryParameterDictionary.Keys.SingleOrDefault(k => k.Equals("clientapplication", StringComparison.InvariantCultureIgnoreCase));
			var clientapplication = clientapplicationkey == null ? "clientapplication" : getQueryParameterDictionary[clientapplicationkey];
			var environmentkey = getQueryParameterDictionary.Keys.SingleOrDefault(k => k.Equals("environment", StringComparison.InvariantCultureIgnoreCase));
			var environment = environmentkey == null ? "development" : getQueryParameterDictionary[environmentkey];
			var useridvalue = getQueryParameterDictionary.Keys.SingleOrDefault(k => k.Equals("userid", StringComparison.InvariantCultureIgnoreCase));

			if (String.IsNullOrWhiteSpace(useridvalue)) throw new ArgumentException("User ID is required.");

			var userid = getQueryParameterDictionary[useridvalue];

			var typevalue = getQueryParameterDictionary.Keys.SingleOrDefault(k => k.Equals("type", StringComparison.InvariantCultureIgnoreCase));
			var type = typevalue == null ? "information" : getQueryParameterDictionary[typevalue];

			//return new OkObjectResult(await _EntryRepository.AddAsync(clientapplication, environment, type, userid, requestBody, default));


			throw new NotImplementedException();
			//_logEntryRepository.Add(environment, type, userid, requestBody);
		}
	}
}
