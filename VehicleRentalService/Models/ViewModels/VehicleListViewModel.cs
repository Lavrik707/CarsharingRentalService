namespace VehicleRentalService.Models.ViewModels
{
    public class VehicleListViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

