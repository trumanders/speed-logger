namespace Persistence.Services;

public class LogDayService(IRepository _repository) : ILogDayService
{
	public async Task<bool> SaveLogDayContentAsync(int logId, int baseLogDayId, string content, string applicationUserId)
	{
		if (!await _repository.AnyAsync<Log>(log =>
			log.Id == logId && log.ApplicationUserId == applicationUserId))
		{
			return false;
		}

		var logDayEntity = await _repository.GetByIdAsync<LogDay>(l => l.LogId == logId && l.BaseLogDayId == baseLogDayId);

		// Create new log if it doesn't exist
		if (logDayEntity == null)
		{
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
			logDayEntity.Content = content;
		}
		
		
		await _repository.SaveChangesAsync();
		return true;
	}
}
