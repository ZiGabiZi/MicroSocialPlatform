using FinalDAW2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalDAW2.Models
{
    public class Post
    {
        [Key] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Continutul articolului este obligatoriu")]
        public string Continut { get; set; }


        public DateTime DataPostarii { get; set; }
        // Alte atribute relevante
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

        // Relație cu Utilizator

    }
}
