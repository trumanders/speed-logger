
namespace Persistence.Entities;

public class ApplicationUser : IdentityUser
{
	public ICollection<Student> Students { get; set; } = [];
	public ICollection<AcademicYear> AcademicYears { get; set; } = [];
}
