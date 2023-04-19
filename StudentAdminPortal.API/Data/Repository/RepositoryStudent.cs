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
            return await _context.Student.Include(x => x.Address).Include(nameof(Gender)).ToListAsync();
        }
    }
}