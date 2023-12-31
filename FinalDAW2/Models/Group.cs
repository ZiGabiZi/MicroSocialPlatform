using FinalDAW2.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalDAW2.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Grupul trebuie sa aibe un nume")]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUserGroup>? ApplicationUserGroups{ get; set; }


        public virtual ICollection<Post>? Posts { get; set; }
    }
}
