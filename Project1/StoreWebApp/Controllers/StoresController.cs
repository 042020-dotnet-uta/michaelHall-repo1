﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreWebApp.Data;
using StoreWebApp.Models;
using StoreWebApp.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace StoreWebApp.Controllers
{
    public class StoresController : Controller
    {
        private readonly StoreAppContext _context;
        private readonly ILogger<StoresController> _logger;

        /// <summary>
        /// Constructor for setting up the DB context and logger within the controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public StoresController(StoreAppContext context, ILogger<StoresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Stores
        /// <summary>
        /// Default page for Stores which returns the view for 
        /// listing out all the store and product info associated with it
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var repo = new ProductRepo();
                var storeData = await repo.GetProductData(_context);

                return View(storeData);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to get the product/store data");
                return RedirectToAction("Index", new { area = "Home" });
            }
        }

        // GET: Stores/History/5
        /// <summary>
        /// Gets all the order history for a particular store and returns
        /// the view displaying said history data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> History(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            try
            {
                var repo = new StoreRepo();
                var storeHistory = await repo.GetStoreHistory(_context, (int)id);

                return View(storeHistory);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Wasn't able to obtain the store's order history");
                return RedirectToAction(nameof(Index));
            }
        }

        /*
        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Location")] Store store)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
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
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
