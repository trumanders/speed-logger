namespace Common.Dtos;

public class LogAllUserDataDto
{
	public int Id { get; set; }
	public int DayOfWeek { get; set; }
	public TimeOnly LessonStartTime { get; set; }
	public TimeOnly LessonEndTime { get; set; }

	public List<LogDayDto> LogDays { get; set; } = [];
	public int AcademicYearId { get; set; }
}
