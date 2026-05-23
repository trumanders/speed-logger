   namespace Persistence.Entities;

public class Log : IEntity, IUserOwned
{
	public int Id { get; set; }
	public DayOfWeek DayOfWeek { get; set; }
	public TimeOnly LessonStartTime { get; set; }
	public TimeOnly LessonEndTime { get; set; }

	public string ApplicationUserId { get; set; } = string.Empty;
	public ICollection<LogDay> LogDays { get; set; } = [];


	// MANY TO MANY RELATIONSHIP: Student has many AcademicYears. AcademicYear has many Students. Log is the join table.
	public int StudentId { get; set; }
	public int AcademicYearId { get; set; }
	public Student Student { get; set; } = null!;
	public AcademicYear AcademicYear { get; set; } = null!;
}
