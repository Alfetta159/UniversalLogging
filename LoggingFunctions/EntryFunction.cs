using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Meyer.Logging.Data.Interfaces;

namespace Meyer.Logging
{
    public class EntryFunction
    {
        readonly IEntryRepository _EntryRepository;

        public EntryFunction(IEntryRepository entryFunction) { _EntryRepository = entryFunction; }

        [FunctionName("logentry")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (req.Method)
            {
                case "GET":
                    return GetEntries(req.GetQueryParameterDictionary());
                case "POST":
                    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                    return await CreateEntryAsync(req.GetQueryParameterDictionary(), requestBody);
                case "DELETE":
                    return DeleteEntry(req.GetQueryParameterDictionary());
                default:
                    return new ObjectResult("Unsupported method")
                    {
                        StatusCode = 405,
                    };
            }

            //string name = req.Query["name"];

            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            //name = name ?? data?.name;

            //return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
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

            return new OkObjectResult(await _EntryRepository.AddAsync(clientapplication, environment, type, userid, requestBody, default));


            //throw new NotImplementedException();
            //_logEntryRepository.Add(environment, type, userid, requestBody);
        }
    }
}
