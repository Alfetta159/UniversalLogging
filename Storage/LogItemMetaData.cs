using System;

namespace Meyer.Logging.Storage
{
    public class LogItemMetaData
    {
        public string? Id { get; set; }
        public string? Environment { get; set; }
        public string? Application { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? UserId { get; set; }
    }
}