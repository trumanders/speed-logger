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
		CreateMap<Log, LogDto>().ReverseMap();
		CreateMap<Log, LogCreateDto>().ReverseMap();
		CreateMap<LogDay, LogDayDto>();
			//.ForSourceMember(src => src.Log, opt => opt.DoNotValidate())
			//.ForSourceMember(src => src.BaseLogDay, opt => opt.DoNotValidate());
		CreateMap<LogDay, LogDayCreateDto>().ReverseMap();
	}
}
