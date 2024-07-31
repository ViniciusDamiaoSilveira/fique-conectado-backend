namespace fique_conectado_backend.DTO
{
    public class UserDTO
    {
        public record Login(string username, string password);
        public record Register(string username, string password, string email, string phone);

    }
}
