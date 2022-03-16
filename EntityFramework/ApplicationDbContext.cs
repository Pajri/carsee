using System;
using System.Linq;
using CarSee.Constants;
using CarSee.Entities;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Car> Car { get; set; }
    }
}
