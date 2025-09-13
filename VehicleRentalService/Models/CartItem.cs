namespace VehicleRentalService.Models
{
    public class CartItem
    {
        public long Id { get; set; }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value >= 0 ? value : throw new ArgumentOutOfRangeException();
        }
        public VehicleType VehicleType { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Bike,
        Scooter
    }
}
