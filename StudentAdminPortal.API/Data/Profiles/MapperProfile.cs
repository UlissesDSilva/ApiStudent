using AutoMapper;
using StudentAdminPortal.API.Data.Profiles.AfterMapper;
using StudentAdminPortal.API.Models.DomainModels;
using StudentAdminPortal.API.Models.RequestModels;
using Entity = StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<Entity.Student, Student>()
                .ReverseMap();

            CreateMap<Entity.Address, Address>()
                .ReverseMap();

            CreateMap<Entity.Gender, Gender>()
                .ReverseMap();

            CreateMap<UpdateStudentRequest, Entity.Student>()
                .AfterMap<UpdateStudentRequestAfterMapper>();

            CreateMap<AddStudentRequest, Entity.Student>()
                .AfterMap<AddStudentRequestAfterMapper>();
        }
    }
}