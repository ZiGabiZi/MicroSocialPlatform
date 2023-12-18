using FinalDAW2.Data;
using FinalDAW2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalDAW2.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProiectDAW.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public PostsController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager
                                )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Index()
        {
            var posts = db.Posts.Include("User");
            ViewBag.Posts = posts;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View();
        }

        public IActionResult IndexNou()
        {
            return View();
        }
        [Authorize(Roles = "User,Editor,Admin")]

        public IActionResult Show(int id)
        {
            Post postare = db.Posts.Include("User")
                               .Where(art => art.Id == id)
                               .First();

            return View(postare);
        }


        public IActionResult New()
        {
            Post postare = new Post();
            
            return View(postare);
        }


        [HttpPost]
        public IActionResult New(Post post)
        {   
            post.DataPostarii = DateTime.Now;
            post.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                TempData["message"] = "Postarea a fost adaugata";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                return View(post);
            }

        }





        // Se adauga articolul modificat in baza de date
        [HttpPost]
        public IActionResult Edit(int id, Post requestPost)
        {
            Post post = db.Posts.Find(id);

            if (ModelState.IsValid)
            {
                post.Continut = requestPost.Continut;
                db.SaveChanges();
                TempData["message"] = "Articolul a fost modificat";
                return RedirectToAction("Index");

            }
            else
            {
                return View(requestPost);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {   ///identificare articol + stergere + salvare shimbari + mesaj
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            TempData["message"] = "Articolul a fost sters";
            return RedirectToAction("Index");
        }



    }
}