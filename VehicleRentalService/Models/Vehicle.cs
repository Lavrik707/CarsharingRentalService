using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalService.Models
{
    public abstract class Vehicle
    {
        public long? VehicleId { get; set; }
        [Required(ErrorMessage = "Please enter a vehicle name")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        [NotMapped]
        public decimal PricePerMinute { get; set; } //Old
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal PricePerHour { get; set; }
        public bool IsAvailable { get; set; }

        public string? Location { get; set; }
    }
}
