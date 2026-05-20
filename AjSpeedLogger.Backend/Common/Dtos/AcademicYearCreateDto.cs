namespace Common.Dtos;
public class AcademicYearCreateDto
{
	public string Name { get; set; } = string.Empty; // "2026/2027"
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
}
