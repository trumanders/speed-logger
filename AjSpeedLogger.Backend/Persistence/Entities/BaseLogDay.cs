namespace Persistence.Entities;

public class BaseLogDay : IEntity, IAcademicYearOwned
{
	public int Id { get; set; }
	public DateOnly Date { get; set; }
	public string Content { get; set; } = string.Empty;

	public int AcademicYearId { get; set; }
}
