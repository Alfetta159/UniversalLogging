using Meyer.Logging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoggingApi.Pages
{
	public class TestModel : PageModel
	{
		//public Task<IActionResult>  OnGet() { }

		[BindProperty]
		public LogOptions Options { get; set; }

		public async Task<IActionResult> OnPost()
		{
			try
			{
				if (Options.ForceInfo) { await LogAsync("INFO"); }
				if (Options.ForceWarning) { await LogAsync("WARNING"); }
				if (Options.ForceError) { await LogAsync("ERROR"); }
				if (Options.ForceCritical) { await LogAsync("CRITICAL"); }

				return Page();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				return StatusCode(500);
			}
		}

		async Task LogAsync(string type)
		{
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				Content = new StringContent(JsonConvert.SerializeObject(new Event
				{
					ApplicationName = "LOGGINGTEST",
					Description = $"This is a {type} test message. {Guid.NewGuid().ToString()}",
					TypeName = type,
					EnvironmentName = "DEVELOPMENT",
				}), Encoding.UTF8, "application/json"),
				RequestUri = new Uri("https://localhost:44371/api/logger"),
			};

			var client = new HttpClient();

			var response = await client.SendAsync(request);
		}
	}
}