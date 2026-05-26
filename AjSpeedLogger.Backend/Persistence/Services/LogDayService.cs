using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens.Experimental;
using static System.Net.Mime.MediaTypeNames;

namespace Persistence.Services;

public class LogDayService(IRepository _repository) : ILogDayService
{
	public async Task<bool> SaveLogDayContentAsync(int logId, int baseLogDayId, string content, string applicationUserId)
	{
		// Check if the log belongs to the user
		if (!await _repository.AnyAsync<Log>(log =>
			log.Id == logId && log.ApplicationUserId == applicationUserId))
		{
			return false;
		}

		var logDayEntity = await _repository.GetByIdAsync<LogDay>(logDay => logDay.LogId == logId && logDay.BaseLogDayId == baseLogDayId);

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
			{
				await _repository.DeleteAsync<LogDay>(logDay => logDay.Id == logDayEntity.Id);
			}
			else
			{
				logDayEntity.Content = content;
				await _repository.Update<LogDay>(logDay => logDay.Id == logDayEntity.Id);
			}
		}
		
		await _repository.SaveChangesAsync();
		return true;
	}
}
