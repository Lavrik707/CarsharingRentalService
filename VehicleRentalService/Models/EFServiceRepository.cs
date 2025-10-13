using Microsoft.EntityFrameworkCore;

namespace VehicleRentalService.Models
{
    public class EFServiceRepository : IServiceRepository
    {
        private ServiceDbContext context;
        public EFServiceRepository(ServiceDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return context.Set<T>();
        }
        public T? GetById<T>(long id) where T : class
        {
            return context.Set<T>().Find(id);
        }
        public void Create<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }
        public void Update<T>(T entity) where T : class
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }
        public void Delete<T>(T entity) where T : class
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
        public Vehicle? FindById(VehicleType vehicleType, long id)
        {
            Vehicle vehicle = null;

            switch (vehicleType)
            {
                case VehicleType.Car:
                    vehicle = context.Cars.FirstOrDefault(v => v.VehicleId == id);
                    break;

                case VehicleType.Bike:
                    vehicle = context.Bikes.FirstOrDefault(v => v.VehicleId == id);
                    break;

                case VehicleType.Scooter:
                    vehicle = context.Scooters.FirstOrDefault(v => v.VehicleId == id);
                    break;
            }

            return vehicle;
        }

        public async Task<Vehicle?> FindByIdAsync(long id)
        {
            return await context.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == id);
        }
    }
}
