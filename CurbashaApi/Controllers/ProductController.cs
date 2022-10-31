using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using CurbashaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurbashaApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly CurbashaApiContext _context;

        public ProductController(CurbashaApiContext context)
        {
            _context = context;
        }

        public IActionResult Product(int id, string selectionName)
        {
            var selectedProduct = _context.AspProducts.First(p => p.Id == id);
            string[] imagePathes = new string[2];
            imagePathes[0] = $"~/images/{selectedProduct.NameProduct.Replace(" ", "_")}-1.png";
            imagePathes[1] = $"~/images/{selectedProduct.NameProduct.Replace(" ", "_")}-2.png";
            ProductModel product = new ProductModel()
            {
                Product = selectedProduct,
                SectionName = selectionName,
                ImagePathes = imagePathes
            };
            return View(product);
        }
    }
}

