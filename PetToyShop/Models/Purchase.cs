using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetToyShop.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Display(Name = "Bank Account")]
        public BankAccount BankAccount { get; set; }
        [Display(Name = "Time Of Purchase")]
        [DataType(DataType.DateTime)]
        // [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime TimeOfPurchase { get; set; }
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Toys")]
        public ICollection<Toy> ToyItems { get; set; }

    }
}
