using Meyer.Logging.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(LoggingFunctionsDemo.Startup))]

namespace LoggingFunctionsDemo
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			builder
				.Services
				.AddMeyerLoggingForAzureFunctions();
		}
	}
}
