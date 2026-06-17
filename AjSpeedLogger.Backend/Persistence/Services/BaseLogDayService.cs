namespace Persistence.Services;

public class BaseLogDayService(IRepository _repository, IMapper _mapper) : IBaseLogDayService
{
	public async Task<List<BaseLogDayDto>> GetByAcademicYearIdAsync(int academicYearId, string applicationUserId)
	{
		var baseLogDayEntities = await _repository.GetAsync<BaseLogDay>(bld =>
			bld.AcademicYearId == academicYearId && bld.AcademicYear.ApplicationUserId == applicationUserId);
		var baseLogDayDtos = _mapper.Map<List<BaseLogDayDto>>(baseLogDayEntities);
		return baseLogDayDtos;
	}
	public async Task<bool> UpdateBaseLogDayContentAsync(int baseLogDayId, string newBaseLogDayContent, string applicationUserId)
	{
		var baseLogDayEntity = await _repository.GetSingleWithIncludesAsync<BaseLogDay>(
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
