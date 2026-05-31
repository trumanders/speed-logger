namespace Persistence.Interfaces;

public interface IContext
{
	public DbSet<AcademicYear> AcademicYears { get; }
	public DbSet<Log> Logs { get; }
	public DbSet<LogDay> LogDays { get; }
	public DbSet<Student> Students { get; }
	public DbSet<BaseLogDay> BaseLogDays { get;}

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
