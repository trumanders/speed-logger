namespace Api.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<AcademicYear, AcademicYearCreateDto>().ReverseMap();
		CreateMap<AcademicYear, AcademicYearDto>().ReverseMap();
		CreateMap<AcademicYear, AcademicYearNameDto>().ReverseMap();
		CreateMap<Student, StudentCreateDto>().ReverseMap();
		CreateMap<Student, StudentDto>().ReverseMap();
		CreateMap<StudentUpdateDto, Student>().ReverseMap();
		CreateMap<Log, LogDto>().ReverseMap();
		CreateMap<Log, LogCreateDto>().ReverseMap();
	}
}
