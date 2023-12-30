using Microsoft.AspNetCore.Identity;
using FinalDAW2.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalDAW2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsProfilePublic { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public List<ApplicationUser>? FriendList { get; set; } = new List<ApplicationUser>();

        // un user poate posta mai multe comentarii
        public virtual ICollection<Comment>? Comments { get; set; }

        // un user poate posta mai multe articole
        public virtual ICollection<Post>? Posts { get; set; }

        // un user poate fi in  mai multe articole
        public virtual ICollection<Group>? Groups { get; set; }

        // un user poate posta mai multe mesaje

        ///[NotMapped]
        ///public IEnumerable<SelectListItem>? AllRoles { get; set; }
    }
}
