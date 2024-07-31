namespace fique_conectado_backend.DTO
{
    public class EntertainmentDTO
    {
        public record Add(Guid id, string ApiId, string name, string posterPath,string release, string type);
    }
}
