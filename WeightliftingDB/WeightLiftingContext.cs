using Microsoft.EntityFrameworkCore;
using WeightLifting.Persistance.Model;

namespace WeightLifting.Persistance
{
    public class WeightliftingContext(DbContextOptions<WeightliftingContext> options) : DbContext(options)
    {
        public DbSet<Athletes> Athletes { get; set; }
        public DbSet<PushAttempts> PushAttempts { get; set; }
        public DbSet<StartAttempts> StartAttempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athletes>().ToTable("Athlete");
            modelBuilder.Entity<PushAttempts>().ToTable("PushAttempt");
            modelBuilder.Entity<StartAttempts>().ToTable("StartAttempt");
        }

    }
}
