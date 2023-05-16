using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.IRepository
{
    public interface IRepositoryStudent
    {
        Task<IEnumerable<Student>> GetAllStudent();
        Task<IEnumerable<Student>> GetStudentByName(string name);
        Task<Student> GetStudentById(Guid id);
        Task<Student> CreateStudent(Student student);
        Task<Student> UpdateStudent(Guid id, Student student);
        Task<bool> UploadImage(Guid id, string fileName);
        Task<bool> UploadImageBase64(Guid id, IFormFile fileName);
        Task<bool> DeleteStudent(Guid id);
        Task<bool> ExistStudent(Guid id);
    }
}