using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetToyShop.Models
{
    public class CartViewModel
    {
        public int BankAccountId { get; set; }
        public int ActivePurchaseId { get; set; }
        public Purchase ActivePurchase { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<Toy> Items { get; set; }
    }
}
