using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Contexts
{
	public class Context : IdentityDbContext<ApplicationUser>
	{
		public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
		public DbSet<BaseLogDay> BaseLogDays => Set<BaseLogDay>();
		public DbSet<Log> Logs => Set<Log>();
		public DbSet<LogDay> LogDays => Set<LogDay>();
		public DbSet<Student> Students => Set<Student>();

		public Context(DbContextOptions<Context> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
