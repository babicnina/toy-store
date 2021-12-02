using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetToyShop.Models
{
    public class Toy
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 999.99)]
        public decimal Price { get; set; }
        public int? PetId { get; set; }
        public Pet Pet { get; set; }
        public ICollection<Purchase> Purchases { get; set; }


    }
}
