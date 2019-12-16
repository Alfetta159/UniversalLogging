using System;
using System.Collections.Generic;

namespace Meyer.Logging.Data.Context
{
    public partial class Employee
    {
        public Employee()
        {
            Entry = new HashSet<Entry>();
            InverseManager = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public Guid? TransactionId { get; set; }
        public bool IsArchived { get; set; }
        public string UserId { get; set; }
        public string UltiProNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid? LegacyGuid { get; set; }
        public bool CarAllowance { get; set; }
        public string CostCenterNumber { get; set; }
        public int? ManagerId { get; set; }
        public string Extension { get; set; }
        public string CompanyNumber { get; set; }
        public int? LodgingId { get; set; }
        public int? MileageId { get; set; }
        public int? TravelExpenseId { get; set; }
        public int? VendorExpenseId { get; set; }

        public virtual Employee Manager { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Entry> Entry { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
    }
}
