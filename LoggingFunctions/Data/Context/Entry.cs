using System;

namespace Meyer.Logging.Data.Context
{
	public partial class Entry
    {
        public string EnvironmentName { get; set; }
        public DateTime Created { get; set; }
        public int ClientApplicationId { get; set; }
        public string Body { get; set; }
        public string SeverityName { get; set; }
        public string UserId { get; set; }

        public virtual ClientApplication ClientApplication { get; set; }
        public virtual Environment Environment { get; set; }
        public virtual Severity Severity { get; set; }
    }
}
