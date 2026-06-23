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

	public async Task<StudentDto?> UpdateAsync(StudentDto dto, string applicationUserId)
	{
		var existingEntity = await _repository.GetSingleAsync<Student>(e => e.Id == dto.Id && e.ApplicationUserId == applicationUserId);
		if (existingEntity == null)
			return null;
		_mapper.Map(dto, existingEntity); // Maps dto into existing entity
		await _repository.SaveChangesAsync();
		return _mapper.Map<StudentDto>(existingEntity);
	}
}
