using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CurbashaApi.DB;
using CurbashaApi.DB.Entities;

namespace CurbashaApi.Controllers
{
    public class UserAuthsController : Controller
    {
        private readonly Context _context;

        public UserAuthsController(Context context)
        {
            _context = context;
        }

        // GET: UserAuths
        public async Task<IActionResult> Index()
        {
              return View(await _context.UserAuth.ToListAsync());
        }

        // GET: UserAuths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserAuth == null)
            {
                return NotFound();
            }

            var userAuth = await _context.UserAuth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAuth == null)
            {
                return NotFound();
            }

            return View(userAuth);
        }

        // GET: UserAuths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAuths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password")] UserAuth userAuth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAuth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAuth);
        }

        // GET: UserAuths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserAuth == null)
            {
                return NotFound();
            }

            var userAuth = await _context.UserAuth.FindAsync(id);
            if (userAuth == null)
            {
                return NotFound();
            }
            return View(userAuth);
        }

        // POST: UserAuths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password")] UserAuth userAuth)
        {
            if (id != userAuth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAuth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAuthExists(userAuth.Id))
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
            return View(userAuth);
        }

        // GET: UserAuths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserAuth == null)
            {
                return NotFound();
            }

            var userAuth = await _context.UserAuth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAuth == null)
            {
                return NotFound();
            }

            return View(userAuth);
        }

        // POST: UserAuths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserAuth == null)
            {
                return Problem("Entity set 'Context.UserAuth'  is null.");
            }
            var userAuth = await _context.UserAuth.FindAsync(id);
            if (userAuth != null)
            {
                _context.UserAuth.Remove(userAuth);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAuthExists(int id)
        {
          return _context.UserAuth.Any(e => e.Id == id);
        }
    }
}
