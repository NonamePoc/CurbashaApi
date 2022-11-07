using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CurbashaApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserOrdersAdminController : Controller
    {
        private readonly CurbashaApiContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrdersAdminController(CurbashaApiContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserOrdersAdmin/Index
        public async Task<IActionResult> Index()
        {
            ViewBag.userNames = _userManager.Users.ToList();
            return View(await _context.AspUserOrder.ToListAsync());
        }

        // GET: UserOrdersAdmin/User/IdUser
        public async Task<IActionResult> User(string id)
        {
            ViewBag.userNames = _userManager.Users.ToList();
            return View("Index", await _context.AspUserOrder.Where(o => o.User.Id == id).ToListAsync());
        }

        // GET: UserOrdersAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AspUserOrder == null)
            {
                return NotFound();
            }

            var aspUserOrder = await _context.AspUserOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspUserOrder == null)
            {
                return NotFound();
            }
            ViewBag.SelectedItems = _context.AspOrderItems.Where(o => o.OrderId == id).ToList();
            return View(aspUserOrder);
        }

        // GET: UserOrdersAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AspUserOrder == null)
            {
                return NotFound();
            }

            var aspUserOrder = await _context.AspUserOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspUserOrder == null)
            {
                return NotFound();
            }

            return View(aspUserOrder);
        }

        // POST: UserOrdersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspUserOrder == null)
            {
                return Problem("Entity set 'CurbashaApiContext.AspUserOrder'  is null.");
            }
            var aspUserOrder = await _context.AspUserOrder.FindAsync(id);
            if (aspUserOrder != null)
            {
                var activeProductItems = _context.AspOrderItems.Where(ap => ap.OrderId == aspUserOrder.Id);
                _context.AspOrderItems.RemoveRange(activeProductItems);
                _context.AspUserOrder.Remove(aspUserOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspUserOrderExists(int id)
        {
          return _context.AspUserOrder.Any(e => e.Id == id);
        }
    }
}
