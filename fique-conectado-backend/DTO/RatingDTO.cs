namespace fique_conectado_backend.DTO
{
    public class RatingDTO
    {
        public record Add(Guid userId, string entertainmentId, float numRating, string comment);
        public record Verify(Guid userId, string entertainmentId);
        public record Put(Guid ratingId, float numRating, string comment, string date);

    }
}
