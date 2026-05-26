namespace Persistence.Entities;

public class Student : IEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public bool IsTerminated { get; set; }

	public string ApplicationUserId { get; set; } = string.Empty;

	public ICollection<Log> Logs { get; set; } = [];
}
