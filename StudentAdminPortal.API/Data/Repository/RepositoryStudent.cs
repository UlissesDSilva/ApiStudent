using Microsoft.AspNetCore.Mvc;
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

    }
}