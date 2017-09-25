using System.Collections.Generic;
using System.Threading.Tasks;
using CarStore.Models;

namespace CarStore.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetCarAsync(int id);
        Task<ICollection<Car>> GetCarsAsync(CarFilter carFilterParams);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
    }
}
