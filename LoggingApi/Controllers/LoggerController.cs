using Meyer.Api.Models;
using Meyer.Logging.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Meyer.Logging.Controllers
{
	[Produces("application/json")]
	[EnableCors("MeyerEnterprise")]
	[Route("api/Logger")]
	public class LoggerController : Controller
	{
		readonly Data.IRepository _Repository;
		readonly ILogger<LoggerController> _Logger;

		public LoggerController(Data.IRepository repository, ILogger<LoggerController> logger)
		{
			_Repository = repository;
			_Logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetEventsAsync([FromQuery]string environment = "all",
			[FromQuery]string type = "all",
			[FromQuery]string application = "all",
			[FromQuery]int skip = 0,
			[FromQuery]int top = 25,
			[FromQuery]string sortBy = "TimeStamp desc")
		{
			try
			{
				var counttask = await _Repository.ListEventsAsync();
				var count = counttask.Count();
				var list = await _Repository.ListEventsAsync();

				return Ok(new ItemsWithCount<Models.Event>
				{
					ItemCount = count,
					Items = list
						.OrderByDescending(e => e.TimeStamp)
						.Skip(skip)
						.Take(top == 0 ? count : top)
						.Select(c => c.ToApiModel())
						.ToArray(),
				});
			}
			catch (Exception ex)
			{
				_Logger.LogCritical(ex.Message, ex.StackTrace, ex.ToString());
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error.");
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostEventAsync([FromBody] Models.Event eventInfo)
		{
			if (!ModelState.IsValid)
			{
				_Logger.LogError("Universal logging is not working.", ModelState);
				return BadRequest(ModelState);
			}

			try
			{
				var check = await _Repository.AddAsync(eventInfo.ToDataModel());

				if (check != null)
					return CreatedAtAction("Event", new { TimeStamp = eventInfo.TimeStamp.ToString() }, eventInfo);
				else
				{
					_Logger.LogCritical("Universal logging is not working.");
					return StatusCode(500, "Unexpected error trying to create new address.");
				}
			}
			catch (DbException dbex)
			{
				_Logger.LogError("Universal logging is not working.", dbex);
				return BadRequest(dbex.InnerException.Message);
			}
			catch (Exception ex)
			{
				_Logger.LogCritical("Universal logging is not working.", ex.StackTrace, ex);
				return StatusCode(500);
			}
		}
	}
}