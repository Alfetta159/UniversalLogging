using System;
using System.Collections.Generic;

namespace Meyer.Logging.Data.Context
{
    public partial class User
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string NormalizedEmail { get; set; }
        public string Email { get; set; }
    }
}
