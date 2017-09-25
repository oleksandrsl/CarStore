using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models
{
    public class Make
    {
        public int MakeId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        public List<Model> Models { get; set; }

    }
}
