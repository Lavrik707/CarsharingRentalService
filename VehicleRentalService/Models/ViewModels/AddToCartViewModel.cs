using Microsoft.AspNetCore.Mvc;

namespace VehicleRentalService.Models.ViewModels
{
    public class AddToCartViewModel
    {
        public long VehicleId { get; set; }
        public VehicleType VehicleType { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
