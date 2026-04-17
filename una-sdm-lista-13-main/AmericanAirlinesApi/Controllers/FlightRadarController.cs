using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Data;

namespace AmericanAirlinesApi.Controllers
{
    [ApiController]
    [Route("api/radar")]
    public class FlightRadarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FlightRadarController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/radar/proximos-destinos
        [HttpGet("proximos-destinos")]
        public async Task<ActionResult> GetProximosDestinos()
        {
            // ⏳ Simulação de latência (2 segundos)
            Thread.Sleep(2000);

            // ✈️ Agrupar voos ativos por destino
            var resultado = await _context.Voos
                .Where(v => v.Status == "Agendado" || v.Status == "Em Voo")
                .GroupBy(v => v.Destino)
                .Select(g => new
                {
                    Destino = g.Key,
                    QuantidadeVoos = g.Count()
                })
                .ToListAsync();

            return Ok(resultado);
        }
    }
}