using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWebApp.Models
{
    public class Customer
    {
        [Display(Name = "Customer ID")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [RegularExpression(@"^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])*$")]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
        public ICollection<Order> Orders;
    }
}
