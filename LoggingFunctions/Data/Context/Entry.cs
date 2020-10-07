using System;
using System.Collections.Generic;

namespace Meyer.Logging.Data.Context
{
    public partial class Entry
    {
        public DateTime Created { get; set; }
        public int ClientApplicationId { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string SeverityName { get; set; }

        public virtual ClientApplication ClientApplication { get; set; }
        public virtual Severity SeverityNameNavigation { get; set; }
    }
}
