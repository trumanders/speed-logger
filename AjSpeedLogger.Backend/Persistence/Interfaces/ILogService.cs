using Common.Dtos;

namespace Persistence.Interfaces
{
	public interface ILogService
	{
		public Task<LogDto> CreateLogAsync(LogCreateDto dto, string applicationUserId);
	}
}