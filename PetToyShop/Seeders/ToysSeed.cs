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
    public class ToysSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PetToyShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PetToyShopContext>>()))
            {
                // Look for any toys.
                if (context.Toy.Any())
                {
                    return;   // DB has been seeded
                }

                context.Toy.AddRange(
                    new Toy
                    {
                        Name = "Rope Whale",
                        Price = 7.99M,
                        Pet = (from p in context.Pet select p).First()
                    },

                    new Toy
                    {
                        Name = "Ball Knot Rope",
                        Price = 7.99M,
                        Pet = (from p in context.Pet select p).First()
                    },

                    new Toy
                    {
                        Name = "Floppy Knots Fox",
                        Price = 7.99M,
                        Pet = (from p in context.Pet select p).First()
                    },

                    new Toy
                    {
                        Name = "Squeaking Alligator",
                        Price = 7.99M,
                        Pet = (from p in context.Pet select p).First()
                    },

                     new Toy
                     {
                         Name = "Wishbone Bacon Flavor",
                         Price = 7.99M,
                         Pet = (from p in context.Pet select p).First()
                     },
                      new Toy
                      {
                          Name = "Knots Bear",
                          Price = 7.99M,
                          Pet = (from p in context.Pet select p).First()
                      },
                       new Toy
                       {
                           Name = "Squeezz Crackle Bone",
                           Price = 7.99M,
                           Pet = (from p in context.Pet select p).First()
                       },
                        new Toy
                        {
                            Name = "Dynos T-Rex",
                            Price = 7.99M,
                            Pet = (from p in context.Pet select p).First()
                        },
                         new Toy
                         {
                             Name = "Coloful Springs",
                             Price = 7.99M,
                             Pet = (from p in context.Pet select p).First()
                         },
                          new Toy
                          {
                              Name = "Mice",
                              Price = 7.99M,
                              Pet = (from p in context.Pet select p).First()
                          },
                           new Toy
                           {
                               Name = "Laser",
                               Price = 7.99M,
                               Pet = (from p in context.Pet select p).First()
                           },
                            new Toy
                            {
                                Name = "Sponge Soccer Ball",
                                Price = 7.99M,
                                Pet = (from p in context.Pet select p).First()
                            }
                );
                context.SaveChanges();
            }
        }
    }
}
