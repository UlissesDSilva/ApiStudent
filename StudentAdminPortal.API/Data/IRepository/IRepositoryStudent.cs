using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.IRepository
{
    public interface IRepositoryStudent
    {
        Task<IEnumerable<Student>> GetAllStudent();
    }
}