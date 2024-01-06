using FinalDAW2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalDAW2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Friend> Friends { get; set; }

        // UPDATE 30.12.2023 CREARE RELATIE MANY TO MANY intre grup si user 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // definire primary key compus
            modelBuilder.Entity<ApplicationUserGroup>()
            .HasKey(ab => new
            {
                ab.Id,
                ab.UserId,
                ab.GroupId
            });

            // definire relatii cu modelele Group si ApplicationUser (FK)
            modelBuilder.Entity<ApplicationUserGroup>()
            .HasOne(ab => ab.User)
            .WithMany(ab => ab.ApplicationUserGroups)
            .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<ApplicationUserGroup>()
            .HasOne(ab => ab.Group)
            .WithMany(ab => ab.ApplicationUserGroups)
            .HasForeignKey(ab => ab.GroupId);
        }
    }
}
