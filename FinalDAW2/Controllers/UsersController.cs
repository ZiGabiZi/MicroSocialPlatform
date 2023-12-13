using Microsoft.AspNetCore.Mvc;

namespace FinalDAW2.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
