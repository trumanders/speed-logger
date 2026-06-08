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

	public async Task<AcademicYearAllUserDataDto?> GetAcademicYearAllUserDataAsync(string applicationUserId, int? academicYearId)
	{
		int? selectedAcademicYearId;
		if (academicYearId == null || !_db.AcademicYears.Any(ay => ay.Id == academicYearId)) // No academic years is explicitly selected
		{
			// Determine what academic year to fetch
			var userAllAcademicYears = await _db.AcademicYears
				.Where(ay => ay.ApplicationUserId == applicationUserId)
				.ToListAsync();
			if (userAllAcademicYears == null ||!userAllAcademicYears.Any())
				return null;
			var today = DateOnly.FromDateTime(DateTime.Today);
			var selectedAcademicYear = userAllAcademicYears.FirstOrDefault(uay => today >= uay.StartDate && today <= uay.EndDate);
			selectedAcademicYearId = selectedAcademicYear == null
				? userAllAcademicYears.OrderByDescending(uay => uay.EndDate).First().Id
				: selectedAcademicYear.Id;
		}
		else
		{
			selectedAcademicYearId = academicYearId;
		}	

		var data = await _db.AcademicYears
			.Where(ay => ay.ApplicationUserId == applicationUserId && ay.Id == selectedAcademicYearId)
			.Select(ay => new AcademicYearAllUserDataDto
			{
				Name = ay.Name,
				StartDate = ay.StartDate,
				EndDate = ay.EndDate,
				BaseLogDayDtos = ay.BaseLogDays.Select(bld => new BaseLogDayDto
				{
					Id = bld.Id,
					Date = bld.Date,
					WeekNumber = bld.WeekNumber,
					Content = bld.Content
				}).ToList(),
				Students = ay.Logs.Select(ayl => new StudentAllUserDataDto
				{	
					Id = ayl.StudentId,
					Name = ayl.Student.Name,
					IsTerminated = ayl.Student.IsTerminated,
					Log = ayl.Student.Logs
						.Where(sl => sl.AcademicYearId == selectedAcademicYearId)
						.Select(sl => new LogAllUserDataDto
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
						}).FirstOrDefault()!
				}).ToList()
			}).FirstOrDefaultAsync();
		
		return data;
	}			

	// CREATE
	public async Task<AcademicYearDto> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId)
	{
		var academicYearEntity = _mapper.Map<AcademicYear>(dto);
		academicYearEntity.ApplicationUserId = applicationUserId;
		await _repository.AddAsync<AcademicYear>(academicYearEntity);

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
