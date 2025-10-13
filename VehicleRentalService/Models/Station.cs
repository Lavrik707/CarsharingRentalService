namespace VehicleRentalService.Models
{
    public class Station
    {
        public long StationId { get; set; }
        public string Address { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
