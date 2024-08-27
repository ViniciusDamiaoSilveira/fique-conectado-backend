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
        public async Task<IActionResult> AddRating([FromBody] Rating obj)
        {
            if (obj == null) return BadRequest();
            if (!CheckUserIdAsync(obj.UserId)) return BadRequest(new { Message = "Usuário não existe" });
            if (CheckRatingAsync(obj.UserId, obj.EntertainmentId)) return BadRequest(new { Message = "Usuário já adicionou uma nota" });

            await _context.Ratings.AddAsync(obj);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Nota adicionada" });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyRating([FromBody] RatingDTO.Verify obj)
        {
            var verify = await _context.Ratings.Where(rating => rating.UserId == obj.userId && rating.EntertainmentId == obj.entertainmentId).ToListAsync();

            if (verify.Count > 0)
            {
                return Ok(verify[0]);
            } else
            {
                return BadRequest();
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRatings([FromRoute] Guid userId)
        {
           if (!CheckUserIdAsync(userId)) return BadRequest(new { Message = "Usuário não existe" });

            var ratings = await _context.Ratings.Where(rating => rating.UserId == userId).ToListAsync();
            return Ok(ratings);
        }

        [HttpPut]
        public async Task<IActionResult> editRating([FromBody] RatingDTO.Put obj)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(ratings => ratings.Id == obj.ratingId);

            if (rating == null) return BadRequest(new { Message = "Não existe essa crítica" });

            rating.NumRating = obj.numRating;
            rating.Comment = obj.comment;
            rating.Date = obj.date;

            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Crítica alterada" });
        }

        private bool CheckUserIdAsync(Guid userId)
            => _context.Users.Where(user => user.Id == userId).ToListAsync().Result.Count > 0 ? true : false;

        private bool CheckRatingAsync(Guid userId, string entertainmentId)
            => _context.Ratings.Where(rating => rating.UserId == userId && rating.EntertainmentId == entertainmentId).ToListAsync().Result
            .Count > 0 ? true : false;

    }
}
