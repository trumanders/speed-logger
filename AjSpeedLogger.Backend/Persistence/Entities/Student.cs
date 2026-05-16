using Persistence.Interfaces;

namespace Persistence.Entities
{
	public class Student : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DayOfWeek LessonDayOfWeek { get; set; }
		public TimeOnly LessonStartTime { get; set; }
		public TimeOnly LessonEndTime { get; set; }

		//public int AcademicYearId { get; set; } = null!;


		//public string ApplicationUserId { get; set; } = string.Empty;

		public ICollection<Log> Logs { get; set; }
	}
}
