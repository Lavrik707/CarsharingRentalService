using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VehicleRentalService.Models;
using VehicleRentalService.Models.Core;
using VehicleRentalService.Models.ViewModels;

namespace VehicleRentalService.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IServiceRepository _repository;

        public ShoppingCartController(IServiceRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            //getting all data from cookies
            var json = HttpContext.Session.GetString("cart");
            List<CartItem> cartContent = string.IsNullOrEmpty(json)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(json);

            var tripBeginString = HttpContext.Session.GetString("tripBegin");
            DateTime tripBegin = string.IsNullOrEmpty(tripBeginString)
                ? DateTime.Now
                : DateTime.Parse(tripBeginString, null, System.Globalization.DateTimeStyles.RoundtripKind);

            var tripEndString = HttpContext.Session.GetString("tripEnd");
            DateTime tripEnd = string.IsNullOrEmpty(tripEndString)
                ? DateTime.Now
                : DateTime.Parse(tripEndString, null, System.Globalization.DateTimeStyles.RoundtripKind);

            var shoppingCart = new ShoppingCart(_repository)
            {
                CartItems = cartContent ?? new List<CartItem>(),
                TripBegin = tripBegin,
                TripEnd = tripEnd
            };

            var viewModel = new CartViewModel
            {
                TripBegin = shoppingCart.TripBegin,
                TripEnd = shoppingCart.TripEnd,
                Duration = shoppingCart.Duration,
                Total = shoppingCart.CalculateTotalCartPrice(),
                Items = shoppingCart.CartItems.Select(item =>
                {
                    var vehicle = _repository.FindById(item.VehicleType, item.Id);
                    return new CartItemViewModel
                    {
                        Id = item.Id,
                        VehicleName = vehicle?.Name ?? "Unknown",
                        VehicleType = item.VehicleType,
                        Quantity = item.Quantity,
                        PricePerHour = vehicle?.PricePerHour ?? 0,
                        Subtotal = (vehicle?.PricePerHour ?? 0) * (decimal)shoppingCart.Duration.TotalMinutes * item.Quantity
                    };
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RemoveCookies(long Id, VehicleType vehicleType)
        {
            var json = HttpContext.Session.GetString("cart");

            if (!string.IsNullOrEmpty(json))
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(json);
                var itemToRemove = cart.FirstOrDefault(c => c.Id == Id && c.VehicleType == vehicleType);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
                }
            }

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
