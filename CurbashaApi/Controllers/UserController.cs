using Microsoft.AspNetCore.Mvc;

namespace CurbashaApi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult User()
        {
            return View();
        }
    }
}
