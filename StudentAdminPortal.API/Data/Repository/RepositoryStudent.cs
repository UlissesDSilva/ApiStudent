using System.Drawing;
using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Models.Entites;
using StudentAdminPortal.API.Data.IRepository;
using StudentAdminPortal.API.Models.Exceptions;

namespace StudentAdminPortal.API.Data.Repository
{
    public class RepositoryStudent : IRepositoryStudent
    {
        private readonly StudentContext _context;

        public RepositoryStudent(StudentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            var result = await _context.Student.Include(x => x.Address).Include(nameof(Gender)).ToListAsync();
            return result;

        }

        public async Task<Student> GetStudentById(Guid id)
        {
            if (id == null) {
                throw new NotFoundException("Id is null");
            }

            var result = await _context.Student.Where(x => x.Id.Equals(id)).Include(nameof(Address)).Include(nameof(Gender)).FirstOrDefaultAsync();

            if(result == null) {
                throw new NotFoundException("Student not found");
            }
            return result;
        }

        public async Task<IEnumerable<Student>> GetStudentByName(string name) {
            if (name == null) {
                throw new NotFoundException("Name is null");
            }

            var result = await _context.Student.Where(x => x.FirstName.Contains(name)).Include(nameof(Address)).Include(nameof(Gender)).ToListAsync();

            if(result == null) {
                throw new NotFoundException("Students not found");
            }
            return result;
        }

        public async Task<Student> CreateStudent(Student student) {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
            var result = await GetStudentById(student.Id);
            return result;
        }

        public async Task<Student> UpdateStudent(Guid id, Student request) {
            var existingStudent = await GetStudentById(id);
            if (existingStudent is not null) {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName= request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.ProfileImageUrl = request.ProfileImageUrl;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;
                
                await _context.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        public async Task<bool> ExistStudent(Guid id)
        {
            return await _context.Student.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            var student = await GetStudentById(id);

            if (student != null) {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
            }

            var result = await _context.Student.AnyAsync(x => x.Id != id);
            return result;
        }

        public async Task<bool> UploadImage(Guid id, string fileName)
        {
            var student = await GetStudentById(id);

            if (student != null) 
            {
                student.ProfileImageUrl = fileName;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UploadImageBase64(Guid id, IFormFile fileName)
        {
            if (fileName != null) {
                using(MemoryStream ms = new MemoryStream()) {
                    await fileName.CopyToAsync(ms);
                    using(Image image = Image.FromStream(ms)) {
                        image.Save(ms, image.RawFormat);
                        byte[] bt = ms.ToArray();
                        string base64 = Convert.ToBase64String(bt);
                        var student = await GetStudentById(id);
                        if (student != null) {
                            student.ProfileImageUrl = base64;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }

                }
            }
            return false;
        }
    }
}