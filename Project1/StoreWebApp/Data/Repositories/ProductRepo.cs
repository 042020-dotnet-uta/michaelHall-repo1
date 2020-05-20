using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace StoreWebApp.Data.Repositories
{
    public interface IProductRepo
    {
        public IQueryable<Product> GetProducts(StoreAppContext context);
        public int GetInventory(StoreAppContext context, int id);
        public void UpdateInventory(StoreAppContext context, int id, int quant);
        public Task<IEnumerable<Product>> GetProductData(StoreAppContext context);
    }

    public class ProductRepo : IProductRepo
    {
        public IQueryable<Product> GetProducts(StoreAppContext context)
        {
            return from p in context.Products
                   select p;
        }

        public int GetInventory(StoreAppContext context, int id)
        {
            return context.Products
                .Where(p => p.Id == id)
                .Select(p => p.Inventory)
                .SingleOrDefault();
        }

        public void UpdateInventory(StoreAppContext context, int id, int quant)
        {
            var product = context.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();
            product.Inventory -= quant;
            context.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetProductData(StoreAppContext context)
        {
            return await context.Products
                .Include(p => p.Store)
                .OrderBy(p => p.Store.Id)
                .ToListAsync();
        }
    }
}
