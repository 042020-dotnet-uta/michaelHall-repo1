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
    }

    public class StoreRepo : IStoreRepo
    {
        public IQueryable<Store> GetStores(StoreAppContext context)
        {
            return from s in context.Stores
                   select s;
        }
    }
}
