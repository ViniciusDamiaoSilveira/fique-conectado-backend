namespace fique_conectado_backend.DTO
{
    public class ListEntertainmentDTO
    {
        public record Add(Guid listId, string entertainmentId, string type);
        public record Remove(Guid listId, string entertainmentId, string type);

    }
}
