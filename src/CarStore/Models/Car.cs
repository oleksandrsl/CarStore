using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarStore.Models
{
    public class Car
    {
        public int CarId { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        [Range(1, 1000000)]
        public decimal Price { get; set; }
        [Required]
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        [Required]
        [Range(0.1, 10)]
        public decimal Engine { get; set; }
        public int Year { get; set; }
        [Required]
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public Order Order { get; set; }
    }
}
