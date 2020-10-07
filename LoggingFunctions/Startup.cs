using Meyer.Logging.Data.Context;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

[assembly: FunctionsStartup(typeof(Meyer.Logging.Startup))]

namespace Meyer.Logging
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			//builder
			//	.Services
			//	.AddScoped<ISendGridClient, SendGridClient>(factory =>
			//	{
			//		return new SendGridClient(apiKey: System.Environment.GetEnvironmentVariable("SmtpApiKey"));
			//	});

			var connectionString = System.Environment.GetEnvironmentVariable("InfrastructureConnection");

			builder
				.Services
				.AddDbContext<InfrastructureContext>(options => options.UseSqlServer( connectionString));
		}
	}
}
