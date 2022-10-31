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

        public async Task<IActionResult> Index()
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
    }
}
