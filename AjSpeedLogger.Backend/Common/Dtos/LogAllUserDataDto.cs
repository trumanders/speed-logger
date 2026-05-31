namespace Common.Dtos;

public class LogAllUserDataDto
{
	public int Id { get; set; }
	public DayOfWeek DayOfWeek { get; set; }
	public TimeOnly LessonStartTime { get; set; }
	public TimeOnly LessonEndTime { get; set; }

	public List<LogDayDto> LogDays { get; set; } = [];
}
