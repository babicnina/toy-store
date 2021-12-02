using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetToyShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetToyShop.Models
{
    public class ToysViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PetToyShopContext _context;

        public ToysViewComponent(PetToyShopContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            CartViewModel cartViewModel = new CartViewModel();
            cartViewModel.Items = new List<Toy>();
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var purchase = GetActivePurchase().Result;
            cartViewModel.ActivePurchase = purchase;
            cartViewModel.ActivePurchaseId = purchase.Id;
            var bankAccounts = _context.BankAccount.Where(b => b.User.Id == currentUserId).OrderBy(p => p.Name).AsNoTracking();
            ViewBag.BankAccounts = new SelectList(bankAccounts, "Id", "Name");
            return View(cartViewModel);
        }

        public async Task<Purchase> GetActivePurchase()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            Purchase purchase = _context.Purchase.Include(p => p.ToyItems).Where(x => x.User.Id == currentUserId && x.BankAccount == null).FirstOrDefault();
            
            if (purchase == null)
            {
                purchase = new Purchase();
                purchase.User = _context.Users.Find(currentUserId);
                purchase.TimeOfPurchase = DateTime.Now;
                purchase.TotalPrice = 0;
                purchase.ToyItems = new List<Toy>();
                _context.Add(purchase);
                await _context.SaveChangesAsync();
            }
            purchase.ToyItems = purchase.ToyItems ?? new List<Toy>();
            return purchase;
        }
    }
}
