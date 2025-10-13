namespace VehicleRentalService.Models
{
    public interface IServiceRepository
    {
        IQueryable<T> GetAll<T>() where T : class;
        T? GetById<T>(long id) where T : class;
        void Create<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Vehicle? FindById(VehicleType vehicleType, long id);

        Task<Vehicle?> FindByIdAsync(long id);
    }
}
