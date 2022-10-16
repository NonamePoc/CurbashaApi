using Microsoft.AspNetCore.Mvc;

namespace CurbashaApi.Controllers
{
    public class FurnitureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
