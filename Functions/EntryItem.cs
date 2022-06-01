using Newtonsoft.Json.Linq;

namespace Meyer.Logging
{
	public class EntryItem
	{
		public string ClientApplicationName { get; set; }
		public string Severity { get; set; }
		public string UserId { get; set; }
		public JObject Entry { get; set; }
	}
}