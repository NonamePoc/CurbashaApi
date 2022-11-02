using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;

namespace CurbashaApi.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserOrdersAdminController : Controller
    {
        private readonly CurbashaApiContext _context;

        public UserOrdersAdminController(CurbashaApiContext context)
        {
            _context = context;
        }

        // GET: UserOrdersAdmin/Index
        public async Task<IActionResult> Index()
        {
              return View(await _context.AspUserOrder.ToListAsync());
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

            return View(aspUserOrder);
        }

        // GET: UserOrdersAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserOrdersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreateAt,IsActive")] AspUserOrder aspUserOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspUserOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspUserOrder);
        }

        // GET: UserOrdersAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AspUserOrder == null)
            {
                return NotFound();
            }

            var aspUserOrder = await _context.AspUserOrder.FindAsync(id);
            if (aspUserOrder == null)
            {
                return NotFound();
            }
            return View(aspUserOrder);
        }

        // POST: UserOrdersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreateAt,IsActive")] AspUserOrder aspUserOrder)
        {
            if (id != aspUserOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspUserOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspUserOrderExists(aspUserOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
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
