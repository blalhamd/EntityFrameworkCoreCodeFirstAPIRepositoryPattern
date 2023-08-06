
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Entities;

namespace MiddelLayer.APPDBCONTEXT
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Doctor> doctors { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


    }
}
