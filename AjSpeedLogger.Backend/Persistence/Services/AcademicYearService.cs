using System.Net.WebSockets;
using System.Windows.Markup;

namespace Persistence.Services;

public class AcademicYearService(IRepository _repository, IMapper _mapper, IContext _db, IBaseLogDayService _baseLogDayService) : IAcademicYearService
{
	// GET
	public async Task<IEnumerable<AcademicYearDto>> GetAllAsync(string applicationUserId)
	{
		var academicYears = await _repository.GetAsync<AcademicYear>(ay => ay.ApplicationUserId == applicationUserId);
		return _mapper.Map<IEnumerable<AcademicYearDto>>(academicYears);
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
			if (userAllAcademicYears == null || !userAllAcademicYears.Any())
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
				BaseLogDays = ay.BaseLogDays.Select(bld => new BaseLogDayDto
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
							AcademicYearId = sl.AcademicYearId,
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

	public async Task<bool> UpdateAsync(int id, AcademicYearUpdateDto dto, string applicationUserId)
	{
		var existingEntityWithBaseLogDays = await _repository.GetSingleWithIncludesAsync<AcademicYear>(ay => ay.Id == id && ay.ApplicationUserId == applicationUserId, "BaseLogDays");
		if (existingEntityWithBaseLogDays == null || existingEntityWithBaseLogDays.BaseLogDays == null)
			return false;

		var existingFirstDate = existingEntityWithBaseLogDays.BaseLogDays.OrderBy(b => b.Date).FirstOrDefault().Date;
		if (existingFirstDate > dto.StartDate)
		{			
			var date = dto.StartDate;
			while (date < existingFirstDate)
			{
				existingEntityWithBaseLogDays.BaseLogDays.Add(new BaseLogDay
				{
					Date = date,
					WeekNumber = ISOWeek.GetWeekOfYear(date)
				});
				date = date.AddDays(1);
			}
		}

		var existingLastDate = existingEntityWithBaseLogDays.BaseLogDays.OrderBy(b => b.Date).LastOrDefault().Date;
		if (existingLastDate != default && existingLastDate < dto.EndDate)
		{
			var date = existingLastDate.AddDays(1);
			while (date <= dto.EndDate)
			{
				existingEntityWithBaseLogDays.BaseLogDays.Add(new BaseLogDay
				{
					Date = date,
					WeekNumber = ISOWeek.GetWeekOfYear(date)
				});
				date = date.AddDays(1);
			}
		}

		_mapper.Map(dto, existingEntityWithBaseLogDays); // Maps dto into existing entity

		await _repository.SaveChangesAsync();
		return true;
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
