using Microsoft.AspNetCore.Mvc;
using VehicleRentalService.Models;

namespace VehicleRentalService.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public readonly string[] _vehicleTypes = new string[]
        {
            "Bike",
            "Car",
            "Scooter"
        };

        public IViewComponentResult Invoke()
        {
            return View(_vehicleTypes);
        }
    }
}
