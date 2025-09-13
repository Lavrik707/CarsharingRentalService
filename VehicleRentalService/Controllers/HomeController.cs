using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Models;
using VehicleRentalService.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [HttpPost]
        public IActionResult SetCookies(AddToCartViewModel model)
        {
            var json = HttpContext.Session.GetString("cart");
            var cart = string.IsNullOrEmpty(json)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(json);

            var existing = cart.FirstOrDefault(c => c.Id == model.VehicleId && c.VehicleType == model.VehicleType);
            if (existing != null)
            {
                existing.Quantity += model.Quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Id = model.VehicleId,
                    VehicleType = model.VehicleType,
                    Quantity = model.Quantity
                });
            }

            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));

            return RedirectToAction("Index", new { category = model.VehicleType.ToString() });
        }
    }
}
