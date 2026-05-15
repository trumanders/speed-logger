using Persistence.Interfaces;

namespace Persistence.Entities
{
	public class StudentLog : IEntity
	{
		public int Id { get; set; }
		public int StudentId { get; set; }
		public int AcademicYearId { get; set; }
		public ICollection<LogDay> LogDays { get; set; } = [];
	}
}
