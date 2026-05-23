
using System.Globalization;

namespace Persistence.Services;

public class LogService(IRepository _repository, IMapper _mapper) : ILogService
{

	public async Task<LogDto?> GetByIdAsync(int id, string applicationUserId)
	{		
		var logEntity = await _repository.GetByIdAsync<Log>(log => log.Id == id && log.ApplicationUserId == applicationUserId);

		return logEntity == null ? null : _mapper.Map<LogDto>(logEntity);
	}

	public async Task<LogDto?> CreateLogAsync(LogCreateDto createDto, string applicationUserId)
	{
		var academicYear = await _repository.GetByIdAsync<AcademicYear>(ay => createDto.AcademicYearId == ay.Id && ay.ApplicationUserId == applicationUserId);
		if (academicYear == null) return null;

		var logEntity = await _repository.AddAsync<Log, LogCreateDto>(createDto);
		logEntity.ApplicationUserId = applicationUserId;

		// Generate the log days
		var academicYearStartDate = academicYear.StartDate;
		var academicYearEndDate = academicYear.EndDate;
		var daysDiff = (int)logEntity.DayOfWeek - (int)academicYearStartDate.DayOfWeek;
		daysDiff = daysDiff < 0 ? daysDiff + 7 : daysDiff; // logStartDate will jump to the next week because academicYear starts on a weekday that is after the log's weekday
		var logStartDate = academicYearStartDate.AddDays(daysDiff);
		var currentDate = logStartDate;
		var logDays = new List<LogDay>();

		while (currentDate <= academicYearEndDate)
		{
			var logDay = new LogDay
			{
				Date = currentDate,
				WeekNumber = ISOWeek.GetWeekOfYear(currentDate),
				Log = logEntity
			};
			logDays.Add(logDay);
			currentDate = currentDate.AddDays(7);
		}

		await _repository.AddRangeAsync(logDays); 

		await _repository.SaveChangesAsync();

		return _mapper.Map<LogDto>(logEntity);
	}
}