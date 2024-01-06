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
        public bool IsCurrentUserFollowing { get; set; }
        public bool IsCurrentUserPending { get; set; }

        //Tabela cu lista de asteptare
        public virtual ICollection<Friend>? Friends { get; set; }


        // Un user poate posta mai multe comentarii
        public virtual ICollection<Comment>? Comments { get; set; }

        // Un user poate posta mai multe articole
        public virtual ICollection<Post>? Posts { get; set; }

        // Un user poate fi în mai multe grupuri
        public virtual ICollection<ApplicationUserGroup>? ApplicationUserGroups { get; set; } 

    }
}
