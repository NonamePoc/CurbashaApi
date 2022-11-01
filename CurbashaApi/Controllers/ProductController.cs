using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using CurbashaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly CurbashaApiContext _context;

        public ProductController(CurbashaApiContext context)
        {
            _context = context;
        }

        // GET: Product
        public IActionResult Index()
        {
            var products = _context.AspProducts.ToList();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> CreateAsync([Bind("Id,NameProduct,Description,Price,SelectionId")] AspProduct product)
        {
            if (ModelState.IsValid)
            {
                _context.AspProducts.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SelectionId = new SelectList(_context.AspSelections, "Id", "SelectionName", product.SelectionId);
            return View(product);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameProduct,Price,Description,SelectionId")] AspProduct product)
        {
            if (id != product.Id)
            {
                return NotFound("Your request did not valid.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(product).State = EntityState.Modified;
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
                return RedirectToAction("Index");
            }
            ViewBag.SelectionId = new SelectList(_context.AspSelections, "Id", "SelectionName", product.SelectionId);
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
            AspProduct? product = await _context.AspProducts.FirstAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Your request did not find.");
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AspProduct product = await _context.AspProducts.FirstAsync(p => p.Id == id);
            _context.AspProducts.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
