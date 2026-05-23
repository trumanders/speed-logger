namespace Persistence.Interfaces;

public interface ILogService
{
	public Task<LogDto?> CreateLogAsync(LogCreateDto dto, string applicationUserId);
	public Task<LogDto?> GetByIdAsync(int id, string applicationUserId);
}