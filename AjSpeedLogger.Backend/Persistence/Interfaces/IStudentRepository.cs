using Common.Dtos;

namespace Persistence.Interfaces;

public interface IStudentRepository
{
    public Task<List<StudentDto>> GetByAcademicYearIdAsync(int academicYearId, string applicationUserId);
}
