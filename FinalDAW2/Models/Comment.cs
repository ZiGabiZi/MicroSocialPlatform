using FinalDAW2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectDAW.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Continutul comentariului este obligatoriu")]
        public string Content { get; set; }

        public DateTime DataComentariu { get; set; }

        // un comentariu apartine postari
        public int? PostId { get; set; }
        // un comentariu este postat de catre un user

        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        //useri si roluri
        public virtual Post? Post { get; set; }


        // Navigation properties???

    }

}
