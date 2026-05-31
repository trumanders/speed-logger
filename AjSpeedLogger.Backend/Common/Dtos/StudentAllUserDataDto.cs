namespace Common.Dtos;

public class StudentAllUserDataDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public List<LogAllUserDataDto> Logs { get; set; }
}
