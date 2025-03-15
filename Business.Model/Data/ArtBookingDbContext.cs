using Business.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Model.Data
{
    public class ArtBookingDbContext : DbContext
    {
        public ArtBookingDbContext(DbContextOptions<ArtBookingDbContext> options) : base(options)   
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ArtOrganization> ArtOrganizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<ArtOrganization>().HasKey(x => x.ArtOrganizationId);
        }
    }
}
