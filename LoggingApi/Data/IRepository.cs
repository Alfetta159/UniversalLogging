using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meyer.Logging.Data
{
	public interface IRepository
	{
		IQueryable<Event> ListEvents();

		Task<IQueryable<Event>> ListEventsAsync();

		Task<Event> AddAsync(Event entity);
	}
}
