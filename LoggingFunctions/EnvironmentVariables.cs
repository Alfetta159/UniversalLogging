namespace Meyer.Logging
{
	public static class EnvironmentVariables
	{
		public static string AzureWebJobsStorage { get => System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"); }

		public static string Environment { get => System.Environment.GetEnvironmentVariable("Environment"); }
	}
}
