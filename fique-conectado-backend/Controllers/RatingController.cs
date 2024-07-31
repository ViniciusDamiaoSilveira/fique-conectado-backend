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
    public class RatingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RatingController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingDTO.Add obj)
        {
            if (obj == null) return BadRequest();
            if (!CheckUserIdAsync(obj.userId)) return BadRequest(new { Message = "Usuário não existe" });
            if (CheckRatingAsync(obj.userId, obj.entertainmentId)) return BadRequest(new { Message = "Usuário já adicionou uma nota" });

            Rating rating = new Rating(Guid.NewGuid(), obj.userId, obj.entertainmentId, obj.numRating, obj.comment, DateTime.Now);

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Nota adicionada" });

        }

        private bool CheckUserIdAsync(Guid userId)
            => _context.Users.Where(user => user.Id == userId).ToListAsync().Result.Count > 0 ? true : false;

        private bool CheckRatingAsync(Guid userId, string entertainmentId)
            => _context.Ratings.Where(rating => rating.UserId == userId && rating.EntertainmentId == entertainmentId).ToListAsync().Result
            .Count > 0 ? true : false;

    }
}
