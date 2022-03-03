using CarSee.Entities;
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

        public DbSet<Car> Car {get;set;}
    }
}
