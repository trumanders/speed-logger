namespace Persistence.Interfaces;

public interface ILogDayService
{
	public Task<bool> UpdateLogDayContentAsync(int id, string content, string applicationUserId);
}
