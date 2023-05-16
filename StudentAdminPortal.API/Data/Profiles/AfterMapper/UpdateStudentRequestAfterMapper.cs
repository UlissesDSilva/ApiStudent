using AutoMapper;
using StudentAdminPortal.API.Models.RequestModels;
using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.Profiles.AfterMapper
{
    public class UpdateStudentRequestAfterMapper : IMappingAction<UpdateStudentRequest, Student>
    {
        public void Process(UpdateStudentRequest source, Student destination, ResolutionContext context)
        {
            destination.Address = new Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress,
            };
        }
    }
}