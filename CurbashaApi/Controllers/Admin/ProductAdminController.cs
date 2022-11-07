using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using CurbashaApi.Models;
using CurbashaApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductAdminController : Controller
    {
        private readonly CurbashaApiContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductAdminController(CurbashaApiContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: /ProductAdmin/Index
        public IActionResult Index()
        {
            var products = _context.AspProducts.ToList();
            ViewBag.Sections = _context.AspSelections.Select(p => p).ToList();
            return View(products);
        }

        #region Create
        // GET: ProductAdmin/Create
        public IActionResult Create()
        {
            ViewBag.SelectionId = new SelectList(_context.AspSelections, "Id", "SelectionName");
            return View();
        }

        // POST: ProductAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameProduct,Description,SelectionId,Price,IsActive")] AspProduct aspProduct)
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
        // GET: ProductAdmin/Edit/5
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

        // POST: ProductAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameProduct,Description,SelectionId,Price,IsActive")] AspProduct product, IFormFile formFile)
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
        // GET: ProductAdmin/Delete/5
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

        // POST: ProductAdmin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.AspProducts.FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                DeleteImageFromWWWRootFolder(product);
                _context.AspProducts.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
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

        #region Photo
        // GET: ProductAdmin/Photo/5
        [HttpGet]
        public async Task<IActionResult> Photo(int? id)
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

        // POST: ProductAdmin/Photo/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Photo(int id, AspProduct product, IFormFile formFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            product = await _context.AspProducts.FirstAsync(p => p.Id == id);
            if (product != null)
            {
                if (IsImage(formFile))
                {
                    var ImagePath = Path.Combine(_environment.WebRootPath, "images", "products",
                        $"{product.Id}.1.jpg");
                    if (System.IO.File.Exists(ImagePath))
                    {
                        DeleteImageFromWWWRootFolder(product);
                    }
                    string actionResult = ProcessUploadedFile(ImagePath, formFile);
                    ViewBag.Message = actionResult;
                }
                else
                    ViewBag.Message = "Not an image";

                return View(product);
            }

            return View(product);
        }
        #endregion


        [HttpPost]
        private string ProcessUploadedFile(string ImagePath, IFormFile formFile)
        {

            try
            {
                Guid guid = Guid.NewGuid();
                var stream = new FileStream(ImagePath, FileMode.Create);
                formFile.CopyToAsync(stream);
                return "File uploaded successfully.";
            }
            catch
            {
                return "Error while uploading the files.";
            }
        }

        private void DeleteImageFromWWWRootFolder(AspProduct product)
        {
            var CurrentImage = Path.Combine(_environment.WebRootPath, "images", "products",
                $"{product.Id}.1.jpg");
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
        }

        private bool IsImage(IFormFile formFile)
        {
            var imageTypes = new string[] {
                    "image/jpg",
                    "image/jpeg",
                    "image/png"
                };
            if (formFile == null || formFile.Length == 0)
            {
                return false;
            }
            else if (!imageTypes.Contains(formFile.ContentType))
            {
                return false;
            }
            return true;
        }

        private bool ProductExists(int id)
        {
            return _context.AspProducts.Any(e => e.Id == id);
        }
    }
}