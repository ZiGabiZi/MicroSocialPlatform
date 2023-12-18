using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FinalDAW2.Models;

namespace FinalDAW2.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        [Required(ErrorMessage = "Continutul mesajului este obligatoriu")]
        public string Content { get; set; }
        public DateTime DataMesaj { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }

        [Required(ErrorMessage = "GRUPUL este obligatorie")]
        public virtual Group? Group { get; set; }

    }
}
