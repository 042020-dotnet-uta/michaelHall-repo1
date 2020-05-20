using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemClassificationsController : ControllerBase
    {
        private readonly TodoContext _context;

        public ItemClassificationsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/ItemClassifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemClassification>>> GetItemClassifications()
        {
            return await _context.ItemClassifications.ToListAsync();
        }

        // GET: api/ItemClassifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemClassification>> GetItemClassification(long id)
        {
            var itemClassification = await _context.ItemClassifications.FindAsync(id);

            if (itemClassification == null)
            {
                return NotFound();
            }

            return itemClassification;
        }

        // PUT: api/ItemClassifications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemClassification(long id, ItemClassification itemClassification)
        {
            if (id != itemClassification.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemClassification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemClassificationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItemClassifications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemClassification>> PostItemClassification(ItemClassification itemClassification)
        {
            _context.ItemClassifications.Add(itemClassification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemClassification", new { id = itemClassification.Id }, itemClassification);
        }

        // DELETE: api/ItemClassifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemClassification>> DeleteItemClassification(long id)
        {
            var itemClassification = await _context.ItemClassifications.FindAsync(id);
            if (itemClassification == null)
            {
                return NotFound();
            }

            _context.ItemClassifications.Remove(itemClassification);
            await _context.SaveChangesAsync();

            return itemClassification;
        }

        private bool ItemClassificationExists(long id)
        {
            return _context.ItemClassifications.Any(e => e.Id == id);
        }
    }
}
