using FluentValidation;
using StudentAdminPortal.API.Data.IRepository;
using StudentAdminPortal.API.Models.RequestModels;

namespace StudentAdminPortal.API.Validations
{
    public class AddStudentRequestValidation : AbstractValidator<AddStudentRequest>
    {
        public AddStudentRequestValidation(IRepositoryGender repository) {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotNull();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).NotEmpty().GreaterThan(9999999999).LessThan(100000000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id => {
                var gender = repository.GetAllGenders().Result.ToList().FirstOrDefault(x => x.Id == id);

                return gender != null ? true : false;
            }).WithMessage("Please select a valid gender");
            RuleFor(x => x.AddressId).NotEmpty().NotNull();
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
        }
    }
}