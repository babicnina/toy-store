using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetToyShop.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Bank Account Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bank account number must be numeric")]
        [Required]
        [StringLength(12)]
        public string BankAccountNumber { get; set; }
        public double Amount { get; set; }
        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
