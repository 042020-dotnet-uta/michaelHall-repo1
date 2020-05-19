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
        public IQueryable<Order> GetOrders(StoreAppContext context)
        {
            return from o in context.Orders
                   select o;
        }

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
