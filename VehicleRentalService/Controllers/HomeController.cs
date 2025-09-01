using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Models;
using VehicleRentalService.Models.ViewModels;

namespace VehicleRentalService.Controllers
{
    public class HomeController : Controller
    {
        private IServiceRepository _repository;
        private int _pageSize = 2;


        public HomeController(IServiceRepository repo)
        {
            _repository = repo;
        }

        public IActionResult Index(string category ="Car", int page = 1)
        {
            ViewData["CurrentCategory"] = category;


            var totalItems = category switch
            {
                "Car" => _repository.Cars.Count(),

                "Bike" => _repository.Bikes.Count(),

                "Scooter" => _repository.Scooters.Count(),

                _ => 0
            };
            Console.WriteLine($"Items count:{totalItems}");

            var vehicles = category switch
            {
                "Car" => _repository.Cars
                .Cast<Vehicle>()
                .OrderBy(v => v.VehicleId)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),

                "Bike" => _repository.Bikes
                .Cast<Vehicle>()
                .OrderBy(v => v.VehicleId)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),

                "Scooter" => _repository.Scooters.Cast<Vehicle>()
                 .OrderBy(v => v.VehicleId)
                 .Skip((page - 1) * _pageSize)
                 .Take(_pageSize),

                _ => Enumerable.Empty<Vehicle>()
            };


            var ViewModelVh = new VehicleListViewModel
            {
                Vehicles = vehicles,

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = totalItems,
                }
            };
            return View(ViewModelVh);
        }

    }
}
