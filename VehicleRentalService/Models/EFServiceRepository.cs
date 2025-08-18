namespace VehicleRentalService.Models
{
    public class EFServiceRepository : IServiceRepository
    {
        private ServiceDbContext context;
        public EFServiceRepository(ServiceDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Car> Cars => context.Cars;
        public IQueryable<Bike> Bikes => context.Bikes;
        public IQueryable<Scooter> Scooters => context.Scooters;
        public IQueryable<News> News => context.News;
        public IQueryable<Trip> Trips => context.Trips;
    }
}
