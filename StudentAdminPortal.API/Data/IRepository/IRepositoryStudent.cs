using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.IRepository
{
    public interface IRepositoryStudent
    {
        Task<IEnumerable<Student>> GetAllStudent();
        Task<IEnumerable<Student>> GetStudentByName(string name);
        Task<Student> GetStudentById(Guid id);
    }
}