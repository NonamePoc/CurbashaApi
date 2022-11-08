using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CurbashaApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CurbashaApiContext _context;

        public UsersAdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, CurbashaApiContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        // GET: UsersAdmin/Index
        public IActionResult Index()
        {
            ViewBag.roles = _roleManager.Roles.ToList();
            return View(_userManager.Users.ToList());
        }

        // GET: UsersAdmin/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Admin");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Error404", "Admin");
            }

            return View(user);
        }

        // POST: UsersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var lastUserCart = _context.AspUserOrder.LastOrDefault(o => o.User.Id == id);
                if (lastUserCart != null && lastUserCart.IsActive)
                {
                    var activeProductItems = _context.AspOrderItems.Where(ap => ap.OrderId == lastUserCart.Id);
                    _context.AspOrderItems.RemoveRange(activeProductItems);
                    _context.AspUserOrder.Remove(lastUserCart);
                }
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

