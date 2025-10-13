using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleRentalService.Models;

namespace VehicleRentalService.Controllers
{
    public class EditController : Controller
    {
        private IServiceRepository _repository;


        public EditController(IServiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Index(long id)
        {
             var vehicle= await _repository.FindByIdAsync(id);


            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditVehicle(Vehicle vehicle)
        {
            if(!ModelState.IsValid)
                return View(vehicle);

            _repository.Update(vehicle);
            return RedirectToAction("Home");
        }

    }
}
