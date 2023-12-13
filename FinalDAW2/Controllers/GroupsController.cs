using FinalDAW2.Data;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Models;

namespace ProiectDAW.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext db;

        public GroupsController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
