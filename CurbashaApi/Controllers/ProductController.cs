using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using CurbashaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly CurbashaApiContext _context;

        public ProductController(CurbashaApiContext context)
        {
            _context = context;
        }

        // GET: Product
        public IActionResult Index(string id)
        {
            string searchString = id;
            var products = _context.AspProducts.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.NameProduct.Contains(searchString)).ToList();
            }
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            AspProduct? product = await _context.AspProducts.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Your request did not find.");
            }
            return View(product);
        }

        #region Create
        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.SelectionId = new SelectList(_context.AspSelections, "Id", "SelectionName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameProduct,Description,SelectionId,Price")] AspProduct aspProduct)
        {
            aspProduct.AspSelections = _context.AspSelections.First(s => s.Id == aspProduct.SelectionId);
            if (aspProduct != null)
            {
                _context.AspProducts.Add(aspProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SelectionId = new SelectList(_context.AspSelections, "Id", "SelectionName", aspProduct.SelectionId);
            return View(aspProduct);
        }
        #endregion

        #region Edit
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            AspProduct? product = await _context.AspProducts.FirstAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Your request did not find.");
            }
            ViewBag.SelectionId = new SelectList(_context.AspSelections, "Id", "SelectionName", product.SelectionId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameProduct,Description,SelectionId,Price")] AspProduct product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (product != null)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.SelectionId = new SelectList(_context.AspSelections, "Id", "SelectionName", product.SelectionId);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
        #endregion

        #region Delete
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            AspProduct? product = await _context.AspProducts.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Your request did not find.");
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspProducts == null)
            {
                return Problem("Entity set 'CurbashaApiContext.AspProducts' is null.");
            }
            var product = await _context.AspProducts.FindAsync(id);
            if (product != null)
            {
                _context.AspProducts.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return _context.AspProducts.Any(e => e.Id == id);
        }
    }
}
