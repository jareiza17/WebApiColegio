using AutoMapper;
using DomainLayer.DTOs;
using ModelsLayer.Entities;

namespace WebApiColegios.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStudentDTO, Student>();
            CreateMap<UpdateStudentDTO, Student>();
            CreateMap<Student, StudentDTO>();

            CreateMap<CreateSubjectDTO, Subject>();
            CreateMap<UpdateSubjectDTO, Subject>();
            CreateMap<Subject, SubjectDTO>();
        }
    }

}
