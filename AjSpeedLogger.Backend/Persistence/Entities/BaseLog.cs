namespace Persistence.Entities; 

public class BaseLog : IEntity
{
	public int Id { get; set; }
	
	public int AcademicYearId { get; set; }
	public AcademicYear AcademicYear { get; set; } = null!;
	public ICollection<LogDay> LogDays { get; set; } = [];
}
