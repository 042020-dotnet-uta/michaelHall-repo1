using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreWebApp.Models;

namespace StoreWebApp.Data
{
    public class SeedData
    {
        /// <summary>
        /// Seeds the database if there is no data already within it
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StoreAppContext>>()))
            {
                // look for any product/store in the DB
                if (context.Products.Any() || context.Stores.Any())
                {
                    return; // DB already has something
                }

                context.Stores.AddRange(
                    new Store
                    {
                        Location = "New York"
                    },
                    new Store
                    {
                        Location = "Harrisburg",
                    },
                    new Store
                    {
                        Location = "Austin"
                    }
                );

                context.SaveChanges();

                context.Products.AddRange(
                    new Product
                    {
                        ProductName = "Shampoo",
                        StoreId = 1,
                        Inventory = 30,
                        Price = 6.50M
                    },
                    new Product
                    {
                        ProductName = "Conditioner",
                        StoreId = 1,
                        Inventory = 20,
                        Price = 5.00M
                    },
                    new Product
                    {
                        ProductName = "Soap",
                        StoreId = 1,
                        Inventory = 40,
                        Price = 4.00M
                    },
                    new Product
                    {
                        ProductName = "Shampoo",
                        StoreId = 2,
                        Inventory = 60,
                        Price = 5.00M
                    },
                    new Product
                    {
                        ProductName = "Conditioner",
                        StoreId = 2,
                        Inventory = 40,
                        Price = 4.00M
                    },
                    new Product
                    {
                        ProductName = "Soap",
                        StoreId = 2,
                        Inventory = 20,
                        Price = 3.00M
                    },
                    new Product
                    {
                        ProductName = "Shampoo",
                        StoreId = 3,
                        Inventory = 30,
                        Price = 4.00M
                    },
                    new Product
                    {
                        ProductName = "Conditioner",
                        StoreId = 3,
                        Inventory = 30,
                        Price = 4.00M
                    },
                    new Product
                    {
                        ProductName = "Soap",
                        StoreId = 3,
                        Inventory = 60,
                        Price = 2.00M
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
