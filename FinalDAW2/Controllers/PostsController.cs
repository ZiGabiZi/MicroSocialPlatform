using FinalDAW2.Data;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Models;

namespace ProiectDAW.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext db;

        public PostsController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
