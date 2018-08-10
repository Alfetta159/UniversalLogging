using System.Linq;
using System.Threading.Tasks;

namespace Meyer.Logging.Data
{
	public interface IRepository
	{
		IQueryable<Event> ListEvents();

		Task<IQueryable<Event>> ListEventsAsync();

		IQueryable<Application> ListApplications();

		Task<IQueryable<Application>> ListApplicationsAsync();

		IQueryable<LogLevel> ListEventTypes();

		Task<IQueryable<LogLevel>> ListEventTypesAsync();

		IQueryable<OperatingEnvironment> ListEnvironments();

		Task<IQueryable<OperatingEnvironment>> ListEnvironmentAsync();


		Task<Event> AddAsync(Event entity);

		Task<Application> AddAsync(Application entity);

		Task<LogLevel> AddAsync(LogLevel entity);

		Task<OperatingEnvironment> AddAsync(OperatingEnvironment entity);
	}
}
