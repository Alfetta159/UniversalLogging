using Meyer.Common.EmailApiClient;
using Meyer.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Meyer.Logging.Api;

    public partial class LogEntry
    {
        private readonly ILogger _logger;

        public LogEntry(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogEntry>();
        }

        [Function("LogEntry")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Admin, "get", "post", "delete", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Universal Logging starting a request...");

            try
            {
                switch (req.Method)
                {
                    case "GET":
                        return await GetEntriesAsync(req.GetQueryParameterDictionary());
                    case "POST":
                        await QueueEntryAsync(req);
                        return new CreatedResult(String.Empty, null);
                    case "DELETE":
                        //TODO: determine environment
                        return EnvironmentVariables.Environment == "Production"
                            ? new ForbidResult()
                            : await DeleteEntryAsync(req.GetQueryParameterDictionary());
                    default:
                        return new ObjectResult("Unsupported method") { StatusCode = 405, };
                }
            }
            catch (ArgumentException ex)
            {
                log.LogError(ex, "Universal Logging has had a error.");

                if (ex.HResult == 2)
                    return new BadRequestObjectResult(ex.Message);
                else
                    throw;
            }
            catch (Exception ex)
            {
                log.LogCritical(ex, "Universal Logging has had a critical error.");

                EmailClient.SendHelpDeskEmail(new Common.Storage.Email.CriticalExceptionMessage
                {
                    Application = "UNIVERSALLOGGING",
                    Exception = ex,
                    Subject = "Universal Logging has had a critical error.",
                });

                throw;
            }


            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name ??= data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(responseMessage);
        }
    }
