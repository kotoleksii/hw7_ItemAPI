using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemAPI.Models;

namespace ItemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemContext _context;

        public ItemsController(ItemContext context)
        {
            _context = context;

            if (!_context.Items.Any())
            {
                _context.Items.Add(new Item { Title = "test1", Description = "test1", Price = 100, Quantity = 3, ImageUrl = "https://g.com/img1.png", Seller = "Amazon" });
                _context.Items.Add(new Item { Title = "test2", Description = "test2", Price = 200, Quantity = 2, ImageUrl = "https://g.com/img2.png", Seller = "Walmart" });
                _context.Items.Add(new Item { Title = "test3", Description = "test3", Price = 300, Quantity = 1, ImageUrl = "https://g.com/img3.png", Seller = "Żabka" });
                _context.SaveChanges();
            }
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            Item item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        // PUT: api/Items/
        [HttpPut]
        public async Task<ActionResult<Item>> Put(Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (!_context.Items.Any(x => x.Id == item.Id))
            {
                return NotFound();
            }

            _context.Update(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult<Item>> Post(Item item)
        {
            if (item.Quantity < 1 || item.Quantity > 49)
                ModelState.AddModelError("Quantity", "Min Quantity - 1, Max Quantity - 50");

            if (item.Title == "title")
                ModelState.AddModelError("Title", "This title is not supported");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Item item = _context.Items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
    }
}
