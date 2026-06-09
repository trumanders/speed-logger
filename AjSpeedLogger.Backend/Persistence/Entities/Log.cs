   namespace Persistence.Entities; 


public class Log : IEntity 
{
	public int Id { get; set; }

	public string ApplicationUserId { get; set; } = string.Empty;
	public int StudentId { get; set; }
	public Student Student { get; set; } = null!;

	public int AcademicYearId { get; set; }
	public AcademicYear AcademicYear { get; set; } = null!;

	public int DayOfWeek { get; set; }
	public TimeOnly LessonStartTime { get; set; }
	public TimeOnly LessonEndTime { get; set; }

	public ICollection<LogDay> LogDays { get; set; } = [];	
}
