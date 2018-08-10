using System.ComponentModel.DataAnnotations;

namespace Meyer.Logging.Data
{
	public class LogLevel
	{
		[Required(AllowEmptyStrings = false)]
		public string DisplayName { get; set; }

		[Key]
		public string Name { get; set; }
	}
}