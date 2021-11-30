
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

            var currentUser = _userManager.GetUserId(HttpContext.User);

            var purchase = _context.Purchase.Where(p => p.User.Id == currentUser).Include(t => t.ToyItems).FirstOrDefault();

            if(purchase!=null)
            {
                cartViewModel.Items = purchase.ToyItems ?? new List<Toy>();
                cartViewModel.TotalPrice = purchase.TotalPrice;
            } else
            {
                purchase = new Purchase();
                purchase.User = _context.Users.Find(currentUser);
                purchase.ToyItems = new List<Toy>();
                purchase.TotalPrice = 0;
                _context.Add(purchase);
                _context.SaveChanges();
            }
            cartViewModel.ActivePurchase = purchase;
            cartViewModel.BankAccount = _context.BankAccount.Where(b => b.User.Id == currentUser).FirstOrDefault();
            var bankAccounts = _context.BankAccount.Where(b => b.User.Id == currentUser).OrderBy(p => p.Name).AsNoTracking();
            ViewBag.Pets = new SelectList(bankAccounts, "Id", "Name");
           
            return View(cartViewModel);
        }

    }
}
