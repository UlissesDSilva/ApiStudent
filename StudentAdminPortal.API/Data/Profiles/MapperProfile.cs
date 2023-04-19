using AutoMapper;
using StudentAdminPortal.API.Models.DomainModels;
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
        }
    }
}