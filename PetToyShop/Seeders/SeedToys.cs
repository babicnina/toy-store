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
    public class SeedToys
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PetToyShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PetToyShopContext>>()))
            {
                // Look for any movies.
                if (context.Toy.Any())
                {
                    return;   // DB has been seeded
                }

                context.Toy.AddRange(
                    new Toy
                    {
                        Name = "toy1",
                        Price = 7.99M
                    },

                    new Toy
                    {
                        Name = "toy2",
                        Price = 7.99M
                    },

                    new Toy
                    {
                        Name = "toy3",
                        Price = 7.99M
                    },

                    new Toy
                    {
                        Name = "toy4",
                        Price = 7.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
