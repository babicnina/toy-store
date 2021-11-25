using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetToyShop.Models;

namespace PetToyShop.Data
{
    public class PetToyShopContext : IdentityDbContext
    {
        public PetToyShopContext (DbContextOptions<PetToyShopContext> options)
            : base(options)
        {
        }

        public DbSet<PetToyShop.Models.Toy> Toy { get; set; }
    }
}
