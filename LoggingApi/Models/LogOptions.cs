using System.ComponentModel.DataAnnotations;

namespace Meyer.Logging.Models
{
	public class LogOptions
    {
		[Display(Name ="Force Informational Event")]
		public bool ForceInfo { get; set; }

		[Display(Name = "Force Warning Event")]
		public bool ForceWarning { get; set; }

		[Display(Name = "Force Error Event")]
		public bool ForceError { get; set; }

		[Display(Name = "Force Critical Event")]
		public bool ForceCritical { get; set; }
	}
}
