using System.ComponentModel.DataAnnotations;

namespace fique_conectado_backend.Models
{
    public class Entertainment
    {
        [Key]
        public Guid Id { get; set; }
        public string ApiId { get; set; }
        public string Name { get; set; }
        public string Release { get; set; }
        public string PathPoster { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<ListEntertainment> ListEntertainments { get; set; }

        public Entertainment(Guid id, string apiId, string name, string release, string pathPoster, string type)
        {
            Id = id;
            ApiId = apiId;
            Name = name;
            Release = release;
            PathPoster = pathPoster;
            Type = type;
        }
    }
}
