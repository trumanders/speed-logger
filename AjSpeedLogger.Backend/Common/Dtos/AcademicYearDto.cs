namespace Common.Dtos;
public class AcademicYearDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty; // "2026/2027"
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public string ApplicationUserId { get; set; } = string.Empty;

	//public BaseLogDto BaseLog { get; set; } = null!;
	//public ICollection<StudentLogDto> StudentLogs { get; set; } = [];
}
