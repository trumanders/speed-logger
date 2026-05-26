namespace Persistence.Services;

public class AcademicYearService(IRepository _repository, IMapper _mapper) : IAcademicYearService
{
	public async Task<IEnumerable<AcademicYearNameDto>> GetAllAsync(string applicationUserId)
	{
		var academicYears = await _repository.GetAllAsync<AcademicYear>(ay => ay.ApplicationUserId == applicationUserId);
		return _mapper.Map<IEnumerable<AcademicYearNameDto>>(academicYears);
	}

	public async Task<AcademicYearDto?> GetByIdAsync(int id, string applicationUserId)
	{
		var academicYearEntity = await _repository.GetByIdAsync<AcademicYear>(ay => ay.Id == id && ay.ApplicationUserId == applicationUserId);
		return _mapper.Map<AcademicYearDto>(academicYearEntity);
	}

	public async Task<IEnumerable<AcademicYearNameDto>> GetByStudentIdAsync(int studentId, string applicationUserId)
	{
		var academicYearEntities = await _repository.GetAllAsync<AcademicYear>(ay => ay.Logs.Any(log => log.StudentId == studentId) && ay.ApplicationUserId == applicationUserId);
		return _mapper.Map<IEnumerable<AcademicYearNameDto>>(academicYearEntities);
	}

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

	public async Task<bool> DeleteAsync(int id, string applicationUserId)
	{
		if (!await _repository.DeleteAsync<AcademicYear>(ay => ay.Id == id && ay.ApplicationUserId == applicationUserId))
			return false;
		await _repository.SaveChangesAsync();

		// Logs will cascade delete.
		// Remove any Students that don't have any Logs after the AcademicYear is deleted.
		await _repository.DeleteAsync<Student>(s => s.ApplicationUserId == applicationUserId && !s.Logs.Any());
		await _repository.SaveChangesAsync();
		return true;
	}
}
