using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Models;
using TesteBackEndCSharp.Service;

namespace TesteBackEndCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly MoneyService _service;

        public MoneyController(DataContext context)
        {
            _context = context;
            _service = new MoneyService(_context);
        }

        // GET: api/Money
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Money>>> GetItemFila()
        {
            if (_context.Money == null)
            {
                return NotFound();
            }

            return await _service.GetItemFila();
        }

        // POST: api/Money
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Money>>> AddItemFila(List<Money> money)
        {

            var result = await _service.AddItemFila(money);

            if (result)
            {
                return CreatedAtAction("GetItemFila", new { id = money.ToList().FirstOrDefault() }, money);
            }
            else
            {
                return Problem("Entity set 'DataContext.Money'  is null.");
            }     
        }
    }
}
