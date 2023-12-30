using Microsoft.AspNetCore.Mvc;
using FinalDAW2.Models;
using System.Threading.Tasks;
using FinalDAW2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            bool isAdmin = User.IsInRole("Admin");

            // Dacă este admin, afișăm toți utilizatorii, altfel doar utilizatorul curent
            var users = isAdmin
                ? from user in db.Users
                  orderby user.UserName
                  select user
                : from user in db.Users
                  where user.UserName == User.Identity.Name
                  orderby user.UserName
                  select user;

            ViewBag.UsersList = users;

            return View();
        }


        public async Task<ActionResult> Show(string id)
        {
            ViewBag.UserNume = _userManager.GetUserName(User);
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await db.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }








        public async Task<ActionResult> EditProfile(string id)
        {
            ApplicationUser user = db.Users.Find(id);

            //user.AllRoles = GetAllRoles();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(string id, ApplicationUser newData)
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

                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }


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


            if (user.Comments.Count > 0)
            {
                foreach (var comment in user.Comments)
                {
                    db.Comments.Remove(comment);
                }
            }




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


        [NonAction]
        public IEnumerable<SelectListItem> GetAllRoles()
        {
            var selectList = new List<SelectListItem>();

            var roles = from role in db.Roles
                        select role;

            foreach (var role in roles)
            {
                selectList.Add(new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name.ToString()
                });
            }
            return selectList;
        }
    }
}