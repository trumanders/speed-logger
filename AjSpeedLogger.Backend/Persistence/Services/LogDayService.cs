namespace Persistence.Services;

public class LogDayService(IRepository _repository, IMapper _mapper) : ILogDayService
{
	// GET
	// GetAsync returns LIST. Does this work?
	public async Task<LogDayDto?> GetByIdAsync(int id, string applicationUserId)
	{
		var logDayEntity = await _repository.GetAsync<LogDay>(ld => ld.Id == id && ld.Log.ApplicationUserId == applicationUserId);
		return _mapper.Map<LogDayDto>(logDayEntity);
	}


	public async Task<LogDayDto?> GetByBaseLogDayIdAsync(int baseLogDayId, string applicationUserId)
	{
		var logDayEntity = await _repository.GetSingleAsync<LogDay>(ld => ld.BaseLogDayId == baseLogDayId && ld.Log.ApplicationUserId == applicationUserId);
		return _mapper.Map<LogDayDto>(logDayEntity);
	}

	public async Task<List<LogDayDto?>> GetByLogIdAsync(int logId, string applicationUserId)
	{
		var logDayEntity = await _repository.GetAsync<LogDay>(ld => ld.LogId == logId && ld.Log.ApplicationUserId == applicationUserId);
		return _mapper.Map<List<LogDayDto?>>(logDayEntity);
	}

	// CREATE
	public async Task<LogDayDto?> CreateLogDay(LogDayCreateDto logDayCreateDto)
	{
		var logDayEntity = _mapper.Map<LogDay>(logDayCreateDto);
		var newLogDayDto = await _repository.AddAsync<LogDay>(logDayEntity);
		await _repository.SaveChangesAsync();
		return _mapper.Map<LogDayDto>(newLogDayDto);
	}

	// UPDATE
	public async Task<bool> SaveLogDayContentAsync(int logId, int baseLogDayId, string content, string applicationUserId)
	{
		// Check if the log belongs to the user
		if (!await _repository.AnyAsync<Log>(log =>log.Id == logId && log.ApplicationUserId == applicationUserId))
			return false;
		var logDayEntity = await _repository.GetSingleAsync<LogDay>(logDay => logDay.LogId == logId && logDay.BaseLogDayId == baseLogDayId);
		if (logDayEntity == null)
		{
			if (string.IsNullOrWhiteSpace(content))
				return true;
			logDayEntity = new LogDay
			{
				LogId = logId,
				BaseLogDayId = baseLogDayId,
				Content = content
			};
			await _repository.AddAsync(logDayEntity);
		}
		else
		{
			if (string.IsNullOrEmpty(content))
				await _repository.DeleteAsync<LogDay>(logDay => logDay.Id == logDayEntity.Id);
			else
			{
				logDayEntity.Content = content;
				await _repository.Update<LogDay>(logDayEntity.Id, logDayEntity);
			}
		}
		await _repository.SaveChangesAsync();
		return true;
	}

	// DELETE
	public async Task<bool> DeleteAsync(int id, string applicationUserId)
	{
		if (!await _repository.DeleteAsync<LogDay>(ld => ld.Id == id && ld.Log.ApplicationUserId == applicationUserId))
			return false;
		await _repository.SaveChangesAsync();
		return true;
	}
}