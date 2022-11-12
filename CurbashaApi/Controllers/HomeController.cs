using CurbashaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace CurbashaApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly CurbashaApiContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CurbashaApiContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Home()
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            //Если у юзера нету аккаунта сделать ему аккаунт и присвоить ему id
            if (currentUser == null)
            {
                var cookie = Request.Cookies["guest"];
                if (cookie == null)
                {
                    var random = new Random();
                    var guest = new IdentityUser()
                    {
                        UserName = "Guest" + random.Next(1000, 9999),
                        EmailConfirmed = true
                    };
                    _context.Users.Add(guest);
                    _context.SaveChanges();
                    currentUser = guest;
                    Response.Cookies.Append("guest", guest.Id);
                }
                else
                {
                    currentUser = _context.Users.FirstOrDefault(u => u.Id == cookie);
                }
            }


            var delivery = _context.AspUserOrder.OrderBy(o => o.Id).LastOrDefault(u => u.User == currentUser);
            if (delivery == null || delivery.IsActive == false)
            {
                var userOder = new AspUserOrder()
                {
                    CreateAt = DateAndTime.Now,
                    IsActive = true,
                    User = currentUser,
                };

                _context.AspUserOrder.Add(userOder);
                _context.SaveChanges();
                return RedirectToAction("Home");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}