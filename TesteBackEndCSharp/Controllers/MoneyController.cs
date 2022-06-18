using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "", Description = "")]
        [SwaggerResponse(200, @"Consulta realizada com Sucesso.")]
        [SwaggerResponse(400, @"Não existem Fila a Serem processado.")]
        [SwaggerResponse(500, @"Erro servidor indisponível.")]
        public async Task<ActionResult<IEnumerable<Money>>> GetItemFila()
        {
            try
            {
                if (_context.Money == null)
                {
                    return NotFound();
                }
                var retorno = await _service.GetItemFila();

                return retorno;
            }
            catch (System.Exception ex)
            {
                return BadRequest("Não possui filas de consulta a serem processadas....");
            }

            
        }

        // POST: api/Money
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerResponse(200, @"Dados da consulta salvo Sucesso.")]
        [SwaggerResponse(400, @"Erro ao processar a solicitação.")]
        [SwaggerResponse(500, @"Erro servidor indisponível.")]
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
