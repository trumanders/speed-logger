using Persistence.Interfaces;

namespace Persistence.Entities;

public class Log : IEntity
{
	public int Id { get; set; }
	public DayOfWeek DayOfWeek { get; set; }
	public TimeOnly LessonStartTime { get; set; }
	public TimeOnly LessonEndTime { get; set; }

	public ICollection<LogDay> LogDays { get; set; } = [];


	// MANY TO MANY RELATIONSHIP: Student has many AcademicYears. AcademicYear has many Students. Log is the join table.
	public int StudentId { get; set; }
	public int AcademicYearId { get; set; }
}
