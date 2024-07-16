using BarberAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BarberAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<BarberModel> Barbers { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<ReservationServiceModel> ReservationServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación muchos a muchos entre Reservations y Services
            modelBuilder.Entity<ReservationServiceModel>()
                .HasKey(rs => new { rs.ReservationId, rs.ServiceId });

            modelBuilder.Entity<ReservationServiceModel>()
                .HasOne(rs => rs.Reservation)
                .WithMany(r => r.ReservationServices)
                .HasForeignKey(rs => rs.ReservationId);

            modelBuilder.Entity<ReservationServiceModel>()
                .HasOne(rs => rs.Service)
                .WithMany(s => s.ReservationServices)
                .HasForeignKey(rs => rs.ServiceId);
        }

    }
}
