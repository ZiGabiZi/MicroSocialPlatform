using Microsoft.AspNetCore.Mvc;
using FinalDAW2.Models;
using System.Threading.Tasks;
using FinalDAW2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalDAW2.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            var users = from user in db.Users
                        orderby user.UserName
                        select user;

            ViewBag.UsersList = users;

            return View();
        }

        public async Task<ActionResult> Show(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            return View(user);
        }


        // Implementează acțiunile Index, Show, Edit și Delete așa cum ai nevoie pentru afișare și editare
        // Nu include logica pentru rolurile utilizatorilor, doar pentru editarea proprietății IsProfilePublic

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = db.Users.Find(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser newData)
        {
            ApplicationUser user = db.Users.Find(id);
            if (ModelState.IsValid)
            {
                user.UserName = newData.UserName;
                user.IsProfilePublic = newData.IsProfilePublic;
                user.Email = newData.Email;
                user.FirstName = newData.FirstName;
                user.LastName = newData.LastName;
                user.PhoneNumber = newData.PhoneNumber;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var user = db.Users
                         .Include("Posts")
                         .Include("Comments")
                         .Where(u => u.Id == id)
                         .First();

            // Delete user comments
            if (user.Comments.Count > 0)
            {
                foreach (var comment in user.Comments)
                {
                    db.Comments.Remove(comment);
                }
            }



            // Delete user articles
            if (user.Posts.Count > 0)
            {
                foreach (var article in user.Posts)
                {
                    db.Posts.Remove(article);
                }
            }

            db.ApplicationUsers.Remove(user);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}