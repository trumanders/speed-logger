namespace Persistence.Services;

public class LogService(IRepository _repository, IMapper _mapper) : ILogService
{

	public async Task<LogDto?> GetByIdAsync(int id, string applicationUserId)
	{		
		var logEntity = await _repository.GetByIdAsync<Log>(log => log.Id == id && log.ApplicationUserId == applicationUserId);
		return logEntity == null ? null : _mapper.Map<LogDto>(logEntity);
	}

	public async Task<LogDto?> CreateLogAsync(LogCreateDto createDto, string applicationUserId)
	{
		// Check if student already has a log for the same academic year (not allowed)
		if (await _repository.IsCountGreaterThan<Log>(l => 
			l.StudentId == createDto.StudentId && l.AcademicYearId == createDto.AcademicYearId, 0))
		{
			throw new InvalidOperationException("Eleven har redan en log registrerad på samma läsår.");
		}

		var logEntity = await _repository.AddAsync<Log>(_mapper.Map<Log>(createDto));
		logEntity.ApplicationUserId = applicationUserId;
		await _repository.SaveChangesAsync();
		return _mapper.Map<LogDto>(logEntity);
	}

	public async Task<bool> DeleteLogAsync(int id, string applicationUserId)
	{
		if (!await _repository.DeleteAsync<Log>(log => log.Id == id && log.ApplicationUserId == applicationUserId))
			return false;
		await _repository.SaveChangesAsync();

		// Delete Students that no longer have any logs
		await _repository.DeleteAsync<Student>(s => s.ApplicationUserId == applicationUserId && !s.Logs.Any());
		await _repository.SaveChangesAsync();
		return true;
	}

	public async Task<bool> HasMultipleLogsAsync(int studentId)
	{
		return await _repository.IsCountGreaterThan<Log>(l => l.StudentId == studentId, 1);
	}
}