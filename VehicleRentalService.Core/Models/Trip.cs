using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalService.Models
{
    public class Trip
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public DateTime TripBegin { get; set; }
        private DateTime _tripEnd;
        public DateTime TripEnd
        {
            get => _tripEnd;

            set
            {
                if (value >= TripBegin)
                {
                    _tripEnd = value;
                }
                else
                {
                    throw new Exception("Trip end can't be earlier than Trip begin");
                }
            }
        }
        [NotMapped]
        public TimeSpan Duration => TripEnd - TripBegin;

        [NotMapped]
        public decimal TripPrice
        {
            get
            {
                if (Vehicle == null)
                    throw new InvalidOperationException("Vehicle is not loaded. Please use include during the request"); //i beg you use include

                var minutes = (decimal)Duration.TotalMinutes;
                return Math.Round(minutes * Vehicle.PricePerHour, 2);
            }
        }

    }
}
