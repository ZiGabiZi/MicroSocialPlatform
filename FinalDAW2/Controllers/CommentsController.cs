using FinalDAW2.Data;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Models;

namespace ProiectDAW.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;

        public CommentsController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
