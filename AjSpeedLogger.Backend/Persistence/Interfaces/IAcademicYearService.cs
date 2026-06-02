namespace Persistence.Interfaces;

public interface IAcademicYearService
{
	public Task<AcademicYearAllUserDataDto?> GetAcademicYearAllUserDataAsync(string applicationUserId, int? academicYearId);
	public Task<AcademicYearDto> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId);
	public Task<AcademicYearDto?> GetByIdAsync(int id, string applicationUserId);
	public Task<bool> DeleteAsync(int id, string applicationUserId);
	public Task<IEnumerable<AcademicYearNameDto>> GetAllNamesByStudentIdAsync(int studentId, string applicationUserId);
	public Task<IEnumerable<AcademicYearNameDto>> GetAllNamesAsync(string applicationUserId);

}
