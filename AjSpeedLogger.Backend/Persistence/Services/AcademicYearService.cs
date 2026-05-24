namespace Persistence.Services;

public class AcademicYearService(IRepository _repository, IMapper _mapper) : IAcademicYearService
{
	public async Task<AcademicYearDto> CreateAcademicYearAsync(AcademicYearCreateDto dto, string applicationUserId)
	{
		var academicYearEntity = await _repository.AddAsync<AcademicYear, AcademicYearCreateDto>(dto);
		academicYearEntity.ApplicationUserId = applicationUserId;
		academicYearEntity.BaseLog = new BaseLog
		{
			AcademicYearId = academicYearEntity.Id // Works because the entity is being tracked by the context, so when we save changes, it will update the BaseLog's AcademicYearId with the correct value
		};

		await _repository.SaveChangesAsync();

		return _mapper.Map<AcademicYearDto>(academicYearEntity);
	}	

	public async Task<AcademicYearDto?> GetByIdAsync(int id, string applicationUserId)
	{
		var academicYearEntity = await _repository.GetByIdAsync<AcademicYear>(ay => ay.Id == id && ay.ApplicationUserId == applicationUserId);

		return _mapper.Map<AcademicYearDto>(academicYearEntity);
	}
}
