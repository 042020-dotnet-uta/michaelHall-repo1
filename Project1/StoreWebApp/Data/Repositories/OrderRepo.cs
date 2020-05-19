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
    }

    public class OrderRepo : IOrderRepo
    {
        public IQueryable<Order> GetOrders(StoreAppContext context)
        {
            return from o in context.Orders
                   select o;
        }
    }
}
