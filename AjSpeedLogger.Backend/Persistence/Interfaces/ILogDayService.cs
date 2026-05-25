namespace Persistence.Interfaces;

public interface ILogDayService
{
	public Task<bool> SaveLogDayContentAsync(int logId, int baseLogDayId, string content, string applicationUserId);
}
