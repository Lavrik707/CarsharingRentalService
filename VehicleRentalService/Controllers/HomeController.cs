using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Models;
using VehicleRentalService.Models.Core;
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

        public IActionResult Index(string category = "Car", int page = 1)
        {
            ViewData["CurrentCategory"] = category;


            var totalItems = category switch
            {
                "Car" => _repository.GetAll<Car>().Count(),
                "Bike" => _repository.GetAll<Bike>().Count(),
                "Scooter" => _repository.GetAll<Scooter>().Count(),
                _ => 0
            };

#if DEBUG
            Console.WriteLine($"Items count:{totalItems}");
#endif

            var vehicles = category switch
            {
                "Car" => _repository.GetAll<Car>()
                    .Cast<Vehicle>()
                    .OrderBy(v => v.VehicleId)
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize),

                "Bike" => _repository.GetAll<Bike>()
                    .Cast<Vehicle>()
                    .OrderBy(v => v.VehicleId)
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize),

                "Scooter" => _repository.GetAll<Scooter>()
                    .Cast<Vehicle>()
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

        //maybe....
        //[HttpPost]
        //public void CreateVehicle(string Name, string Description,  decimal PricePerHour, bool IsAvailable,
        //    string VehicleType, double? MaxFuel, TransmissionType? Transmission, double? CurrentFuel, double? Charge)
        //{

        //}


        [HttpPost]
        public IActionResult AddVehicle(VehicleCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Validation failed. Please check your input.";
                return RedirectToAction("Index", "Home");
            }

            Vehicle vehicle;

            switch (model.VehicleType)
            {
                case "Car":
                    if (!model.MaxFuel.HasValue || !model.CurrentFuel.HasValue || !model.Transmission.HasValue)
                    {
                        TempData["Error"] = "All fields for Car are required.";
                        return RedirectToAction("Index", "Home");
                    }

                    vehicle = new Car
                    {
                        Name = model.Name,
                        Description = model.Description,
                        PricePerHour = model.PricePerHour,
                        IsAvailable = model.IsAvailable,
                        MaxFuel = model.MaxFuel.Value,
                        CurrentFuel = model.CurrentFuel.Value,
                        Transmission = model.Transmission.Value
                    };
                    break;

                case "Bike":
                    vehicle = new Bike
                    {
                        Name = model.Name,
                        Description = model.Description,
                        PricePerHour = model.PricePerHour,
                        IsAvailable = model.IsAvailable
                    };
                    break;

                case "Scooter":
                    vehicle = new Scooter
                    {
                        Name = model.Name,
                        Description = model.Description,
                        PricePerHour = model.PricePerHour,
                        IsAvailable = model.IsAvailable,
                        Charge = model.Charge ?? 100
                    };
                    break;

                default:
                    TempData["Error"] = "Unknown vehicle type.";
                    return RedirectToAction("Index", "Home");
            }

            _repository.Create(vehicle);
            TempData["Success"] = $"Vehicle '{vehicle.Name}' successfully created.";

            Console.WriteLine($"Created vehicle: {vehicle.Name}, Type: {model.VehicleType}");
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> DeleteVehicle(long id)
        {
            var vehicle = await _repository.FindByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _repository.Delete(vehicle);
            return RedirectToAction("Index", "Home");
        }
    }
}
