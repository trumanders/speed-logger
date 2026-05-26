namespace Persistence.Interfaces;

public interface IBaseLogDayService
{
	public Task<bool> UpdateBaseLogDayContentAsync(int baseLogDayId, string newBaseLogDayContent, string applicationUserId);
}