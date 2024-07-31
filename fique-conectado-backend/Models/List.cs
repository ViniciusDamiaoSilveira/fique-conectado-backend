using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fique_conectado_backend.Models
{
    public class List
    {        

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string TypeList { get; set; }

        [ForeignKey("UserId")]
        public virtual Guid UserId { get; set; }

        public virtual ICollection<ListEntertainment> ListEntertainments { get; set; }

        public List(Guid id, string name, string typeList, Guid userId)
        {
            Id = id;
            Name = name;
            TypeList = typeList;
            UserId = userId;
        }


    }
}
