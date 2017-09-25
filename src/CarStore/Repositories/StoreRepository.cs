using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreContext _context;


        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<BodyType>> GetBodyTypesAsync()
        {
            return await _context.BodyTypes.ToListAsync();
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            return await _context.Cars.Include(p => p.BodyType)
            .Include(p => p.Model)
            .ThenInclude(p => p.Make)
            .GroupJoin(_context.Orders, c => c.CarId, o => o.CarId, (c, o) => c).Where(p => p.Order == null)
            .ToListAsync();
        }

        public async Task<List<Make>> GetMakesAsync()
        {
            return await _context.Makes.ToListAsync();
        }

        public async Task<List<Model>> GetModelsAsync(int makeId)
        {
            return await _context.Models.Where(m => m.MakeId == makeId).ToListAsync();
        }

        public void OrderCar(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
