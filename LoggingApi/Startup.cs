using Meyer.Logging.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meyer.Logging
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			var connectionstring = Configuration.GetConnectionString("DefaultConnection");

			services
				.AddDbContext<Context>(options => options.UseSqlServer(connectionstring));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services
				.AddCors(options =>
				{
					options
						.AddPolicy("MeyerEnterprise", builder => builder
							.WithOrigins(GetOrigins())
							.WithMethods("GET", "POST")
							.WithHeaders("x-meyer-logging"));
				});

			services.AddScoped<IRepository, Repository>();
		}

		static string[] GetOrigins()
		{
			return new string[]
			{
				"https://meyerinfrastructureapidev.azurewebsites.net",
				"https://meyerinfrastructureclientmvcdev.azurewebsites.net",
				"https://meyerinfrastructureclientspadev.azurewebsites.net",
				"https://outletstorerp.azurewebsites.net"
			};
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseCors("MeyerEnterprise");
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc();
		}
	}
}
