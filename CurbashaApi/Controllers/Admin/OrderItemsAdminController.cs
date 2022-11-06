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

namespace CurbashaApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderItemsAdminController : Controller
    {
        private readonly CurbashaApiContext _context;

        public OrderItemsAdminController(CurbashaApiContext context)
        {
            _context = context;
        }

        // GET: OrderItemsAdmin/Index
        public async Task<IActionResult> Index()
        {
            var curbashaApiContext = _context.AspOrderItems.Include(a => a.Product);
            return View(await curbashaApiContext.ToListAsync());
        }

        // GET: OrderItemsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AspOrderItems == null)
            {
                return NotFound();
            }

            var aspOrderItem = await _context.AspOrderItems
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspOrderItem == null)
            {
                return NotFound();
            }

            return View(aspOrderItem);
        }

        // GET: OrderItemsAdmin/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.AspProducts, "Id", "NameProduct");
            return View();
        }

        // POST: OrderItemsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Size,Quantity,OrderId")] AspOrderItem aspOrderItem)
        {
            aspOrderItem.Price = aspOrderItem.Product.Price;
            if (aspOrderItem != null)
            {
                _context.Add(aspOrderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.AspProducts, "Id", "NameProduct", aspOrderItem.ProductId);
            return View(aspOrderItem);
        }

        // GET: OrderItemsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AspOrderItems == null)
            {
                return NotFound();
            }

            var aspOrderItem = await _context.AspOrderItems.FindAsync(id);
            if (aspOrderItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.AspProducts, "Id", "NameProduct", aspOrderItem.ProductId);
            return View(aspOrderItem);
        }

        // POST: OrderItemsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Size,Quantity,Price,OrderId")] AspOrderItem aspOrderItem)
        {
            if (id != aspOrderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspOrderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspOrderItemExists(aspOrderItem.Id))
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
            ViewData["ProductId"] = new SelectList(_context.AspProducts, "Id", "NameProduct", aspOrderItem.ProductId);
            return View(aspOrderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AspOrderItems == null)
            {
                return NotFound();
            }

            var aspOrderItem = await _context.AspOrderItems
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspOrderItem == null)
            {
                return NotFound();
            }

            return View(aspOrderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspOrderItems == null)
            {
                return Problem("Entity set 'CurbashaApiContext.AspOrderItems'  is null.");
            }
            var aspOrderItem = await _context.AspOrderItems.FindAsync(id);
            if (aspOrderItem != null)
            {
                _context.AspOrderItems.Remove(aspOrderItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspOrderItemExists(int id)
        {
          return _context.AspOrderItems.Any(e => e.Id == id);
        }
    }
}
