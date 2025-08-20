using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Models;
using VehicleRentalService.Models.ViewModels;

namespace VehicleRentalService.Controllers
{
    public class HomeController : Controller
    {
        private IServiceRepository _repository;
        private int _pageSize = 5;


        public HomeController(IServiceRepository repo)
        {
            _repository = repo;
        }

        public IActionResult Index(int page = 1)
        {
            var AllVehicles = new List<Vehicle>();

            AllVehicles.AddRange(_repository.Cars);
            AllVehicles.AddRange(_repository.Bikes);
            AllVehicles.AddRange(_repository.Scooters);

            var ViewModelVh = new VehicleListViewModel
            {
                Vehicles = AllVehicles
                .OrderBy(v => v.VehicleId)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),


                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = AllVehicles.Count()
                }


            };
            return View(ViewModelVh);
        }

    }
}
