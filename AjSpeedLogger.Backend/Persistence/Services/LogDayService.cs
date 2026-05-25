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

		// 2.text skickas, raden fanns -> uppdatera raden
		// 4.tom text skickas, raden fanns -> radera raden.


		var logDayEntity = await _repository.GetByIdAsync<LogDay>(l => l.LogId == logId && l.BaseLogDayId == baseLogDayId);

		// LogDay row did not exist
		if (logDayEntity == null)
		{
			if (string.IsNullOrEmpty(content))
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
				_repository.Update<LogDay>();
				await _repository.SaveChangesAsync();
			}
		}



		if (string.IsNullOrEmpty(content))
		{
			// If content is null, delete the LogDay if it exists
			if (logDayEntity != null)
			{
				_repository.Remove(logDayEntity);
				await _repository.SaveChangesAsync();
			}
			return true;
		}

		
		else
		{
			logDayEntity.Content = content;
		}
		
		await _repository.SaveChangesAsync();
		return true;
	}
}
