using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
