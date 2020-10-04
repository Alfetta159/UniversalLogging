using Meyer.Logging.Data.Context;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Meyer.Logging.Startup))]

namespace Meyer.Logging
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var connectionString = System.Environment.GetEnvironmentVariable("InfrastructureConnection");

			builder
				.Services
				.AddDbContext<InfrastructureDevContext>(options => options.UseSqlServer(connectionString));
		}
	}
}
