using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetToyShop.Data;
using PetToyShop.Models;

namespace PetToyShop.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly PetToyShopContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchasesController(PetToyShopContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Purchases
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var purchases = _context.Purchase
             .Include(p => p.User)
             .Include(p=>p.BankAccount)
             .Where(p=>p.User.Id == currentUser.Id)
             .AsNoTracking();

            return View(await purchases.ToListAsync());
        }

        // GET: Purchases/Details/5
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p=>p.User)
                .Include(p=>p.BankAccount)
                .Include(p=>p.ToyItems)
                .FirstOrDefaultAsync(m => m.Id == id);
            System.Diagnostics.Debug.WriteLine("PurchaseId " + purchase.Id);

            foreach (var item in purchase.ToyItems)
            {
                System.Diagnostics.Debug.WriteLine("Element " + item.Name);
            }
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveToy(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var purchase = _context.Purchase.Where(p => p.User.Id == currentUserId && p.BankAccount == null).Include(t => t.ToyItems).FirstOrDefault();
            var toy = _context.Toy.Find(id);
            purchase.ToyItems.Remove(toy);
            purchase.TotalPrice -= toy.Price;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Toys");
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveAllToys(int id)
        {
            var purchase = _context.Purchase.Where(p => p.Id == id).Include(t => t.ToyItems).FirstOrDefault();
            purchase.ToyItems.Clear();
            purchase.TotalPrice = 0;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Toys");
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> ConcludePurchase([Bind("ActivePurchaseId,BankAccountId")] CartViewModel cartViewModel)
        {
            var purchase = _context.Purchase.Find(cartViewModel.ActivePurchaseId);
            purchase.TimeOfPurchase = DateTime.Now;
            purchase.BankAccount = _context.BankAccount.Find(cartViewModel.BankAccountId);
            await _context.SaveChangesAsync(); 
            return RedirectToAction("Index", "Toys");
        }
    }
}
