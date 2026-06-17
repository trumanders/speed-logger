namespace Persistence.Interfaces;

public interface IBaseLogDayService
{
	public Task<List<BaseLogDayDto>> GetByAcademicYearIdAsync(int academicYearId, string applicationUserId);
	public Task<bool> UpdateBaseLogDayContentAsync(int baseLogDayId, string newBaseLogDayContent, string applicationUserId);
}