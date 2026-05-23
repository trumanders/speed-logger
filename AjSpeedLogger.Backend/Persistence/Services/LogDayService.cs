namespace Persistence.Services;

public class LogDayService(IRepository _repository)
{
	public async Task<bool> UpdateLogDayContentAsync(int id, string content, string applicationUserId)
	{
		var logDay = await _repository.GetByIdAsync<LogDay>(logDay => 
			logDay.Id == id &&
			((logDay.Log != null &&
			logDay.Log.ApplicationUserId == applicationUserId) ||
			logDay.BaseLog != null &&
			logDay.BaseLog.AcademicYear.ApplicationUserId == applicationUserId));
		
		if (logDay == null)
			return false;
		
		logDay.Content = content;
		await _repository.SaveChangesAsync();
		return true;
	}
}
