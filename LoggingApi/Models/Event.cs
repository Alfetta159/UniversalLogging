using System;
using System.ComponentModel.DataAnnotations;

namespace Meyer.Logging.Models
{
	public class Event
	{
		[Required(AllowEmptyStrings = false)]
		public string Description { get; set; }
		[Required(AllowEmptyStrings = false)]
		public string EnvironmentName { get; set; }
		[Required(AllowEmptyStrings = false)]
		public string ApplicationName { get; set; }
		[Required(AllowEmptyStrings = false)]
		public string TypeName { get; set; }
		[Required]
		public DateTime TimeStamp { get; set; } = DateTime.Now;
	}
}
