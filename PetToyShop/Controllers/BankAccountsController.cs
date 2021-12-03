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
    [Authorize(Roles = "Customer")]
    public class BankAccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly PetToyShopContext _context;

        public BankAccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, PetToyShopContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User); 
            
            return View(await _context.BankAccount.Where(b=> b.User.Id == currentUser.Id ).ToListAsync());
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var bankAccount = await _context.BankAccount
                .Include(b=>b.User)
               .FirstOrDefaultAsync(b => b.Id == id);
            if (_userManager.GetUserId(HttpContext.User) != bankAccount.User.Id)
            {
                return Redirect("~/Identity/Account/AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BankAccountNumber,Amount,User")] BankAccount bankAccount)
        {
            var currentUser = _userManager.GetUserId(HttpContext.User);
            bankAccount.User = _context.Users.Find(currentUser);
            if (ModelState.IsValid)
            {
                _context.Add(bankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var bankAccount = await _context.BankAccount
              .Include(b => b.User)
             .FirstOrDefaultAsync(b => b.Id == id);
           /* var bankAccount = await _context.BankAccount
                .FindAsync(id);*/
            if (_userManager.GetUserId(HttpContext.User) != bankAccount.User.Id)
            {
                return Redirect("~/Identity/Account/AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }
            if (bankAccount == null)
            {
                return NotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BankAccountNumber,Amount")] BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountExists(bankAccount.Id))
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
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var bankAccount = await _context.BankAccount
             .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);
            /* var bankAccount = await _context.BankAccount
                 .FindAsync(id);*/
            if (_userManager.GetUserId(HttpContext.User) != bankAccount.User.Id)
            {
                return Redirect("~/Identity/Account/AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccount = await _context.BankAccount.FindAsync(id);
            _context.BankAccount.Remove(bankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountExists(int id)
        {
            return _context.BankAccount.Any(e => e.Id == id);
        }
    }
}
