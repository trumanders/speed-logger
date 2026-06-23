namespace Persistence.Interfaces;

public interface IStudentService
{
	public Task<List<StudentDto>> GetActiveStudentsByAcademicYear(int academicYearId, string applicationUserId);
	public Task<StudentDto?> UpdateAsync(StudentDto dto, string applicationUserId);
	
}
