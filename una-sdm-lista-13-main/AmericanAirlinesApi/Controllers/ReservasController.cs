using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Data;
using AmericanAirlinesApi.Models;

namespace AmericanAirlinesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/reservas
        [HttpPost]
        public async Task<ActionResult> CriarReserva(Reserva reserva)
        {
            var voo = await _context.Voos
                .Include(v => v.Aeronave)
                .FirstOrDefaultAsync(v => v.Id == reserva.VooId);

            if (voo == null)
                return NotFound("Voo não encontrado.");

            // 🚨 Garantir que a aeronave existe
            if (voo.Aeronave == null)
            {
                return BadRequest("Aeronave não vinculada ao voo.");
            }

            // 🚨 OVERBOOKING
            var totalReservas = await _context.Reservas
                .CountAsync(r => r.VooId == reserva.VooId);

            if (totalReservas >= voo.Aeronave.CapacidadePassageiros)
            {
                return BadRequest("Voo lotado. Não é possível realizar novas reservas.");
            }

            // 🪟 ASSENTO JANELA
            if (reserva.Assento != null &&
                (reserva.Assento.EndsWith("A") || reserva.Assento.EndsWith("F")))
            {
                Console.WriteLine("Assento na janela reservado com sucesso! (+$50)");
            }

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return Ok(reserva);
        }
    }
}