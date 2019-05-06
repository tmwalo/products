using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Products.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [Route("")]
        [Route("Index")]
        [Route("Products")]
        public IActionResult Index()
        {
            IEnumerable<Product> products;

            products = _context.ReadAllProducts();
            return View(products);
        }

        [Route("Products/ProductForm")]
        public IActionResult ProductForm() {
            Product product;

            product = new Product();
            return View(product);
        }

        [HttpPost]
        [Route("Products/Save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Product product) {

            if (!ModelState.IsValid)
                return View("ProductForm", product);
            if (product.Id == 0)
                _context.Create(product);
            else
                _context.Update(product);
            return RedirectToAction("Index", "Products");

        }

        [Route("Products/Edit/{id}")]
        public IActionResult Edit(int id) {
            Product product;

            product = _context.ReadProduct(id);
            if (product == null)
                return NotFound();
            return View("ProductForm", product);
        }

        [Route("Products/Delete/{id}")]
        public IActionResult Delete(int id) {
            Product product;

            product = _context.ReadProduct(id);
            if (product == null)
                return NotFound();
            _context.Delete(id);
            return RedirectToAction("Index", "Products");
        }
    }
}
