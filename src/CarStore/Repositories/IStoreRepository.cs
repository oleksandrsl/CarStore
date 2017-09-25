using System.Collections.Generic;
using System.Threading.Tasks;
using CarStore.Models;

namespace CarStore.Repositories
{
    public interface IStoreRepository
    {
        Task<List<Make>> GetMakesAsync();
        Task<List<Model>> GetModelsAsync(int makeId);
        Task<List<Car>> GetCarsAsync();
        Task<List<BodyType>> GetBodyTypesAsync();
        void OrderCar(Order order);
    }
}
