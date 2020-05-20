using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreWebApp.Data.Repositories
{
    public interface IStoreRepo
    {
        public IQueryable<Store> GetStores(StoreAppContext context);
        public Task<IEnumerable<Order>> GetStoreHistory(StoreAppContext context, int id);
    }

    public class StoreRepo : IStoreRepo
    {
        public IQueryable<Store> GetStores(StoreAppContext context)
        {
            return from s in context.Stores
                   select s;
        }

        public async Task<IEnumerable<Order>> GetStoreHistory(StoreAppContext context, int id)
        {
            return await context.Orders
                .Where(o => o.Customer.Id == id)
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .ThenInclude(p => p.Store)
                .OrderBy(o => o.Timestamp)
                .ToListAsync();
        }
    }
}
