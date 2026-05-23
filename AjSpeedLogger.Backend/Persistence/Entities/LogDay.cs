 namespace Persistence.Entities;

public class LogDay : IEntity
{
	public int Id { get; set; }
	public DateOnly Date { get; set; }
	public int WeekNumber { get; set; }
	public string Content { get; set; } = string.Empty;

	public int? LogId { get; set; } // Null for LogDays asciociated with BaseLog
	public Log? Log { get; set; } = null!;

	public int? BaseLogId { get; set; } // Null for LogDays that aren't BaseLogs
	public BaseLog? BaseLog { get; set; } = null!;
}