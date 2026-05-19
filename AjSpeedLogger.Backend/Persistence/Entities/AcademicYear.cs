namespace Persistence.Entities;

public class AcademicYear : IEntity, IUserOwned
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty; // "2026/2027"
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public string ApplicationUserId { get; set; } = string.Empty;


	public ICollection<LogDay> BaseLogDays { get; set; } = [];
}
