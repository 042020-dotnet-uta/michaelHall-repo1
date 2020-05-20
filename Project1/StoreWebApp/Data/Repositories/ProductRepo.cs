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
        /// <summary>
        /// Get all the products in the DB
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IQueryable<Product> GetProducts(StoreAppContext context)
        {
            return from p in context.Products
                   select p;
        }

        /// <summary>
        /// Get the inventory for a particular product found
        /// using the incoming id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetInventory(StoreAppContext context, int id)
        {
            return context.Products
                .Where(p => p.Id == id)
                .Select(p => p.Inventory)
                .SingleOrDefault();
        }

        /// <summary>
        /// Updates the inventory for a particular product 
        /// based on the quantity that a recent order has purchased
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="quant"></param>
        public void UpdateInventory(StoreAppContext context, int id, int quant)
        {
            var product = context.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();
            product.Inventory -= quant;
            context.SaveChanges();
        }

        /// <summary>
        /// Get all the product/store data from the DB
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductData(StoreAppContext context)
        {
            return await context.Products
                .Include(p => p.Store)
                .OrderBy(p => p.Store.Id)
                .ToListAsync();
        }
    }
}
