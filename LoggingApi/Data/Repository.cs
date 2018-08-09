using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Meyer.Logging.Data
{
	public class Repository : IRepository
	{
		public Context DataContext { get; set; }

		public Repository(Context dataContext) { DataContext = dataContext; }

		public IQueryable<Event> ListEvents() { return DataContext.Events; }

		public virtual async Task<IQueryable<Event>> ListEventsAsync() { return await Task.Run(() => ListEvents()); }

		public async Task<Event> AddAsync(Event entity)
		{
			var entry = await DataContext.AddAsync(entity);

			try
			{
				var result = await DataContext.SaveChangesAsync();

				return entry.Entity;
			}
			catch (DbUpdateException)
			{
				if (entry.State == EntityState.Added)
				{
					DataContext.Remove(entity);
				}

				await DataContext.SaveChangesAsync();

				throw;
			}
		}

	}
}
