using System;
using System.Collections.Generic;

namespace Meyer.Logging.Data.Context
{
    public partial class Environment
    {
        public Environment()
        {
            Entry = new HashSet<Entry>();
        }

        public string DisplayName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Entry> Entry { get; set; }
    }
}
