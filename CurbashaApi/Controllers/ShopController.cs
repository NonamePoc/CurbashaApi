using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using CurbashaApi.Models;
using CurbashaApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Controllers
{
    public class ShopController : Controller
    {
        private readonly CurbashaApiContext _context;

        public ShopController(CurbashaApiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Shop()
        {
            var allProducts = _context.AspProducts.Select(p => p);
            var allSelections = _context.AspSelections.Select(s => s);

            var shopVM = new ShopViewModel
            {
                Products = await allProducts.ToListAsync(),
                Selections = await allSelections.ToListAsync()
            };
            return View(shopVM);
        }

        public IActionResult Product(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            var selectedProduct = _context.AspProducts.Find(id);
            var selectedSection = _context.AspSelections.Find(selectedProduct.SelectionId);

            if (selectedProduct == null)
            {
                return NotFound("Your request did not find.");
            }
            string[] imagePathes = new string[2];
            imagePathes[0] = $"~/images/products/{selectedSection.SelectionName.ToLower()}-{id}.1.jpg";
            imagePathes[1] = $"~/images/products/{selectedSection.SelectionName.ToLower()}-{id}.2.jpg";
            ProductViewModel product = new ProductViewModel()
            {
                Product = selectedProduct,
                SectionName = selectedSection.SelectionName,
                ImagePathes = imagePathes
            };
            return View(product);
        }
    }
}
