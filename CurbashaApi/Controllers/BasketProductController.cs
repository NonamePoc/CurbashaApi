using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using CurbashaApi.Models;
using CurbashaApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Controllers
{
    public class BasketProductController : Controller
    {
        private readonly CurbashaApiContext _context;

        public BasketProductController(CurbashaApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Basket()
        {
            //ViewBag.count = _context.AspOrderItems.Where( w=> w.ProductId ).Select(s => s.Quantity);
            var items = _context.AspOrderItems.ToList();
            ViewBag.total = _context.AspOrderItems.Sum(s => s.Price);

            return View(items);
           
        }

        [HttpPost]
        public IActionResult Basket(BasketViewModel model)
        {
          
            var items = _context.AspOrderItems.ToList();
            _context.AspOrderItems.RemoveRange(items);
            _context.SaveChanges();

            // asp order isactive false 
            var order = _context.AspUserOrder.FirstOrDefault();
            order.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Basket");
        }
    }
}

