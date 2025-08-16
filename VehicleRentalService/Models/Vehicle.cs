using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalService.Models
{
    public abstract class Vehicle
    {
        public long? VehicleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PricePerMinute { get; set; }
        public bool IsAvailable { get; set; }
    }
}
