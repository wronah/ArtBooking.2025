using Business.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Model.Data;

public class ArtBookingDbContext : DbContext
{
    public ArtBookingDbContext(DbContextOptions<ArtBookingDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<ArtOrganization> ArtOrganizations { get; set; }
    public DbSet<ArtEvent> ArtEvents { get; set; }
    public DbSet<PriceList> PriceLists { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<ScheduleItem> ScheduleItems { get; set; }
    public DbSet<PriceEntry> PriceEntries { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Seat> Seats { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<ArtOrganization>().HasKey(o => o.Id);
        modelBuilder.Entity<ArtEvent>().HasKey(x => x.Id);
        modelBuilder.Entity<PriceList>().HasKey(x => x.Id);
        modelBuilder.Entity<Venue>().HasKey(x => x.Id);
        modelBuilder.Entity<ScheduleItem>().HasKey(x => x.Id);
        modelBuilder.Entity<PriceEntry>().HasKey(x => x.Id);
        modelBuilder.Entity<Area>().HasKey(x => x.Id);
        modelBuilder.Entity<Ticket>().HasKey(x => x.Id);
        modelBuilder.Entity<Seat>().HasKey(x => x.Id);

        modelBuilder.Entity<Seat>()
            .HasOne(x => x.Ticket)
            .WithOne(x => x.Seat)
            .HasForeignKey<Ticket>(x => x.SeatId);

        modelBuilder.Entity<PriceList>()
            .HasOne(x => x.Venue)
            .WithOne(x => x.PriceList)
            .HasForeignKey<Venue>(x => x.PriceListId);

    }
}