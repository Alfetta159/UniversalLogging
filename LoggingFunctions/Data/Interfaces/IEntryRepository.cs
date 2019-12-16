using Meyer.Logging.Data.Context;
using System.Threading;
using System.Threading.Tasks;

namespace Meyer.Logging.Data.Interfaces
{
    public interface IEntryRepository
    {
        Entry Add(string clientApplicationName, string environmentName, string type, string userId, string entry);
        Task<Entry> AddAsync(string clientApplicationName, string environmentName, string type, string userId, string entry, CancellationToken cancellationToken);
    }
}
