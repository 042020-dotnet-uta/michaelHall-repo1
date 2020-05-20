using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreWebApp.Data;
using StoreWebApp.Data.Repositories;
using StoreWebApp.Models;

namespace StoreWebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly StoreAppContext _context;
        private readonly ILogger<CustomersController> _logger;

        /// <summary>
        /// Constructor for setting up the DB context and logger within the controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public CustomersController(StoreAppContext context, ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Default page for customers that returns the index view and 
        /// is able to search through the customers by name/username
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string firstName, string lastName, string userName)
        {
            var query = new CustomerRepo();
            try
            {
                var customers = query.GetCustomers(_context);

                if (!String.IsNullOrEmpty(firstName))
                {
                    customers = query.SearchFirstName(customers, firstName);
                }
                if (!String.IsNullOrEmpty(lastName))
                {
                    customers = query.SearchLastName(customers, lastName);
                }
                if (!String.IsNullOrEmpty(userName))
                {
                    customers = query.SearchUserName(customers, userName);
                }

                return View(await customers.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to search through customers.");
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Get method for creating a new customer
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Post method for creating a new customer, checks for validation and antiforgery token
        /// redirects to the index once the customer is made
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserName,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    _logger.LogInformation(ex, "Wasn't able to create the customer.");
                }   
            }
            return View(customer);
        }

        // GET: Customers/Details/5
        /// <summary>
        /// Goes to the order history page for a specific customer and
        /// returns to the index if anything goes wrong
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> History(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repo = new CustomerRepo();

            try
            {
                var customerHistory = await repo.GetCustomerHistory(_context, (int)id);
                return View(customerHistory);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to get the customer history.");
            }

            return RedirectToAction(nameof(Index));
        }

        /*
        // GET: Customers
        public async Task<IActionResult> Search(string firstName, string lastName, string userName)
        {
            var query = new CustomerRepo();
            try
            {
                var customers = query.GetCustomers(_context);

                if (!String.IsNullOrEmpty(firstName))
                {
                    customers = query.SearchFirstName(customers, firstName);
                }
                if (!String.IsNullOrEmpty(lastName))
                {
                    customers = query.SearchLastName(customers, lastName);
                }
                if (!String.IsNullOrEmpty(userName))
                {
                    customers = query.SearchUserName(customers, userName);
                }

                return View(await customers.ToListAsync());
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to search through customers.");
            }

            return RedirectToAction(nameof(Search));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,UserName,Password")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        */
    }
}
