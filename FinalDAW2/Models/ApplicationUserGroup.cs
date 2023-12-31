using System.ComponentModel.DataAnnotations.Schema;

namespace FinalDAW2.Models
{
    public class ApplicationUserGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? UserId { get; set; }
        public int? GroupId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual Group? Group { get; set; }
    }
}
