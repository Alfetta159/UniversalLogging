using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meyer.Logging.Storage
{
    public class Client : IClient
    {
        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<LogItem> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LogItemMetaData>> ListAsync(DateTimeOffset start, DateTimeOffset? end)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LogItemMetaData>> ListAsync(int pageNumber, int pageSize = 50)
        {
            throw new NotImplementedException();
        }

        public Task<LogItem> PatchAsync(string id, object body)
        {
            throw new NotImplementedException();
        }

        public Task<LogItem> PostAsync(object body)
        {
            throw new NotImplementedException();
        }
    }
}
