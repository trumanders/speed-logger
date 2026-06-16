namespace Common.Dtos;

public class LogDayCreateDto
{
	public int LogId { get; set; }
	public int BaseLogDayId { get; set; }
	public string Content { get; set; } = string.Empty;
}
