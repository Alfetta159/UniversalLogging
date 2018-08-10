using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meyer.Logging.Data
{
	public class Event
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string Description { get; set; }

		[Required]
		public string TypeName { get; set; }

		[ForeignKey("TypeName")]
		public EventType Type { get; set; }

		[Required]
		public string ApplicationName { get; set; }

		[ForeignKey("ApplicationName")]
		public Application Application { get; set; }

		[Required]
		public string EnvironmentName { get; set; }

		[ForeignKey("EnvironmentName")]
		public OperatingEnvironment Environment { get; set; }

		[Required]
		public DateTime TimeStamp { get; set; } = DateTime.Now;
	}
}