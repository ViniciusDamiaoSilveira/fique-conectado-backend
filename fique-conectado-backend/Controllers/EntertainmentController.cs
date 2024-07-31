using fique_conectado_backend.Context;
using fique_conectado_backend.DTO;
using fique_conectado_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fique_conectado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntertainmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntertainmentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddEntertainment([FromRoute] Entertainment obj)
        {
            if (obj == null) return BadRequest();

            await _context.Entertainments.AddAsync(obj);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Entretenimento adicionado"});
        }

    }
}
