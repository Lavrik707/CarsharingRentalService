using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Models;

namespace VehicleRentalService.Controllers
{
    public class HomeController:Controller
    {
        private IServiceRepository repository;

        public HomeController(IServiceRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index() => View(repository.Cars);

    }
}
