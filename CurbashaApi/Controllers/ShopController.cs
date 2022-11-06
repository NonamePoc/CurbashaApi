using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using CurbashaApi.Models;
using CurbashaApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;


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


        [HttpGet]
        public IActionResult Order(int? id, string size)
        {
            var selectedProduct = _context.AspProducts.FirstOrDefault(p => p.Id == id);

            // вытягнуть текущего юзера 
            var currentUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (ModelState.IsValid)
            {
                var delivery = _context.AspUserOrder.FirstOrDefault(u => u.User == currentUser);
                if (delivery == null || delivery.IsActive == false)
                {
                    var userOder = new AspUserOrder()
                    {
                        CreateAt = DateAndTime.Now,
                        IsActive = true,
                        User = currentUser
                    };

                    _context.AspUserOrder.Add(userOder);
                    _context.SaveChanges();
                    return RedirectToAction("Product");
                }

                // есть ли такой товар в aspitem
                var item = _context.AspOrderItems.FirstOrDefault(i => i.ProductId == id);
                if (item == null)
                {
                    var order = new AspOrderItem()
                    {
                        ProductId = selectedProduct.Id,
                        Quantity = 1,
                        Price = selectedProduct.Price,
                        Size = size,
                        OrderId = 2,
                        Product = selectedProduct,
                        ProductName = selectedProduct.NameProduct,

                    };
                    _context.AspOrderItems.Add(order);
                    _context.SaveChanges();
                }
                else
                {
                    _context.AspOrderItems.FirstOrDefault(i => i.ProductId == id).Quantity++;
                    _context.SaveChanges();
                }



            }

            return RedirectToAction("Product");

        }
    }
}

