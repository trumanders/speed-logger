namespace Common.Dtos;

public class LogDayDto
{
	public int Id { get; set; }
	public int LogId { get; set; }
	public int BaseLogDayId { get; set; }
	public string Content { get; set; } = string.Empty;
}
