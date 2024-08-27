using fique_conectado_backend.Context;
using fique_conectado_backend.DTO;
using fique_conectado_backend.Helpers;
using fique_conectado_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace fique_conectado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO.Login userLogin)
        {
            if (userLogin == null) return BadRequest();

            if (!await CheckUsernameExistsAsync(userLogin.username)) 
                return NotFound(new { Message = "O usuário não existe" });

            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Username == userLogin.username);


            if (!PasswordHasher.VerifyPassword(userLogin.password, user.Password))
                return BadRequest(new { Message = "Senha incorreta" });

            user.Token = CreateJwt(user);

            await _context.SaveChangesAsync();

            return Ok(new 
            { 
                Token = user.Token,
                Message = "Seja bem-vindo!" 
            });
            
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO.Register userRegister)
        {
            if (userRegister == null) return BadRequest();

            if (await CheckUsernameExistsAsync(userRegister.username)) return BadRequest(new { Message = "Nome de usuário já utilizado" });
            if (await CheckEmailExistsAsync(userRegister.email)) return BadRequest(new { Message = "E-mail já utilizado" });
            if (await CheckPhoneExistsAsync(userRegister.phone)) return BadRequest(new { Message = "Telefone já utilizado" });

            var pass = CheckUserPasswordsAsync(userRegister.password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass });

            var hashPassword = PasswordHasher.HashPassword(userRegister.password);


            User user = new User(Guid.NewGuid(), userRegister.username, hashPassword, userRegister.email, userRegister.phone, "User", "");

            List listFav1 = new List(Guid.NewGuid(), "Favoritos", "Movie", user.Id);
            List listFav2 = new List(Guid.NewGuid(), "Favoritos", "Serie", user.Id);
            List listFav3 = new List(Guid.NewGuid(), "Favoritos", "Game", user.Id);
            await _context.Lists.AddAsync(listFav1);
            await _context.Lists.AddAsync(listFav2);
            await _context.Lists.AddAsync(listFav3);

            List listSave1 = new List(Guid.NewGuid(), "Salvos", "Movie", user.Id);
            List listSave2 = new List(Guid.NewGuid(), "Salvos", "Serie", user.Id);
            List listSave3 = new List(Guid.NewGuid(), "Salvos", "Game", user.Id);
            await _context.Lists.AddAsync(listSave1);
            await _context.Lists.AddAsync(listSave2);
            await _context.Lists.AddAsync(listSave3);

            List listRated1 = new List(Guid.NewGuid(), "Classificados", "Movie", user.Id);
            List listRated2 = new List(Guid.NewGuid(), "Classificados", "Serie", user.Id);
            List listRated3 = new List(Guid.NewGuid(), "Classificados", "Game", user.Id);
            await _context.Lists.AddAsync(listRated1);
            await _context.Lists.AddAsync(listRated2);
            await _context.Lists.AddAsync(listRated3);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Usuário cadastrado" });
        }


        private Task<bool> CheckUsernameExistsAsync(string username)
            => _context.Users.AnyAsync(user => user.Username == username);
        private Task<bool> CheckEmailExistsAsync(string email)
            => _context.Users.AnyAsync(user => user.Email == email);
        private Task<bool> CheckPhoneExistsAsync(string phone)
            => _context.Users.AnyAsync(user => user.Phone == phone);
        private string CheckUserPasswordsAsync(string password)
        {
            StringBuilder sb = new StringBuilder();
            
            if (password.Length < 8) sb.Append("A senha deve conter pelo menos 8 caracteres");
            if (!(Regex.IsMatch(password, "[a-z]") && 
                Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[0-9]"))) 
                sb.Append("A senha deve conter números, letras e caracteres especiais");

            if (!Regex.IsMatch(password, "^[a-zA-Z0-9,. ]*$")) sb.Append("A senha deve conter pelo menos um caractere especial");

            return sb.ToString();

        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("b7800cd7-a125-4b02-8a32-1ad594ea3aef");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(type: "id", user.Id.ToString()),
                new Claim(type: "username", user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Type),
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
