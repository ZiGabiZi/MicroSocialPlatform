using FinalDAW2.Data;
using FinalDAW2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalDAW2.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Microsoft.Extensions.Hosting;

namespace FinalDAW2.Controllers
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
        [Authorize(Roles = "User,Admin")]
        public IActionResult Index()
        {
            SetAccessRights();

            var friendPosts = GetFriendPosts();
            var friendPostIds = friendPosts.Select(post => post.Id).ToList();



            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            ViewBag.Posts = friendPosts.ToList();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View();
        }

        private List<Post> GetFriendPosts()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var friendIds = db.Friends
                .Where(f => (f.SenderId == currentUserId || f.ReceiverId == currentUserId) && f.Status == 1)
                .Select(f => f.SenderId == currentUserId ? f.ReceiverId : f.SenderId)
                .ToList();

            friendIds.Add(currentUserId);

            var publicPosts = db.Posts
                .Include(post => post.User)
                .Where(post =>
                    (post.User.IsProfilePublic && post.UserId != currentUserId && !friendIds.Contains(post.UserId) && post.GroupId == null))
                .ToList();

            var allPosts = db.Posts
            .Include("User")
                .Where(post => (friendIds.Contains(post.UserId) || (post.User.IsProfilePublic && post.UserId == currentUserId)) && post.GroupId == null)
                .ToList();

            var friendPosts = allPosts
                .Where(post => friendIds.Contains(post.UserId))
                .ToList();

            var concatenatedPosts = friendPosts.Concat(publicPosts).ToList();

            return concatenatedPosts;
        }


        public IActionResult IndexNou()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(int id)
        {
            SetAccessRights(); 

            Post postare = db.Posts.Include("User")
                                   .Include("Comments")
                                   .Include("Comments.User")
                                   .Where(art => art.Id == id)
                                   .First();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View(postare);
        }


        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Show([FromForm] Comment comment)
        {
            comment.DataComentariu = DateTime.Now;
            comment.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("/Posts/Show/" + comment.PostId);
            }

            else
            {
                Post postare = db.Posts.Include("User")
                                         .Include("Comments")
                                         .Include("Comments.User")
                                         .Where(art => art.Id == comment.PostId)
                                         .First();


               

                return View(postare);
            }
        }


        [Authorize(Roles = "Admin,User")]
        public IActionResult New(int? groupId)
        {
            Post postare = new Post();
            if (groupId.HasValue)
            {
                postare.GroupId = groupId.Value;
            }

            return View(postare);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public IActionResult New(Post post, int? groupId)
        {
            post.DataPostarii = DateTime.Now;
            post.UserId = _userManager.GetUserId(User);

            if(groupId != null)
            {
                post.GroupId = groupId;
            }

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


        [Authorize(Roles = "Admin,User")]
        public IActionResult Edit(int id)
        {

            Post postare = db.Posts.Where(art => art.Id == id)
                                   .First();


            if (postare.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(postare);
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei POSTARI care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]

        public IActionResult Edit(int id, Post requestPost)
        {
            Post postare = db.Posts.Find(id);

            if (ModelState.IsValid)
            {
                if (postare.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    postare.Continut = requestPost.Continut;
                    db.SaveChanges();
                    TempData["message"] = "Postarea a fost modificat";
                    return RedirectToAction("Index");
                }


                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei postari care nu va apartine";
                    TempData["messageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(requestPost);

            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin,User")]

        public ActionResult Delete(int id)
        {
            Post postare = db.Posts.Include("Comments")
                                         .Where(art => art.Id == id)
                                         .First();

            if (postare.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Posts.Remove(postare);
                db.SaveChanges();
                TempData["message"] = "Postarea a fost stearsa";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti o postare care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }

        private void SetAccessRights()
        {
            ViewBag.AfisareButoane = false;

            if (User.IsInRole("Admin"))
            {
                ViewBag.AfisareButoane = true;
            }

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }




    }
}