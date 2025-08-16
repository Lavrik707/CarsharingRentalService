using Microsoft.AspNetCore.Mvc;

namespace VehicleRentalService.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index() => View();
    }
}
