using System;

namespace Meyer.Logging.Models
{
	public class Event
	{
		public string Description { get; set; }
		public string EnvironmentName { get; set; }
		public string ApplicationName { get; set; }
		public string TypeName { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
