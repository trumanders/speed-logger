using Common.Dtos;

namespace Persistence.Repositories;

public class StudentRepository(Context _db, IMapper _mapper) : IStudentRepository
{
    public async Task<List<StudentDto>> GetByAcademicYearIdAsync(int academicYearId, string applicationUserId)
    {
        var studentEntities = await _db.Set<Log>()
            .Where(log => log.AcademicYearId == academicYearId && log.Student.ApplicationUserId == applicationUserId)
            .Select(log => log.Student)
            .ToListAsync();

        return _mapper.Map<List<StudentDto>>(studentEntities);
    }
}
