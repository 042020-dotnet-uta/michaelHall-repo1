using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StoreWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Product")]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Product(s)")]
        public Product Product { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be a positive number")]
        [Required]
        public int Quantity { get; set; }

        private DateTime timestamp = DateTime.Now;
        [Display(Name = "Date/Time")]
        public DateTime Timestamp
        {
            get { return timestamp; }
            set {; }
        }
    }
}
