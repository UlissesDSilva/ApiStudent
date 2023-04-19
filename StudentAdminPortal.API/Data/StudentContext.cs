using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Models.Entites;

namespace StudentAdminPortal.API.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }

        public DbSet<Student> Student {get; set;} = default!;

        public DbSet<Gender> Gender {get; set;} = default!;

        public DbSet<Address> Address {get; set;} = default!;
    }
}