using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fique_conectado_backend.Models
{
    public class ListEntertainment
    {

        [Key]
        public Guid id { get; set; }

        [ForeignKey("ListId")]
        public virtual Guid ListId { get; set; }

        [ForeignKey("EntertainmentId")]
        public virtual string EntertainmentId { get; set; }

        public ListEntertainment(Guid id, Guid listId, string entertainmentId)
        {
            this.id = id;
            ListId = listId;
            EntertainmentId = entertainmentId;
        }
    }
}
