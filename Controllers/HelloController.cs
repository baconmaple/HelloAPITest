using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloApi.Models;

namespace HelloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly HelloContext _context;

        public HelloController(HelloContext context)
        {
            _context = context;
        }

        // GET: api/Hello
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HelloItem>>> GetHelloItems()
        {
            return await _context.HelloItems.ToListAsync();
        }

        // GET: api/Hello/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HelloItem>> GetHelloItem([FromHeader]long id)
        {
            var helloItem = await _context.HelloItems.FindAsync(id);

            if (helloItem == null)
            {
                return NotFound();
            }

            return helloItem;
        }

        // PUT: api/Hello/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelloItem([FromHeader]long id, [FromBody]HelloItem helloItem)
        {
            if (id != helloItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(helloItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelloItemExists(id))
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

        // POST: api/Hello
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HelloItem>> PostHelloItem(HelloItem helloItem)
        {
            _context.HelloItems.Add(helloItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHelloItem), new { id = helloItem.Id }, helloItem);
        }

        // DELETE: api/Hello/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HelloItem>> DeleteHelloItem([FromHeader]long id)
        {
            var helloItem = await _context.HelloItems.FindAsync(id);
            if (helloItem == null)
            {
                return NotFound();
            }

            _context.HelloItems.Remove(helloItem);
            await _context.SaveChangesAsync();

            return helloItem;
        }

        private bool HelloItemExists(long id)
        {
            return _context.HelloItems.Any(e => e.Id == id);
        }
    }
}
