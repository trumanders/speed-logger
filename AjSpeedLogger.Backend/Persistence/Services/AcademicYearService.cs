using System.Globalization;

namespace Persistence.Services;

public class AcademicYearService(IRepository _repository, IMapper _mapper) : IAcademicYearService
{
	public async Task<AcademicYearDto> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId)
	{
		var academicYearEntity = await _repository.AddAsync<AcademicYear>(_mapper.Map<AcademicYear>(dto));
		academicYearEntity.ApplicationUserId = applicationUserId;

		// Create all the BaseLogDays for the year
		var date = academicYearEntity.StartDate;
		academicYearEntity.BaseLogDays = new List<BaseLogDay>();

		while (date <= academicYearEntity.EndDate)
		{
			academicYearEntity.BaseLogDays.Add(new BaseLogDay
			{
				Date = date,
				WeekNumber = ISOWeek.GetWeekOfYear(date)
			});

			date = date.AddDays(1);
		}

		await _repository.SaveChangesAsync();

		return _mapper.Map<AcademicYearDto>(academicYearEntity);
	}	

	public async Task<AcademicYearDto?> GetByIdAsync(int id, string applicationUserId)
	{
		var academicYearEntity = await _repository.GetByIdAsync<AcademicYear>(ay => ay.Id == id && ay.ApplicationUserId == applicationUserId);

		return _mapper.Map<AcademicYearDto>(academicYearEntity);
	}
}
