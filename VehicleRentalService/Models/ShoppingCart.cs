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
                switch (item.VehicleType)
                {
                    case VehicleType.Car:
                        vehicle = _vehicleRepository.Cars.FirstOrDefault(c => c.VehicleId == item.Id);
                        break;
                    case VehicleType.Bike:
                        vehicle = _vehicleRepository.Cars.FirstOrDefault(c => c.VehicleId == item.Id);
                        break;
                    case VehicleType.Scooter:
                        vehicle = _vehicleRepository.Cars.FirstOrDefault(c => c.VehicleId == item.Id);
                        break;
                    default:
                        throw new NotImplementedException();

                }
                if (vehicle != null)
                {
                    total += vehicle.PricePerMinute * (decimal)Duration.TotalMinutes * item.Quantity;
                }
            }
            return total;
        }
    }
}
