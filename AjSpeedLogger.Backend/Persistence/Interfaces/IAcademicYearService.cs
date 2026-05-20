using Common.Dtos;

namespace Persistence.Interfaces;

public interface IAcademicYearService
{
	public Task<AcademicYear> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId);
}
