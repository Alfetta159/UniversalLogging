using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meyer.Logging.Data
{
	public class Repository : IRepository
	{
		public Context DataContext { get; set; }

		public Repository(Context dataContext) { DataContext = dataContext; }


		public async Task<Event> AddAsync(Event entity)
		{
			if (!(await ListApplicationsAsync()).Any(a => a.Name == entity.ApplicationName))
			{
				await AddAsync(new Application
				{
					DisplayName = entity.ApplicationName,
					Name = entity.ApplicationName,
				});
			}

			if (!(await ListEventTypesAsync()).Any(a => a.Name == entity.TypeName))
			{
				await AddAsync(new LogLevel
				{
					DisplayName = entity.TypeName,
					Name = entity.TypeName,
				});
			}

			if (!(await ListEnvironmentAsync()).Any(a => a.Name == entity.EnvironmentName))
			{
				await AddAsync(new OperatingEnvironment
				{
					DisplayName = entity.EnvironmentName,
					Name = entity.EnvironmentName,
				});
			}

			return await AddAsync<Event>(entity);
		}

		public IQueryable<Event> ListEvents() { return DataContext.Events; }

		public virtual Task<IQueryable<Event>> ListEventsAsync() { return Task.Run(() => ListEvents()); }


		public Task<Application> AddAsync(Application entity) { return AddAsync<Application>(entity); }

		public IQueryable<Application> ListApplications() { return DataContext.Applications; }

		public Task<IQueryable<Application>> ListApplicationsAsync() { return Task.Run(() => ListApplications()); }


		public Task<LogLevel> AddAsync(LogLevel entity) { return AddAsync<LogLevel>(entity); }

		public IQueryable<LogLevel> ListEventTypes() { return DataContext.EventTypes; }

		public Task<IQueryable<LogLevel>> ListEventTypesAsync() { return Task.Run(() => ListEventTypes()); }



		public Task<OperatingEnvironment> AddAsync(OperatingEnvironment entity) { return AddAsync<OperatingEnvironment>(entity); }

		public IQueryable<OperatingEnvironment> ListEnvironments() { return DataContext.Environments; }

		public Task<IQueryable<OperatingEnvironment>> ListEnvironmentAsync() { return Task.Run(() => ListEnvironments()); }


		async Task<T> AddAsync<T>(T entity) where T : class
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
