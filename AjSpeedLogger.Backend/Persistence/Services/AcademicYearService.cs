using Common.Dtos;

namespace Persistence.Services;

public class AcademicYearService(IRepository _repository, IMapper _mapper) : IAcademicYearService
{
	public async Task<AcademicYearDto> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId)
	{
		var academicYearEntity = await _repository.AddAsync<AcademicYear, AcademicYearCreateDto>(dto);
		academicYearEntity.ApplicationUserId = applicationUserId;
		await _repository.SaveChangesAsync();


		var baseLogDays = new List<LogDay>();
		var date = academicYearEntity.StartDate;

		while(date <= academicYearEntity.EndDate)
		{
			baseLogDays.Add(new LogDay
			{
				Date = date,
				AcademicYearId = academicYearEntity.Id
			});
			date = date.AddDays(1);
		}

		await _repository.AddRangeAsync(baseLogDays);
		await _repository.SaveChangesAsync();

		return _mapper.Map<AcademicYearDto>(academicYearEntity);
	}		
}
