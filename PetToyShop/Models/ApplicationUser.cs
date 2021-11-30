using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetToyShop.Models
{
    public class ApplicationUser: IdentityUser
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<BankAccount> Accounts { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
