using Persistence.Interfaces;

namespace Persistence.Entities
{
	public class Student : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public ICollection<Log> Logs { get; set; }
	}
}
