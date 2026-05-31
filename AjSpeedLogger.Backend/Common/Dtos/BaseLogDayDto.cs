namespace Common.Dtos;

public class BaseLogDayDto
{
	public int Id { get; set; }
	public DateOnly Date { get; set; }
	public int WeekNumber { get; set; }
	public string Content { get; set; }
}
