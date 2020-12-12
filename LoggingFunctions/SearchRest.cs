using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Meyer.Logging.Data.Context;
using System.Linq;

namespace Meyer.Logging
{
	public class SearchRest
	{
		readonly InfrastructureContext _Data;

		public SearchRest(InfrastructureContext data) { _Data = data; }

		[FunctionName("Search")]
		public async Task<IActionResult> Search(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			string responseMessage = string.IsNullOrEmpty(name)
				? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
				: $"Hello, {name}. This HTTP triggered function executed successfully.";

			return new OkObjectResult(responseMessage);
		}

		[FunctionName("Environments")]
		public async Task<IActionResult> Environments(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			string responseMessage = string.IsNullOrEmpty(name)
				? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
				: $"Hello, {name}. This HTTP triggered function executed successfully.";

			return new OkObjectResult(responseMessage);
		}

		[FunctionName("Severities")]
		public IActionResult Severities(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			//string name = req.Query["name"];

			//string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			//dynamic data = JsonConvert.DeserializeObject(requestBody);
			//name = name ?? data?.name;

			//string responseMessage = string.IsNullOrEmpty(name)
			//	? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
			//	: $"Hello, {name}. This HTTP triggered function executed successfully.";

			var output = _Data.Severities.ToArray();

			return new OkObjectResult(output);
		}

		[FunctionName("Applications")]
		public async Task<IActionResult> Applications(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			string responseMessage = string.IsNullOrEmpty(name)
				? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
				: $"Hello, {name}. This HTTP triggered function executed successfully.";

			return new OkObjectResult(responseMessage);
		}

		[FunctionName("Users")]
		public async Task<IActionResult> Users(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			string responseMessage = string.IsNullOrEmpty(name)
				? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
				: $"Hello, {name}. This HTTP triggered function executed successfully.";

			return new OkObjectResult(responseMessage);
		}
	}
}
