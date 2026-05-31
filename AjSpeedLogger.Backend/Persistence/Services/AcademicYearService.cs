using System.Windows.Markup;

namespace Persistence.Services;

public class AcademicYearService(IRepository _repository, IMapper _mapper, IContext _db) : IAcademicYearService
{
	// GET
	public async Task<IEnumerable<AcademicYearNameDto>> GetAllNamesAsync(string applicationUserId)
	{
		var academicYears = await _repository.GetAsync<AcademicYear>(ay => ay.ApplicationUserId == applicationUserId);
		return _mapper.Map<IEnumerable<AcademicYearNameDto>>(academicYears);
	}

	public async Task<AcademicYearDto?> GetByIdAsync(int id, string applicationUserId)
	{
		var academicYearEntity = await _repository.GetAsync<AcademicYear>(ay => ay.Id == id && ay.ApplicationUserId == applicationUserId);
		return _mapper.Map<AcademicYearDto>(academicYearEntity);
	}

	public async Task<IEnumerable<AcademicYearNameDto>> GetAllNamesByStudentIdAsync(int studentId, string applicationUserId)
	{
		var academicYearEntities = await _repository.GetAsync<AcademicYear>(ay => ay.Logs.Any(log => log.StudentId == studentId) && ay.ApplicationUserId == applicationUserId);
		return _mapper.Map<IEnumerable<AcademicYearNameDto>>(academicYearEntities);
	}

	public async Task<AcademicYearAllUserDataDto?> GetAcademicYearAllUserDataAsync(int academicYearId, string applicationUserId)
	{
		var data = await _db.AcademicYears
			.Where(ay => ay.ApplicationUserId == applicationUserId && ay.Id == academicYearId)
			.Select(ay => new AcademicYearAllUserDataDto
			{
				Name = ay.Name,
				StartDate = ay.StartDate,
				EndDate = ay.EndDate,
				Students = ay.Logs.Select(l => new StudentAllUserDataDto
				{
					Id = l.StudentId,
					Name = l.Student.Name,
					Logs = l.Student.Logs.Select(sl => new LogAllUserDataDto
					{
						Id = sl.Id,
						DayOfWeek = sl.DayOfWeek,
						LessonStartTime = sl.LessonStartTime,
						LessonEndTime = sl.LessonEndTime,
						LogDays = sl.LogDays.Select(ld => new LogDayDto
						{
							Id = ld.Id,
							BaseLogDayId = ld.BaseLogDayId,
							Content = ld.Content
						}).ToList()
					}).ToList()
				}).ToList()
			}).FirstOrDefaultAsync();
		
		return data;
	}
			

	// CREATE
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
