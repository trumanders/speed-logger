using Common.Dtos;

namespace Persistence.Services;

public class AcademicYearService(IRepository _repository) : IAcademicYearService
{
	public async Task<AcademicYear> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId)
	{
		var academicYearEntity = await _repository.AddAsync<AcademicYear, AcademicYearCreateDto>(dto);
		academicYearEntity.ApplicationUserId = applicationUserId;
		await _repository.SaveChangesAsync();


		var baseLogDays = new List<BaseLogDay>();
		var date = academicYearEntity.StartDate;

		while(date <= academicYearEntity.EndDate)
		{
			baseLogDays.Add(new BaseLogDay
			{
				Date = date,
				AcademicYearId = academicYearEntity.Id
			});
			date = date.AddDays(1);
		}

		await _repository.AddRangeAsync(baseLogDays);
		await _repository.SaveChangesAsync();

		return academicYearEntity;
	}		
}
