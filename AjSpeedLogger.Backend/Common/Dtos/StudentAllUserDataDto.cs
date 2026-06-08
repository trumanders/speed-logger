namespace Common.Dtos;

public class StudentAllUserDataDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public bool IsTerminated { get; set; }

	public LogAllUserDataDto Log { get; set; }
}
