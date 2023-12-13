using Microsoft.AspNetCore.Mvc;

namespace FinalDAW2.Controllers
{
    public class ApplicationUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
