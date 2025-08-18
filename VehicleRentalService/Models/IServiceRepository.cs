namespace VehicleRentalService.Models
{
    public interface IServiceRepository
    {
        IQueryable<Car> Cars { get; }
        IQueryable<Bike> Bikes { get; }
        IQueryable<Scooter> Scooters { get; }
        IQueryable<Trip> Trips { get; }
        IQueryable<News> News { get; }
    }
}
