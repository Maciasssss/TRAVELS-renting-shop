using TRAVELS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TRAVELS.Data
{
    public class TravelDBcontext : IdentityDbContext<User>
    {
        public TravelDBcontext(DbContextOptions<TravelDBcontext> options) : base(options) { }

        public DbSet<Travel> Travels { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public new DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Travel>().ToTable("Wycieczki");
            modelBuilder.Entity<Guide>().ToTable("Przewodnik");
            modelBuilder.Entity<User>().ToTable("Użytkownicy");
            modelBuilder.Entity<Reservation>().ToTable("Rezerwacje");

        }
    }
}
