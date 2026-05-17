using AutoMapper;
using Common.Dtos;
using Persistence.Entities;

namespace Api.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<AcademicYear, AcademicYearCreateDto>().ReverseMap();
			CreateMap<Student, StudentCreateDto>().ReverseMap();
			CreateMap<Student, StudentDto>().ReverseMap();
		}
	}
}
