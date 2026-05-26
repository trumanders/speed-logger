namespace Persistence.Entities;

public class BaseLogDay : IEntity
{
	public int Id { get; set; }

	public int AcademicYearId { get; set; }
	public AcademicYear AcademicYear { get; set; } = null!;

	public DateOnly Date { get; set; }
	public int WeekNumber { get; set; }
	public string Content { get; set; } = string.Empty;

	public ICollection<LogDay> LogDays { get; set; } = new List<LogDay>();
}