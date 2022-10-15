using Microsoft.AspNetCore.Mvc;

namespace CurbashaApi.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
