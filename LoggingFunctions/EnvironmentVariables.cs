using System;
using System.Collections.Generic;
using System.Text;

namespace Meyer.Logging
{
	public static class EnvironmentVariables
	{
		public static string StorageAccountString { get => System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"); }

		public static string Environment { get => System.Environment.GetEnvironmentVariable("Environment"); }
	}
}
