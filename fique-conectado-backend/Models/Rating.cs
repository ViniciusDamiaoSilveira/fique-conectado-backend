using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fique_conectado_backend.Models
{
    public class Rating
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public virtual Guid UserId { get; set; }

        [ForeignKey("EntertainmentId")]
        public virtual string EntertainmentId { get; set; }
        public float NumRating { get; set; }
        public string Comment { get; set; }
        public DateTime date { get; set; }

        public Rating(Guid id, Guid userId, string entertainmentId, float numRating, string comment, DateTime date)
        {
            Id = id;
            UserId = userId;
            EntertainmentId = entertainmentId;
            NumRating = numRating;
            Comment = comment;
            this.date = date;
        }


    }
}
