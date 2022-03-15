using CarSee.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CarSee.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        // public ApplicationDbContext()
        // {

        // }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Car> Car {get;set;}
    }
}
