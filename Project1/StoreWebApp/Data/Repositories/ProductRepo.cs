using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreWebApp.Data.Repositories
{
    public interface IProductRepo
    {
        public IQueryable<Product> GetProducts(StoreAppContext context);
    }

    public class ProductRepo : IProductRepo
    {
        public IQueryable<Product> GetProducts(StoreAppContext context)
        {
            return from p in context.Products
                   select p;
        }
    }
}
