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

        public CustomersController(StoreAppContext context, ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string firstName, string lastName, string userName)
        {
            var customers = from c in _context.Customers
                            select c;

            if (!String.IsNullOrEmpty(firstName))
            {
                customers = customers.Where(s => s.FirstName.Contains(firstName));
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                customers = customers.Where(s => s.FirstName.Contains(lastName));
            }
            if (!String.IsNullOrEmpty(userName))
            {
                customers = customers.Where(s => s.FirstName.Contains(userName));
            }

            return View(await customers.ToListAsync());
            //return View(await _context.Customers.ToListAsync());
        }

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

        // GET: Customers/Details/5
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
            catch(Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to get the customer history.");
            }

            return RedirectToAction(nameof(Search));
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserName,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
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
    }
}
