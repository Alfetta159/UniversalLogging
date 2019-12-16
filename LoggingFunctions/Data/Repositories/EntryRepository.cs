using Meyer.Logging.Data.Context;
using Meyer.Logging.Data.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Meyer.Logging.Data.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        readonly InfrastructureDevContext _DataContext;

        public EntryRepository(InfrastructureDevContext dataContext) { _DataContext = dataContext; }

        public Entry Add(string clientApplicationName, string environmentName, string type, string userId, string entry)
        {
            var entity = _DataContext
                .Add(CreateEntry(clientApplicationName, environmentName, type, userId, entry));

            return entity.Entity;
        }

        public async Task<Entry> AddAsync(string clientApplicationName, string environmentName, string type, string userId, string entry, CancellationToken cancellationToken)
        {
            var entity = await _DataContext
                .AddAsync(CreateEntry(clientApplicationName, environmentName, type, userId, entry));

            return entity.Entity;
        }

        Entry CreateEntry(string clientApplicationName, string environmentName, string type, string userId, string entry)
        {
            return new Entry
            {
                Body = entry,
                ClientApplication = _DataContext.ClientApplication.Single(ca => ca.NormalizedName == clientApplicationName),
                Created = DateTime.UtcNow,
                Employee = _DataContext.Employee.Single(e => e.UserId == userId),
                EnvironmentNameNavigation = _DataContext.Environment.Single(e => e.DisplayName == environmentName),
                TypeName = type
            };
        }
    }
}