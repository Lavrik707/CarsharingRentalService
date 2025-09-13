using Microsoft.AspNetCore.Mvc;

namespace VehicleRentalService.Models.ViewModels
{
    public class CartViewModel 
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public DateTime TripBegin { get; set; }
        public DateTime TripEnd { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Total { get; set; }
    }
}
