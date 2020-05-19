using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Quantity { get; set; }
        private DateTime timestamp;
        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = DateTime.Now; }
        }
    }
}
