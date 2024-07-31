using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace fique_conectado_backend.Models
{
    public class User
    {

        [Key]
        public Guid Id { get; private set; }
        
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
           
        public string Phone { get; set; }

        public string? Token { get; set; }

        public string Type { get; set; }

        public virtual ICollection<List> Lists { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }


        public User(Guid id,string username, string password, string email, string phone, string type,string token)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
            Type = type;
            Token = "";
        }
    }
}
