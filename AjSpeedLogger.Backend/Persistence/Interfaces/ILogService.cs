namespace Persistence.Interfaces;

public interface ILogService
{
	public Task<ResultWrapper<LogDto?>> CreateLogAsync(LogCreateDto dto, string applicationUserId);
	public Task<LogDto?> GetByIdAsync(int id, string applicationUserId);
	public Task<bool> DeleteLogAsync(int id, string applicationUserId);
	public Task<bool> HasMultipleLogsAsync(int studentId);
}