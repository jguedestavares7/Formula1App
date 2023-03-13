using Formula1API.Models;
using Microsoft.EntityFrameworkCore;

namespace Formula1API.Data
{
    public class Formula1DbContext : DbContext
    {
        public Formula1DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Race> Races { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Formula1DB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure Team entity
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.Id)
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Drivers)
                .WithOne(d => d.Team)
                .HasForeignKey(d => d.TeamId);

            //Configure Driver entity
            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.Id)
                .IsUnique();

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Team)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.TeamId);
            
            modelBuilder.Entity<Driver>()
                .HasMany(d => d.Races)
                .WithOne(r => r.WinnerDriver)
                .HasForeignKey(r => r.WinnerDriverId);

            //Configure Race entity
            modelBuilder.Entity<Race>()
                .HasIndex(r => r.Id)
                .IsUnique();

            modelBuilder.Entity<Race>()
                .HasOne(r => r.WinnerDriver)
                .WithMany(d => d.Races)
                .HasForeignKey(r => r.WinnerDriverId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
