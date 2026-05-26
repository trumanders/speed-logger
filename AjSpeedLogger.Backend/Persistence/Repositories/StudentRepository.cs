namespace Persistence.Repositories;


// BEHÖVS DENNA KLASSEN?? Vi hämtar LOGS per ACACEMIC YEAR, och därifrån hämtar vi STUDENTS, och listar dem.
// Inga students utan Logs.
public class StudentRepository(Context _db, IMapper _mapper) : IStudentRepository
{
    public async Task<List<StudentDto>> GetByAcademicYearIdAsync(int academicYearId, string applicationUserId)
    {
        var studentEntities = await _db.Set<Log>()
			.Include(log => log.Student)
            .Where(log => log.AcademicYearId == academicYearId && log.ApplicationUserId == applicationUserId)
            .Select(log => log.Student)
            .ToListAsync();

        return _mapper.Map<List<StudentDto>>(studentEntities);
    }
}
