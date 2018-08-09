using System.ComponentModel.DataAnnotations;

namespace Meyer.Logging.Data
{
	public class EventType: Entity
	{
		[Required(AllowEmptyStrings = false)]
		public string DisplayName { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string NormalizedName { get; set; }
	}
}