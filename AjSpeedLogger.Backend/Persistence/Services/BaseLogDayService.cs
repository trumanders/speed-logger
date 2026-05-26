namespace Persistence.Services;

public class BaseLogDayService(IRepository _repository) : IBaseLogDayService
{
	public async Task<bool> UpdateBaseLogDayContentAsync(int baseLogDayId, string newBaseLogDayContent, string applicationUserId)
	{
		var baseLogDayEntity = await _repository.GetByIdAsync<BaseLogDay>(
			bld => bld.Id == baseLogDayId && bld.AcademicYear.ApplicationUserId == applicationUserId,
			"AcademicYear"
		);
		if (baseLogDayEntity == null)
			return false;
		baseLogDayEntity.Content = newBaseLogDayContent;
		await _repository.SaveChangesAsync();
		return true;
	}
}
