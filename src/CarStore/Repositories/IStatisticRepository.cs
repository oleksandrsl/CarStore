using System.Collections.Generic;
using CarStore.Models;

namespace CarStore.Repositories
{
    public interface IStatisticRepository
    {
         List<Sales> GetSalesStatistic(int year);

         List<PopularCar> GetPopularCars(int top);
    }
}
