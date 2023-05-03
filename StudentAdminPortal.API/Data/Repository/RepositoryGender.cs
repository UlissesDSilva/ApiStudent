using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Data.IRepository;
using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data.Repository
{
    public class RepositoryGender : IRepositoryGender
    {
        private readonly StudentContext _context;

        public RepositoryGender(StudentContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Gender>> GetAllGenders()
        {
            var result = await _context.Gender.ToListAsync();
            return result;
        }
    }
}