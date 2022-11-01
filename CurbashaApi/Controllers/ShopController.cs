using Microsoft.AspNetCore.Mvc;

namespace CurbashaApi.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }
    }
}
