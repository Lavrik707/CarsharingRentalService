using Microsoft.EntityFrameworkCore;

namespace VehicleRentalService.Models
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options){}
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Scooter> Scooters => Set<Scooter>();
        public DbSet<Bike> Bikes => Set<Bike>();
        public DbSet<News> News => Set<News>();
        public DbSet<Trip> Trips => Set<Trip>();
    }
}
