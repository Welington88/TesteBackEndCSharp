using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var id = _context.Money.Max<Money>(m => m.id);
            return await _context.Money.Where(m => m.id == id).ToListAsync();
        }

        // POST: api/Money
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Money>>> AddItemFila(List<Money> money)
        {
          if (_context.Money == null)
          {
              return Problem("Entity set 'DataContext.Money'  is null.");
          }

            foreach (var m in money)
            {
                _context.Money.Add(m);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoney", new { id = money.ToList().FirstOrDefault() }, money);
        }
    }
}
