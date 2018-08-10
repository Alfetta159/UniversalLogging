using Meyer.Logging.Extensions;
using Meyer.Logging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meyer.Logging.Pages
{
	public class IndexModel : PageModel
	{
		private readonly Data.IRepository _Repository;

		public IndexModel(Data.IRepository repository) { _Repository = repository; }

		[BindProperty]
		public IEnumerable<Event> Events { get; set; }

		public async Task OnGet()
		{
			Events = (await _Repository.ListEventsAsync())
				.Take(25)
				.Select(e => e.ToApiModel());
		}
	}
}
