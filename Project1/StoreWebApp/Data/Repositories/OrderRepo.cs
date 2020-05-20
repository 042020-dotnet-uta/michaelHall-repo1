using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreWebApp.Data.Repositories
{
    public interface IOrderRepo
    {
        public IQueryable<Order> GetOrders(StoreAppContext context);
        public Task<Order> GetOrderDetails(StoreAppContext context, int id);
        public Task<IEnumerable<Order>> GetOrderData(StoreAppContext context);
        public IEnumerable<SelectListItem> ProductList(StoreAppContext context);
    }

    public class OrderRepo : IOrderRepo
    {
        /// <summary>
        /// Get all the orders within the DB
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IQueryable<Order> GetOrders(StoreAppContext context)
        {
            return from o in context.Orders
                   select o;
        }

        /// <summary>
        /// Get all the order/customer/product data for a given order
        /// using the incoming id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Order> GetOrderDetails(StoreAppContext context, int id)
        {
            return await context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .ThenInclude(o => o.Store)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all the order data including foreign key data
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetOrderData(StoreAppContext context)
        {
            return await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .ThenInclude(p => p.Store)
                .OrderBy(o => o.Timestamp)
                .ToListAsync();
        }

        /// <summary>
        /// Creates a product list to store in the select input form
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> ProductList(StoreAppContext context)
        {
            List<SelectListItem> products = new List<SelectListItem>();


            foreach(var product in context.Products.Include(p => p.Store))
            {
                products.Add(new SelectListItem { Text = $"{product.Store.Location } | {product.ProductName} | Inventory: {product.Inventory} @ ${product.Price} each", Value = $"{product.Id}" });
            }

            return products;
        }
    }
}
