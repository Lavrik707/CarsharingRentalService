using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalService.Models
{
    public enum TransmissionType
    {
        Manual,
        Automatic,
        CVT,
        SemiAutomatic
    }

    public class Car : Vehicle
    {
        private double _maxFuel;
        public double MaxFuel
        {
            get => _maxFuel;
            set
            {
                if (value < 1 || value > 70)
                    throw new ArgumentOutOfRangeException(nameof(MaxFuel), "MaxFuel must be in range from 1 to 70");
                _maxFuel = value;
            }
        }

        private double _currentFuel;
        public double CurrentFuel
        {
            get => _currentFuel;
            set
            {
                if (value < 0 || value > MaxFuel)
                    throw new ArgumentOutOfRangeException(nameof(CurrentFuel), $"CurrentFuel must be in range from 0 to MaxFuel ({MaxFuel})");
                _currentFuel = value;
            }
        }

        public TransmissionType Transmission { get; set; }
    }
}
