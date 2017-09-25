using System.Linq;
using CarStore.Models;

namespace CarStore.Helpers
{
    public static class CarContextExtensions
    {
        private static readonly int invalidPrice = 0;
        private static readonly int invalidId = 0;
        public static IQueryable<Car> GetCarsByMake(this IQueryable<Car> source, int makeId)
        {
            if (!isIdValid(makeId))
                return source;

            return source.Where(c => c.Model.MakeId == makeId);
        }

        public static IQueryable<Car> GetCarsByModel(this IQueryable<Car> source, int modelId)
        {
            if (!isIdValid(modelId))
                return source;

            return source.Where(c => c.Model.ModelId == modelId);
        }

        public static IQueryable<Car> FilterByPrice(this IQueryable<Car> source, int minPrice, int maxPrice)
        {
            if (!isPriceValid(minPrice) && !isPriceValid(maxPrice))
                return source;

            var correctMaxPrice = !isPriceValid(maxPrice)? source.Max(c => c.Price) : maxPrice;

            return source.Where(c => c.Price >= minPrice && c.Price <= correctMaxPrice);
        }

        private static bool isPriceValid(int price) {
            return price > invalidPrice;
        }

        private static bool isIdValid(int id) {
            return id > invalidId;
        }
    }
}
