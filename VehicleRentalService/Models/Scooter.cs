using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentalService.Models
{
    public class Scooter:Vehicle
    {
        private int _charge;

        [Range(0, 100)]
        public int Charge
        {
            get => _charge;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _charge = value;
                }
            }
        }
    }
}
