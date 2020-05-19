using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreWebApp.Models;

namespace StoreWebApp.Data
{
    public class StoreAppContext : DbContext
    {
        //public StoreAppContext() { }

        public StoreAppContext (DbContextOptions<StoreAppContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
