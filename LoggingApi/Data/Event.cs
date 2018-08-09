using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meyer.Logging.Data
{
	public class Event : Entity
	{
		[Required(AllowEmptyStrings = false)]
		public string Description { get; set; }

		[Required]
		public int EventTypeId { get; set; }

		[ForeignKey("EventTypeId")]
		public EventType Type { get; set; }

		[Required]
		public Guid ApplicationId { get; set; }

		[ForeignKey("ApplicationId")]
		public Application Application { get; set; }

		[Required]
		public int EnvironmentId { get; set; }

		[ForeignKey("EnvironmentId")]
		public OperatingEnvironment Environment { get; set; }
	}
}