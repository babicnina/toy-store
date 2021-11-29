using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetToyShop.Models;

namespace PetToyShop.Data
{
    public class PetToyShopContext : IdentityDbContext<ApplicationUser>
    {
        public PetToyShopContext (DbContextOptions<PetToyShopContext> options)
            : base(options)
        {
        }

        public DbSet<PetToyShop.Models.Toy> Toy { get; set; }

        public DbSet<PetToyShop.Models.Pet> Pet { get; set; }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Toy>()
                .HasOne(p => p.Pet)
                .WithMany(b => b.Toys);
        }*/
    }
}
