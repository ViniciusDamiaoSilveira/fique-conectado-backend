using fique_conectado_backend.Context;
using fique_conectado_backend.DTO;
using fique_conectado_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fique_conectado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserLists([FromRoute] string userId)
        {
            var lists_duplicates = await _context.Lists.Where(list => list.UserId == Guid.Parse(userId)).ToListAsync();
            var lists_response = lists_duplicates.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            return Ok(lists_response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListDTO.Create list_obj)
        {
            if (list_obj == null) return BadRequest();

            if (!CheckUserIdAsync(list_obj.userId)) return BadRequest(new { Message = "O usuário não existe" });

            if (!CheckNameExistsAsync(list_obj.name, list_obj.userId.ToString())) return BadRequest(new { Message = "A lista já existe" });

            List list = new List(Guid.NewGuid(), list_obj.name, list_obj.typeList, Guid.Parse(list_obj.userId));

            await _context.Lists.AddAsync(list);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Lista criada com sucesso" });
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList([FromRoute] string id)
        {
            if (id == null) return BadRequest();

            if (!CheckListAsync(Guid.Parse(id))) return BadRequest(new { Message = "A lista não existe" });

            List lista = await _context.Lists.Where(list => list.Id == Guid.Parse(id)).SingleOrDefaultAsync();
            _context.Remove(lista);
            _context.SaveChanges();

            return Ok(new { Message = "Lista removida" });


        }

        private bool CheckNameExistsAsync(string name, string userId)
            => _context.Lists.Where(list => list.Name == name && list.UserId == Guid.Parse(userId)).ToListAsync().Result.Count > 0 ? false : true;

        private bool CheckUserIdAsync(string userId)
            => _context.Users.Where(user => user.Id == Guid.Parse(userId)).ToListAsync().Result.Count > 0 ? true : false;

        private bool CheckListAsync(Guid id)
            => _context.Lists.Where(list => list.Id == id).ToListAsync().Result.Count > 0 ? true : false;

    }
}
