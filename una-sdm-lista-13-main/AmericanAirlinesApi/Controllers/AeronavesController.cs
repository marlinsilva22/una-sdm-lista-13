using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Data;
using AmericanAirlinesApi.Models;

namespace AmericanAirlinesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AeronavesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AeronavesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/aeronaves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aeronave>>> GetAeronaves()
        {
            return await _context.Aeronaves.ToListAsync();
        }

        // POST: api/aeronaves
        [HttpPost]
        public async Task<ActionResult<Aeronave>> CriarAeronave(Aeronave aeronave)
        {
            _context.Aeronaves.Add(aeronave);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAeronaves), new { id = aeronave.Id }, aeronave);
        }
    }
}