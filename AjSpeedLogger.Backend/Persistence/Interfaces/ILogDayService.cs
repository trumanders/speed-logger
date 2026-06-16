namespace Persistence.Interfaces;

public interface ILogDayService
{
	public Task<LogDayDto?> GetByIdAsync(int id, string applicationUserId);
	public Task<List<LogDayDto?>> GetByLogIdAsync(int logId, string applicationUserId);

	public Task<LogDayDto?> GetByBaseLogDayIdAsync(int baseLogDayId, string applicationUserId);
	public Task<LogDayDto?> CreateLogDay(LogDayCreateDto logDayCreateDto);
	public Task<bool> SaveLogDayContentAsync(int logId, int baseLogDayId, string content, string applicationUserId);
	public Task<bool> DeleteAsync(int logId, string applicationUserId);
}
