using fique_conectado_backend.Context;
using fique_conectado_backend.DTO;
using fique_conectado_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fique_conectado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListEntertainmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListEntertainmentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }


        [HttpPost]
        public async Task<IActionResult> AddEntertainmentToList([FromBody] ListEntertainmentDTO.Add obj)
        {
            if (obj == null) return BadRequest();

            if (!CheckEntertainmentExistsAsync(obj.entertainmentId, obj.type)) return BadRequest(new { Message = "Entretenimento não existe"});
            if (!CheckListExistsAsync(obj.listId)) return BadRequest(new { Message = "Lista não existe" });

            ListEntertainment listEntertainment = new ListEntertainment(Guid.NewGuid(), obj.listId, obj.entertainmentId);

            await _context.ListEntertainments.AddAsync(listEntertainment);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Entretenimento adicionado"});
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveEntertainmentFromList([FromBody] ListEntertainmentDTO.Remove obj)
        {
            if (obj == null) return BadRequest();

            if (!CheckEntertainmentExistsAsync(obj.entertainmentId, obj.type)) return BadRequest(new { Message = "Entretenimento não existe" });
            if (!CheckListExistsAsync(obj.listId)) return BadRequest(new { Message = "Lista não existe" });

            ListEntertainment listEntertainment = new ListEntertainment(Guid.NewGuid(), obj.listId, obj.entertainmentId);

            _context.Remove(listEntertainment);
            _context.SaveChanges();

            return Ok(new { Message = "Entretenimento removido"});

        }

        private bool CheckEntertainmentExistsAsync(string entertainmentId, string type)
            => _context.Entertainments.Where(entertainment => entertainment.ApiId == entertainmentId && entertainment.Type == type).ToListAsync().Result.Count > 0 ? false : true;
        private bool CheckListExistsAsync(Guid listId)
            => _context.Lists.Where(list => list.Id == listId).ToListAsync().Result.Count > 0 ? false : true;

    }
}
