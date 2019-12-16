using Meyer.Logging.Data.Context;
using Meyer.Logging.Data.Interfaces;
using Meyer.Logging.Data.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

            builder
                .Services
                .AddScoped<IEntryRepository, EntryRepository>();
        }
    }
}
