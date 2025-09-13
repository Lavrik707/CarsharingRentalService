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

        public Vehicle? FindById(VehicleType vehicleType, long id)
        {
            Vehicle vehicle = null;

            switch (vehicleType)
            {
                case VehicleType.Car:
                    vehicle = context.Cars.FirstOrDefault(v => v.VehicleId == id);
                    break;

                case VehicleType.Bike:
                    vehicle = context.Bikes.FirstOrDefault(v => v.VehicleId == id);
                    break;

                case VehicleType.Scooter:
                    vehicle = context.Scooters.FirstOrDefault(v => v.VehicleId == id);
                    break;
            }

            return vehicle;
        }
    }
}
