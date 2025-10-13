using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalService.Models
{
    public class ShoppingCart
    {
        private readonly IServiceRepository _vehicleRepository;
        public List<CartItem> CartItems { get; set; } = new();

        public DateTime TripBegin { get; set; }
        public DateTime TripEnd { get; set; }

        public ShoppingCart(IServiceRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        }
        public TimeSpan Duration => TripEnd - TripBegin;

        public decimal CalculateTotalCartPrice()
        {
            decimal total = 0;
            Vehicle? vehicle;

            foreach (var item in CartItems)
            {
                vehicle = item.VehicleType switch
                {
                    VehicleType.Car => _vehicleRepository.GetAll<Car>().FirstOrDefault(c => c.VehicleId == item.Id),
                    VehicleType.Bike => _vehicleRepository.GetAll<Bike>().FirstOrDefault(b => b.VehicleId == item.Id),
                    VehicleType.Scooter => _vehicleRepository.GetAll<Scooter>().FirstOrDefault(s => s.VehicleId == item.Id),
                    _ => throw new NotImplementedException()
                };
                if (vehicle != null)
                {
                    total += vehicle.PricePerHour * (decimal)Duration.TotalMinutes * item.Quantity;
                }
            }
            return total;
        }
    }
}
