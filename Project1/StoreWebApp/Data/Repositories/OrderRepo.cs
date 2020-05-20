using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreWebApp.Data.Repositories
{
    public interface IOrderRepo
    {
        public IQueryable<Order> GetOrders(StoreAppContext context);
        public Task<Order> GetOrderDetails(StoreAppContext context, int id);
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
                .FirstOrDefaultAsync();
        }
    }
}
