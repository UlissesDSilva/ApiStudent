using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Models.Entites;
using StudentAdminPortal.API.Data.IRepository;

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

        public async Task<IEnumerable<Student>> GetStudentByName(string name) {
            var result = await _context.Student.Where(x => x.FirstName.Contains(name)).Include(nameof(Gender)).ToListAsync();
            return result;
        }
    }
}