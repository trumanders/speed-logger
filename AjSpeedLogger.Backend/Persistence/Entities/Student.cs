namespace Persistence.Entities;

public class Student : IEntity, IUserOwned
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public string ApplicationUserId { get; set; } = string.Empty;

	public ICollection<Log> Logs { get; set; } = [];
}
