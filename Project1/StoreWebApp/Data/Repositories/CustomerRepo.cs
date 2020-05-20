using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreWebApp.Data.Repositories
{
    public interface ICustomerRepo
    {
        public IQueryable<Customer> GetCustomers(StoreAppContext context);
        public IQueryable<Customer> SearchFirstName(IQueryable<Customer> context, string name);
        public IQueryable<Customer> SearchLastName(IQueryable<Customer> context, string name);
        public IQueryable<Customer> SearchUserName(IQueryable<Customer> context, string name);
        public Task<IEnumerable<Order>> GetCustomerHistory(StoreAppContext context, int id);
    }

    public class CustomerRepo : ICustomerRepo
    {
        public IQueryable<Customer> GetCustomers(StoreAppContext context)
        {
            return from c in context.Customers
                   select c;
        }

        public IQueryable<Customer> SearchFirstName(IQueryable<Customer> context, string name)
        {
            return context.Where(s => s.FirstName.Contains(name));
        }

        public IQueryable<Customer> SearchLastName(IQueryable<Customer> context, string name)
        {
            return context.Where(s => s.LastName.Contains(name));
        }

        public IQueryable<Customer> SearchUserName(IQueryable<Customer> context, string name)
        {
            return context.Where(s => s.UserName.Contains(name));
        }

        public async Task<IEnumerable<Order>> GetCustomerHistory(StoreAppContext context, int id)
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
