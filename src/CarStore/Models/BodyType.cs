using System.Collections.Generic;

namespace CarStore.Models
{
    public class BodyType
    {
        public int BodyTypeId { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
    }
}
