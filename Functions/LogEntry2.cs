using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meyer.Logging.Api;

partial class LogEntry
{
    async Task<IActionResult> GetEntriesAsync(IDictionary<string, string> parameters)
    {
        var count = parameters.Count;
        var skip = IntOrNegativeOne(parameters, "skip");
        var take = IntOrNegativeOne(parameters, "take");

        await Task.CompletedTask;

        throw new NotImplementedException();

        //var check = count switch
        //{
        //	2 => _Data
        //		.Entries
        //		.Skip(skip)
        //		.Take(take)
        //		.Select(p => new Entry
        //		{
        //			Body = p.Body,
        //			ClientApplication = new ClientApplication
        //			{
        //				DisplayName = p.ClientApplication.DisplayName,
        //				Id = p.ClientApplication.Id,
        //				IsArchived = p.ClientApplication.IsArchived,
        //				NormalizedName = p.ClientApplication.NormalizedName,
        //			},
        //			ClientApplicationId = p.ClientApplicationId,
        //			Created = p.Created,
        //			SeverityName = p.SeverityName,
        //			UserId = p.UserId,
        //		})
        //		.ToArray(),
        //	_ => throw new NotImplementedException(),
        //};

        //return new OkObjectResult(check);
    }

    async Task QueueEntryAsync(HttpRequest req)
    {
        await Task.CompletedTask;

        throw new NotImplementedException();

        //var parameters = req.GetQueryParameterDictionary();

        //CheckParameters(parameters);

        //var queue = new QueueClient(EnvironmentVariables.AzureWebJobsStorage, _EntryQueueName);
        //var createtask = queue.CreateIfNotExistsAsync();

        //var entryitem = new EntryItem
        //{
        //	ClientApplicationName = parameters[_ClientApplication].ToUpperInvariant(),
        //	Severity = parameters[_Severity],
        //	Entry = JObject.Parse(await new StreamReader(req.Body).ReadToEndAsync()),
        //	UserId = parameters.ContainsKey(_UserId) ? parameters[_UserId] : null,
        //};

        //var entrybase64 = JsonConvert
        //	.SerializeObject(entryitem)
        //	.Base64Encode();

        //await createtask;
        //await queue.SendMessageAsync(entrybase64);
    }

    static void CheckParameters(IDictionary<string, string> dictionaries)
    {
        throw new NotImplementedException();
        //var acceptedparameternames = new string[] { _UserId, _Severity, _ClientApplication, _Code };

        //foreach (var key in dictionaries.Keys)
        //{
        //	if (!acceptedparameternames.Contains(key))
        //	{
        //		throw new ArgumentException($"Acceptable parameters are: '{_UserId}','{_Severity}', & '{_ClientApplication}'. The parameter names are case-sensitive.")
        //		{
        //			HResult = 2,
        //		};
        //	}
        //}

        //if (!dictionaries.Keys.Contains(_Severity) || !dictionaries.Keys.Contains(_ClientApplication))
        //{
        //	throw new ArgumentException($"Parameters must include: '{_Severity}', & '{_ClientApplication}'. The parameter names are case-sensitive.")
        //	{
        //		HResult = 2,
        //	};
        //}
    }

    static int IntOrNegativeOne(IDictionary<string, string> parameters, string key)
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

    async Task<IActionResult> DeleteEntryAsync(IDictionary<string, string> parameters)
    {
        await Task.CompletedTask;

        throw new NotImplementedException();
        //var entries = QueryEntries(parameters);

        //_Data.RemoveRange(entries);
        //await _Data.SaveChangesAsync();

        //return new NoContentResult();
    }
}