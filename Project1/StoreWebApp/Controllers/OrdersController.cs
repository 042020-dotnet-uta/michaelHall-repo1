using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreWebApp.Business_Logic;
using StoreWebApp.Data;
using StoreWebApp.Data.Repositories;
using StoreWebApp.Models;
using Microsoft.Extensions.Logging;

namespace StoreWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly StoreAppContext _context;
        private readonly ILogger<OrdersController> _logger;

        /// <summary>
        /// Constructor for setting up the DB context and logger within the controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public OrdersController(StoreAppContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Orders
        /// <summary>
        /// Default page for Orders controller which returns a view containing
        /// order data in a table
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var repo = new OrderRepo();
                var orderData = await repo.GetOrderData(_context);

                return View(orderData);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to get the order data");
                return RedirectToAction("Index", new { area = "Home" });
            }
        }

        // GET: Orders/Details/5
        /// <summary>
        /// Gets the order details from a specific order using
        /// the given id and returns the view displaying its info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var repo = new OrderRepo();
                var order = await repo.GetOrderDetails(_context, (int)id);

                if (order == null)
                {
                    return NotFound();
                }

                return View(order);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to get the order data");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Orders/Create
        /// <summary>
        /// Gets the initial create order view with empty fields
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            try
            {
                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "UserName");

                var repo = new OrderRepo();
                ViewData["ProductInfo"] = repo.ProductList(_context);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to create the order viewBags");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Validates all the inputted order data and maps it into database while
        /// returning the proper view
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CustomerId,Quantity,Timestamp")] Order order)
        {
            var check = new OrderLogic();
            var products = new ProductRepo();
            
            if (ModelState.IsValid && check.IsWithinInventory(products.GetInventory(_context, order.ProductId), order.Quantity))
            {
                try
                {
                    products.UpdateInventory(_context, order.ProductId, order.Quantity);
                    _context.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Something in the order wasn't able to be added");
                }
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "UserName");

            var repo = new OrderRepo();
            ViewData["ProductInfo"] = repo.ProductList(_context);
            return View(order);
        }

        public IActionResult CreateAdd(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            { 
                var repo = new OrderRepo();
                ViewData["ProductInfo"] = repo.ProductList(_context);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to create the order viewBags");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdd([Bind("Id,ProductId,CustomerId,Quantity,Timestamp")] Order order)
        {
            var check = new OrderLogic();
            var products = new ProductRepo();

            if (ModelState.IsValid && check.IsWithinInventory(products.GetInventory(_context, order.ProductId), order.Quantity))
            {
                try
                {
                    products.UpdateInventory(_context, order.ProductId, order.Quantity);
                    _context.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Something in the order wasn't able to be added");
                }
            }
            var repo = new OrderRepo();
            ViewData["ProductInfo"] = repo.ProductList(_context);
            return View(order);
        }

        /*
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", order.ProductId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,CustomerId,Quantity,Timestamp")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", order.ProductId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
        */
    }
}
