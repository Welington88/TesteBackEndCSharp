using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Models;

namespace TesteBackEndCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly DataContext _context;

        public MoneyController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Money
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Money>>> GetMoney()
        {
          if (_context.Money == null)
          {
              return NotFound();
          }
            return await _context.Money.ToListAsync();
        }

        // GET: api/Money/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Money>> GetMoney(int id)
        {
          if (_context.Money == null)
          {
              return NotFound();
          }
            var money = await _context.Money.FindAsync(id);

            if (money == null)
            {
                return NotFound();
            }

            return money;
        }

        // PUT: api/Money/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoney(int id, Money money)
        {
            if (id != money.id)
            {
                return BadRequest();
            }

            _context.Entry(money).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoneyExists(id))
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

        // POST: api/Money
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Money>> PostMoney(Money money)
        {
          if (_context.Money == null)
          {
              return Problem("Entity set 'DataContext.Money'  is null.");
          }
            _context.Money.Add(money);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoney", new { id = money.id }, money);
        }

        // DELETE: api/Money/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoney(int id)
        {
            if (_context.Money == null)
            {
                return NotFound();
            }
            var money = await _context.Money.FindAsync(id);
            if (money == null)
            {
                return NotFound();
            }

            _context.Money.Remove(money);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoneyExists(int id)
        {
            return (_context.Money?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
