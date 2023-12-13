using FinalDAW2.Data;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Models;

namespace ProiectDAW.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext db;

        public MessagesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
