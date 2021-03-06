﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; }
        public int Inventory { get; set; }

        [Display(Name = "Price")]
        [Column(TypeName ="decimal(18, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
