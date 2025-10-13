using System.ComponentModel.DataAnnotations;

namespace VehicleRentalService.Models.ViewModels
{
    public class VehicleCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal PricePerHour { get; set; }
        public bool IsAvailable { get; set; } = true;
        [Required]
        public string VehicleType { get; set; } = "Car";
        public double? MaxFuel { get; set; }
        public TransmissionType? Transmission {  get; set; }
        public double? CurrentFuel { get; set; }
        public int? Charge { get; set; }
    }
}
