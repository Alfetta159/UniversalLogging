using Meyer.Api;
using Meyer.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using System;

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
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			var connectionstring = Configuration.GetConnectionString("DefaultConnection");

			services
				.AddMicrosoftIdentityWebApiAuthentication(Configuration)
				.EnableTokenAcquisitionToCallDownstreamApi()
				//.AddDownstreamWebApi("WmiApi", Configuration.GetSection("WmiApi"))
				.AddSessionTokenCaches();

			services
				.AddMeyerAuthorizationJwtBearer(Configuration["Identity:NormalizedName"])
				.AddMeyerInfrastructure(connectionstring, Configuration)
				.AddMeyerAuthorizationHandlers()
				.AddMeyerPolicies(Configuration);

			//services
			//    .AddHttpClient<Services.AppriseOdataAuth.IService, Services.AppriseOdataAuth.Service>();

			services
				.AddSwaggerSupport(Configuration.GetSection("Swagger"));

			services
				.AddMeyerCors(Configuration);

			services
				.AddControllers();

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();
			app.UseAuthentication();
			app.UseRouting();
			app.UseCors("Relative");
			app.UseAuthorization();
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (/*environment.IsDevelopment() ||*/ environment.EnvironmentName == "Local")
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}
	}
}
