namespace StudentAdminPortal.API.Models.RequestModels
{
    public class AddStudentRequest
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public string Email { get; set; }

        public long Mobile { get; set; }

        public Guid GenderId { get; set; }

        public Guid AddressId {get; set;}

        public string? ProfileImageUrl {get; set;}

        public string? PhysicalAddress { get; set; }

        public string? PostalAddress { get; set; }
    }
}