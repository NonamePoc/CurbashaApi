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
            var allProducts = _context.AspProducts.Where(p => p.IsActive == true);
            var allSelections = _context.AspSelections.Where(s => s.IsActive == true);

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
                return RedirectToAction("Home", "Home");
            }

            var selectedProduct = _context.AspProducts.FirstOrDefault(p => p.Id == id);

            if (selectedProduct == null)
            {
                return RedirectToAction("Home", "Home");
            }

            var selectedSection = _context.AspSelections.FirstOrDefault(s => s.Id == selectedProduct.SelectionId);

            if (!selectedProduct.IsActive || !selectedSection.IsActive)
            {
                return RedirectToAction("Home", "Home");
            }
            string[] imagePathes = new string[2];
            imagePathes[0] = $"~/images/products/{selectedSection.SelectionName.ToLower()}-{id}.1.jpg";
            imagePathes[1] = $"~/images/products/{selectedSection.SelectionName.ToLower()}-{id}.2.jpg";
            ProductViewModel product = new ProductViewModel()
            {
                Product = selectedProduct,
                SelectionName = selectedSection.SelectionName,
                ImagePathes = imagePathes
            };
            return View(product);
        }
    }
}
