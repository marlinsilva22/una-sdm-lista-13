using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Data;
using AmericanAirlinesApi.Models;

namespace AmericanAirlinesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VoosController(AppDbContext context)
        {
            _context = context;
        }

        // 🔍 GET: api/voos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voo>>> GetVoos()
        {
            return await _context.Voos
                .Include(v => v.Aeronave)
                .ToListAsync();
        }

        // 🚀 POST: api/voos
        [HttpPost]
        public async Task<ActionResult<Voo>> CriarVoo(Voo voo)
        {
            // ✅ 1. Verificar se a aeronave existe
            var aeronave = await _context.Aeronaves
                .FirstOrDefaultAsync(a => a.Id == voo.AeronaveId);

            if (aeronave == null)
            {
                return BadRequest("Aeronave não encontrada.");
            }

            // ❌ 2. Verificar se já está em voo
            var aeronaveEmUso = await _context.Voos
                .AnyAsync(v => v.AeronaveId == voo.AeronaveId && v.Status == "Em Voo");

            if (aeronaveEmUso)
            {
                return Conflict("Aeronave indisponível, encontra-se em trânsito.");
            }

            _context.Voos.Add(voo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVoos), new { id = voo.Id }, voo);
        }

        // 🔄 PATCH: api/voos/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] string novoStatus)
        {
            var voo = await _context.Voos.FindAsync(id);

            if (voo == null)
                return NotFound("Voo não encontrado.");

            // 🚨 REGRA DE OURO
            if ((voo.Status == "Finalizado" || voo.Status == "Cancelado") 
                && novoStatus == "Em Voo")
            {
                return BadRequest("Não é possível alterar para 'Em Voo' após finalizado ou cancelado.");
            }

            voo.Status = novoStatus;

            await _context.SaveChangesAsync();

            return Ok(voo);
        }
    }
}