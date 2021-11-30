using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetToyShop.Models
{
    public class CartViewModel
    {
        public Purchase ActivePurchase {get; set;}
        public BankAccount BankAccount { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<Toy> Items { get; set; }
    }
}
