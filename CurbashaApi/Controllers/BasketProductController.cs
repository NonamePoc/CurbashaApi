using Microsoft.AspNetCore.Mvc;

namespace CurbashaApi.Controllers
{
    public class BasketProductController : Controller
    {
        public IActionResult Basket()
        {
            return View();
        }
    }
}
