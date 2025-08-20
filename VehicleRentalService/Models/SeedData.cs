using Microsoft.EntityFrameworkCore;
using VehicleRentalService.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VehicleRentalService.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ServiceDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<ServiceDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            // ------------------ Cars ------------------
            if (!context.Cars.Any())
            {
                context.Cars.AddRange(
                    new Car
                    {
                        Name = "Toyota Corolla",
                        Description = "Reliable compact car",
                        PricePerMinute = 0.50m,
                        IsAvailable = true,
                        Location = "Shevchenko St 12, 10000 Zhytomyr",
                        MaxFuel = 50,
                        CurrentFuel = 50,
                        Transmission = TransmissionType.Automatic
                    },
                    new Car
                    {
                        Name = "Ford Mustang",
                        Description = "Sporty coupe",
                        PricePerMinute = 1.20m,
                        IsAvailable = true,
                        Location = "Khmelnytskyi St 45, 10001 Zhytomyr",
                        MaxFuel = 60,
                        CurrentFuel = 40,
                        Transmission = TransmissionType.Manual
                    },
                    new Car
                    {
                        Name = "Tesla Model 3",
                        Description = "Electric sedan",
                        PricePerMinute = 2.00m,
                        IsAvailable = true,
                        Location = "Kotsiubynskoho St 1, 10002 Zhytomyr",
                        MaxFuel = 70,
                        CurrentFuel = 70,
                        Transmission = TransmissionType.CVT
                    },
                    new Car
                    {
                        Name = "Daewoo Matiz",
                        Description = "Best car of the World",
                        PricePerMinute = 10.00m,
                        IsAvailable = true,
                        Location = "Lisna 7, 10001 Zhytomyr",
                        MaxFuel = 70,
                        CurrentFuel = 70,
                        Transmission = TransmissionType.Automatic
                    }
                );
            }

            // ------------------ Bikes ------------------
            if (!context.Bikes.Any())
            {
                context.Bikes.AddRange(
                    new Bike
                    {
                        Name = "Mountain Bike",
                        Description = "Off-road bike",
                        PricePerMinute = 0.15m,
                        IsAvailable = true,
                        Location = "Sadova St 7, 10003 Zhytomyr"
                    },
                    new Bike
                    {
                        Name = "City Bike",
                        Description = "Comfortable urban bike",
                        PricePerMinute = 0.10m,
                        IsAvailable = true,
                        Location = "Bohdana Khmelnytskoho St 10, 10004 Zhytomyr"
                    },
                    new Bike
                    {
                        Name = "Racing Bike",
                        Description = "Lightweight bike for speed",
                        PricePerMinute = 0.25m,
                        IsAvailable = true,
                        Location = "Akademichna St 3, 10005 Zhytomyr"
                    }
                );
            }

            // ------------------ Scooters ------------------
            if (!context.Scooters.Any())
            {
                context.Scooters.AddRange(
                    new Scooter
                    {
                        Name = "Xiaomi M365",
                        Description = "Electric scooter",
                        PricePerMinute = 0.30m,
                        IsAvailable = true,
                        Location = "Tupikova St 18, 10006 Zhytomyr",
                        Charge = 90
                    },
                    new Scooter
                    {
                        Name = "Segway Ninebot",
                        Description = "Self-balancing scooter",
                        PricePerMinute = 0.35m,
                        IsAvailable = true,
                        Location = "Svobody St 22, 10007 Zhytomyr",
                        Charge = 75
                    },
                    new Scooter
                    {
                        Name = "Boosted Rev",
                        Description = "High-performance electric scooter",
                        PricePerMinute = 0.50m,
                        IsAvailable = true,
                        Location = "Mira St 99, 10008 Zhytomyr",
                        Charge = 100
                    }
                );
            }

            context.SaveChanges();

            // ------------------ Trips ------------------
            if (!context.Trips.Any())
            {
                var ids = context.Cars.Select(c => c.VehicleId!.Value)
                    .Concat(context.Bikes.Select(b => b.VehicleId!.Value))
                    .Concat(context.Scooters.Select(s => s.VehicleId!.Value))
                    .ToList();

                if (ids.Count >= 3)
                {
                    context.Trips.AddRange(
                        new Trip
                        {
                            VehicleId = ids[0],
                            TripBegin = DateTime.Now.AddHours(-2),
                            TripEnd = DateTime.Now.AddHours(-1)
                        },
                        new Trip
                        {
                            VehicleId = ids[1],
                            TripBegin = DateTime.Now.AddHours(-1.5),
                            TripEnd = DateTime.Now.AddHours(-0.5)
                        },
                        new Trip
                        {
                            VehicleId = ids[2],
                            TripBegin = DateTime.Now.AddHours(-3),
                            TripEnd = DateTime.Now.AddHours(-2)
                        }
                    );
                }
            }

            // ------------------ News ------------------
            if (!context.News.Any())
            {
                context.News.AddRange(
                    new News
                    {
                        Title = "New Cars Available",
                        Description = "Check out our latest car models.",
                        PublicationDate = DateTime.Now.AddDays(-2)
                    },
                    new News
                    {
                        Title = "Bike Discounts",
                        Description = "Get up to 20% off on bikes this week.",
                        PublicationDate = DateTime.Now.AddDays(-1)
                    },
                    new News
                    {
                        Title = "Scooter Safety Tips",
                        Description = "Always wear a helmet when riding.",
                        PublicationDate = DateTime.Now
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
