using System;
using System.ComponentModel.DataAnnotations;

namespace Meyer.Logging.Data
{
	public class Application
	{
		[Key]
		public Guid Id { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string DisplayName { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string NormalizedName { get; set; }
	}
}