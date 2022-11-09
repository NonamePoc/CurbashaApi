using System.Linq;
using System.Threading.Tasks;
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
            var delivery = GetDelivery();

            if (delivery == null || delivery.IsActive == false)
            {
                return RedirectToAction("Shop", "Shop");
            }


            var items = _context.AspOrderItems.Select(s => s).Where(w => w.OrderId == delivery.Id);

            ViewBag.total = _context.AspOrderItems.Select(s => s).Where(w => w.OrderId == delivery.Id)
                .Sum(s => s.Price * s.Quantity);


            return View(items);
        }

        [HttpPost]
        public IActionResult Basket(BasketViewModel model, int id)
        {
            var item = GetItem(id);
            _context.AspOrderItems.Remove(item);
            _context.SaveChanges();


            return Json("Basket");
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var delivery = GetDelivery();

            ViewBag.total = _context.AspUserOrder.Select(s => s).Where(w => w.Id == delivery.Id).FirstOrDefault().Total;

            return View(delivery);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderItemQuantity(int id, string symbol)
        {
            var orderItem = await _context.AspOrderItems.FirstOrDefaultAsync(f => f.Id == id);

            if (orderItem is null)
                return BadRequest("Order item not found");

            if (orderItem.Quantity <= 1 && symbol == "-")
                return Ok();

            switch (symbol)
            {
                case "+":
                    ++orderItem.Quantity;
                    break;

                case "-":
                    --orderItem.Quantity;
                    break;

                default:
                    return BadRequest("Symbol not valid");
            }

            _context.AspOrderItems.Update(orderItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private AspUserOrder GetDelivery()
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            var basket = Request.Cookies["guest"];


            if (currentUser == null)
            {
                currentUser = _context.Users.FirstOrDefault(u => u.Id == basket);
            }

            return _context.AspUserOrder.OrderBy(o => o.Id).LastOrDefault(u => u.User == currentUser);
        }


        private AspOrderItem GetItem(int id)
        {
            return _context.AspOrderItems.FirstOrDefault(i => i.Id == id);
        }
    }
}