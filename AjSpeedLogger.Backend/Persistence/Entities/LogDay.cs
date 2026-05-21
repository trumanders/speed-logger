namespace Persistence.Entities;

public class LogDay : IEntity
{
	public int Id { get; set; }
	public DateOnly Date { get; set; }
	public int WeekNumber { get; set; }
	public string Content { get; set; } = string.Empty;

	public int? AcademicYearId { get; set; }
	public AcademicYear? AcademicYear { get; set; }
	public int? LogId { get; set; }
}