using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetToyShop.Data;
using PetToyShop.Models;

namespace ToyShop.Seeders
{
    public class PetsSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PetToyShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PetToyShopContext>>()))
            {
                // Look for any toys.
                if (context.Pet.Any())
                {
                    return;   // DB has been seeded
                }

                context.Pet.AddRange(
                    new Pet
                    {
                        Name = "Cat",
                    },

                    new Pet
                    {
                        Name = "Dog",
                    },

                    new Pet
                    {
                        Name = "Bird",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
