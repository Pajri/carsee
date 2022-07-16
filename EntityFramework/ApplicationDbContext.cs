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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Car>()
              .HasOne(c => c.User)
              .WithMany(u => u.Cars)
              .HasForeignKey(f => f.UserId)
              .HasConstraintName("UserId")
              .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }

        public DbSet<Car> Car { get; set; }
        public DbSet<DecisionResult> DecisionResults { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
    }
}
