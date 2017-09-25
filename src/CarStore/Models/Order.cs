using System;
using System.ComponentModel.DataAnnotations;

namespace CarStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public int CarId{get;set;}
        public Car Car { get; set; }
        public DateTime DateOrder { get; set; } = DateTime.Now;

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 7)]
        public string Phone { get; set; }
    }
}
