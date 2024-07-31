namespace fique_conectado_backend.DTO
{
    public class RatingDTO
    {
        public record Add(Guid userId, string entertainmentId, float numRating, string? comment);
    }
}
