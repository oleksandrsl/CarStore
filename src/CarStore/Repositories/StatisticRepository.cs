using System;
using System.Collections.Generic;
using CarStore.Helpers;
using CarStore.Models;

namespace CarStore.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private StoreContext _context;

        public StatisticRepository(StoreContext context)
        {
            _context = context;
        }

        public List<PopularCar> GetPopularCars(int top)
        {
            var popularCars = new List<PopularCar>();
            string query = string.Format(@"exec dbo.popularCars {0}", top);

            return SqlHelper.ExecSQL<PopularCar>(query, _context);
        }

        public List<Sales> GetSalesStatistic(int year)
        {
            var sales = new List<Sales>();
            string query = string.Format(@"exec dbo.statsByYear {0}", year);

            return SqlHelper.ExecSQL<Sales>(query, _context);
        }
    }
}
