namespace Common.Dtos;

public class AcademicYearAllUserDataDto
{
	public string Name { get; set; } = string.Empty; // "2026/2027"
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }

	public List<StudentAllUserDataDto> Students { get; set; }
	public List<BaseLogDayDto> BaseLogDayDtos { get; set; }
}
