using System;
using System.Collections.Generic;

namespace Meyer.Logging.Data.Context
{
    public partial class ClientApplication
    {
        public ClientApplication()
        {
            Entry = new HashSet<Entry>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string NormalizedName { get; set; }
        public bool IsArchived { get; set; }

        public virtual ICollection<Entry> Entry { get; set; }
    }
}
