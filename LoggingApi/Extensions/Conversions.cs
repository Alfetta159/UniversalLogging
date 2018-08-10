using Model = Meyer.Logging.Models;

namespace Meyer.Logging.Extensions
{
	public static class Method
	{
		public static Data.Event ToDataModel(this Model.Event value)
		{
			return new Data.Event
			{
				ApplicationName = value.ApplicationName,
				Description = value.Description,
				EnvironmentName = value.EnvironmentName,
				TypeName = value.TypeName,
			};
		}

		public static Model.Event ToApiModel(this Data.Event value)
		{
			return new Model.Event
			{
				Description = value.Description,
				ApplicationName = value.ApplicationName,
				EnvironmentName = value.EnvironmentName,
				TypeName = value.TypeName,
			};
		}
	}
}
