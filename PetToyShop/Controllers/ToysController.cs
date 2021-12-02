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
    public class ToysController : Controller
    {
        private readonly PetToyShopContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToysController(PetToyShopContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Toys
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Index(string searchString, int petId)
        {
            var toys = _context.Toy
               .Include(t => t.Pet)
               .AsNoTracking();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                toys = toys.Where(s => s.Name.Contains(searchString));
            }

            if (petId != 0)
            {
                toys = toys.Where(s => s.PetId.Equals(petId));
            }

            return View(await toys.ToListAsync());
        }

        // GET: Toys/Details/5
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toy.Include(t=>t.Pet).AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            if (toy == null)
            {
                return NotFound();
            }

            return View(toy);
        }

        // GET: Toys/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            PopulatePetsList();
            return View();
        }

        // POST: Toys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,PetId")] Toy toy)
        {
            //toy.Pet = (from u in _context.User where u.Id == task.UserId select u).FirstOrDefault();
            toy.Pet = _context.Pet.Where(p => p.Id == toy.PetId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                _context.Add(toy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toy);
        }

        // GET: Toys/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toy.FindAsync(id);
            if (toy == null)
            {
                return NotFound();
            }
            PopulatePetsList(toy.PetId);
            return View(toy);
        }

        // POST: Toys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,PetId")] Toy toy)
        {
            if (id != toy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToyExists(toy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toy);
        }

        // GET: Toys/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toy.Include(t=>t.Pet).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toy == null)
            {
                return NotFound();
            }

            return View(toy);
        }

        // POST: Toys/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toy = await _context.Toy.FindAsync(id);
            _context.Toy.Remove(toy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToyExists(int id)
        {
            return _context.Toy.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var toy = _context.Toy.Find(id);
            var purchase = _context.Purchase.Include(p => p.ToyItems).Where(x => x.User.Id == currentUser.Id && x.BankAccount == null).FirstOrDefault();
            if (!purchase.ToyItems.Contains(toy))
            {
                purchase.ToyItems.Add(toy);
                purchase.TotalPrice += _context.Toy.Find(id).Price;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Toys");
        }

        private void PopulatePetsList(object selectedPet = null)
        {
            var users = _context.Pet.OrderBy(p => p.Name).AsNoTracking();
            ViewBag.Pets = new SelectList(users, "Id", "Name", selectedPet);
        }
    }
}
