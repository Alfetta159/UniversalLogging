using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meyer.Logging.Storage
{
    public interface IClient
    {
        Task<IEnumerable<LogItemMetaData>> ListAsync(DateTimeOffset start, DateTimeOffset? end);
        Task<IEnumerable<LogItemMetaData>> ListAsync(int pageNumber, int pageSize = 50);
        Task<LogItem> GetAsync(string id);
        Task<LogItem> PostAsync(object body);
        Task<LogItem> PatchAsync(string id, object body);
        Task DeleteAsync(string id);
    }
}
