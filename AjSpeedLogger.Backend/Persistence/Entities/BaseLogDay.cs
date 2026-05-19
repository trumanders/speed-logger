namespace Persistence.Entities;

public class BaseLogDay : IEntity
{
	public int Id { get; set; }
	public DateOnly Date { get; set; }
	public string Content { get; set; } = string.Empty;

	public int AcademicYearId { get; set; }
}
