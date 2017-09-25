using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CarStore.Models
{

    public class Model
    {
        public int ModelId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public int MakeId{get;set;}
        public Make Make {get;set;}
        public List<Car> Cars {get;set;}
    }
}
