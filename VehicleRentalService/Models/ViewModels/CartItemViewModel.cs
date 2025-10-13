namespace VehicleRentalService.Models.ViewModels
{
    public class CartItemViewModel
    {
        public long Id { get; set; }
        public string VehicleName { get; set; } = "";
        public VehicleType VehicleType { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal Subtotal { get; set; }
    }
}
