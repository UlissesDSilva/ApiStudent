using AutoMapper;
using StudentAdminPortal.API.Models.RequestModels;
using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.Profiles.AfterMapper
{
    public class AddStudentRequestAfterMapper : IMappingAction<AddStudentRequest, Student>
    {
        public void Process(AddStudentRequest source, Student destination, ResolutionContext context)
        {
            destination.Address = new Address()
            {
                Id = source.AddressId,
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}