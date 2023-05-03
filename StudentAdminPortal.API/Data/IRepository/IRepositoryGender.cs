using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.IRepository
{
    public interface IRepositoryGender
    {
        Task<IEnumerable<Gender>> GetAllGenders();
    }
}