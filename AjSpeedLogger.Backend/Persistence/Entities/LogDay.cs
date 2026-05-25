 namespace Persistence.Entities;

public class LogDay : IEntity
{
	public int Id { get; set; }

	public int LogId { get; set; } 
	public Log Log { get; set; } = null!;

	public int BaseLogDayId { get; set; }
	public BaseLogDay BaseLogDay { get; set; } = null!;

	public string Content { get; set; } = string.Empty;
}