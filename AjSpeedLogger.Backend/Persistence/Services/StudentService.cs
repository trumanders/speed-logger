namespace Persistence.Services;

public class StudentService(IRepository _repository, IMapper _mapper) : IStudentService
{
	public async Task<List<StudentDto>> GetActiveStudentsByAcademicYear(int academicYearId, string applicationUserId)
	{
		var studentEntities = await _repository.GetAsync<Student>(s =>
			!s.IsTerminated && s.Logs.Any(l => l.AcademicYearId == academicYearId && l.ApplicationUserId == applicationUserId)
		);
		return _mapper.Map<List<StudentDto>>(studentEntities);
	} 
}
