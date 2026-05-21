using Common.Dtos;

namespace Persistence.Interfaces;

public interface IAcademicYearService
{
	public Task<AcademicYearDto> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId);
}
