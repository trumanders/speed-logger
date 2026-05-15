using Persistence.Interfaces;

namespace Persistence.Entities
{
	public class LogDay : IEntity
	{
		public int Id { get; set; }
		public DateOnly Date { get; set; }
		public string Content { get; set; }
		public int? BaseLogId { get; set; }
		public int? StudentLogId { get; set; }
	}
}