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
using System.Dynamic;

namespace CurbashaApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SelectionsAdminController : Controller
    {
        private readonly CurbashaApiContext _context;

        public SelectionsAdminController(CurbashaApiContext context)
        {
            _context = context;
        }

        // GET: SelectionsAdmin/Index
        public async Task<IActionResult> Index()
        {
            ViewBag.Products = _context.AspProducts.Select(p => p).ToList();
            return View(await _context.AspSelections.ToListAsync());
        }

        // GET: SelectionsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SelectionsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectionName,IsActive")] AspSelections aspSelection)
        {
            if (aspSelection != null)
            {
                if (!_context.AspSelections.Any(s => s.SelectionName.Equals(aspSelection.SelectionName))) {
                    _context.Add(aspSelection);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Problem("Already exist. Try again");
                }
            }
            return View(aspSelection);
        }

        // GET: SelectionsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AspSelections == null)
            {
                return NotFound();
            }

            var aspSelections = await _context.AspSelections.FindAsync(id);
            if (aspSelections == null)
            {
                return NotFound();
            }
            return View(aspSelections);
        }

        // POST: SelectionsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectionName,IsActive")] AspSelections aspSelections)
        {
            if (id != aspSelections.Id)
            {
                return NotFound();
            }

            if (aspSelections != null)
            {
                try
                {
                    _context.Update(aspSelections);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspSelectionsExists(aspSelections.Id))
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
            return View(aspSelections);
        }

        // GET: SelectionsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AspSelections == null)
            {
                return NotFound();
            }

            var aspSelections = await _context.AspSelections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspSelections == null)
            {
                return NotFound();
            }

            return View(aspSelections);
        }

        // POST: SelectionsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspSelections == null)
            {
                return Problem("Entity set 'CurbashaApiContext.AspSelections'  is null.");
            }
            var aspSelections = await _context.AspSelections.FindAsync(id);
            if (aspSelections != null)
            {
                _context.AspSelections.Remove(aspSelections);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspSelectionsExists(int id)
        {
          return _context.AspSelections.Any(e => e.Id == id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
