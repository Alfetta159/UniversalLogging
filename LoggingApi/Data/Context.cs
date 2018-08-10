using Microsoft.EntityFrameworkCore;

namespace Meyer.Logging.Data
{
	public class Context : DbContext
	{
		public DbSet<OperatingEnvironment> Environments { get; set; }
		public DbSet<Application> Applications { get; set; }
		public DbSet<EventType> EventTypes { get; set; }
		public DbSet<Event> Events { get; set; }

		protected Context(DbContextOptions options) : base(options) { }
		public Context(DbContextOptions<Context> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}
	}
}