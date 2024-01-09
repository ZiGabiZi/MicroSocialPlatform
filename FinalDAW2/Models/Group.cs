using FinalDAW2.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalDAW2.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }
        [Required(ErrorMessage = "Continutul articolului este obligatoriu")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Continutul articolului trebuie să aibă între 5 și 200 de caractere.")]
        public string? Descriere { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual ApplicationUser? User { get; set; }

        [Required(ErrorMessage = "Grupul trebuie sa aibe un nume")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Continutul articolului trebuie să aibă între 2 și 30 de caractere.")]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUserGroup>? ApplicationUserGroups{ get; set; }


        public virtual ICollection<Post>? Posts { get; set; }
    }
}
