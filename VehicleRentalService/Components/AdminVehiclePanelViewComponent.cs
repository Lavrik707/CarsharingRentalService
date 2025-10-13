using Microsoft.AspNetCore.Mvc;
using VehicleRentalService.Models.ViewModels;

namespace VehicleRentalService.Components
{
    public class AdminVehiclePanelViewComponent:ViewComponent
    {
        

        public IViewComponentResult Invoke()
        {
            var model = new VehicleCreateViewModel();
            return View(model);
        }

        /* temp */
        private bool UserIsAdmin()
        {
            return HttpContext.Request.Headers["X-Debug-Admin"] == "true";
        }
    }
}
