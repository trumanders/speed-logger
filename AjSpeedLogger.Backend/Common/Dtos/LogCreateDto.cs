namespace Common.Dtos;

public class LogCreateDto
{
	public DayOfWeek DayOfWeek { get; set; }
	public TimeOnly LessonStartTime { get; set; }
	public TimeOnly LessonEndTime { get; set; }
	public int AcademicYearId { get; set; }
	public int StudentId { get; set; }
}
