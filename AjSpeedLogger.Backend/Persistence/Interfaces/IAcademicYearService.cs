namespace Persistence.Interfaces;

public interface IAcademicYearService
{
	public Task<AcademicYearDto> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId);
	public Task<AcademicYearDto?> GetByIdAsync(int id, string applicationUserId);
	public Task<bool> DeleteAsync(int id, string applicationUserId);
	public Task<IEnumerable<AcademicYearNameDto>> GetByStudentIdAsync(int studentId, string applicationUserId);
	public Task<IEnumerable<AcademicYearNameDto>> GetAllAsync(string applicationUserId);

}
