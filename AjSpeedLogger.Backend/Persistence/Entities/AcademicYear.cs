using Persistence.Interfaces;

namespace Persistence.Entities
{
	public class AcademicYear : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty; // "2026/2027"
		public DateOnly StartDate { get; set; }
		public DateOnly EndDate { get; set; }
		public string ApplicationUserId { get; set; } = string.Empty;

		public BaseLog BaseLog { get; set; } = null!;
		public ICollection<StudentLog> StudentLogs { get; set; } = [];
	}
}
