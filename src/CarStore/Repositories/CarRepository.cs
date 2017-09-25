using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Exceptions;
using CarStore.Helpers;
using CarStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repositories
{
    public class CarRepository : ICarRepository
    {
        private StoreContext _context;

        public CarRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _context.Cars.Where(p => p.Order == null)
            .Include(p => p.BodyType)
          .Include(p => p.Model)
          .ThenInclude(p => p.Make).Where(c => c.CarId == id)
          .GroupJoin(_context.Orders, c => c.CarId, o => o.CarId, (c, o) => c).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Car>> GetCarsAsync(CarFilter carFilterParams)
        {
            var filterData = carFilterParams;
            return await _context.Cars.Include(p => p.BodyType)
           .Include(p => p.Model)
           .ThenInclude(p => p.Make)
           .GetCarsByMake(filterData.MakeId)
           .GetCarsByModel(filterData.ModelId)
           .FilterByPrice(filterData.MinPrice, filterData.MaxPrice)
           .GroupJoin(_context.Orders, c => c.CarId, o => o.CarId, (c, o) => c)
           .Where(p => p.Order == null)
           .ToListAsync();
        }

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            var car = _context.Cars.Where(c => c.CarId == id).FirstOrDefault();

            if (car == null)
                throw new CarNotFoundException(string.Format("Car whith {0} id not found!", id));

            _context.Cars.Remove(car);

            _context.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            _context.Cars.Update(car);

            _context.SaveChanges();
        }
    }
}
