using HelpDesk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller {

        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult CreateProduct() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product model) {

            if(ModelState.IsValid) {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Product");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id) {
            if(id==null) {
                return NotFound();
            }

            var product = await _context.Products.Select(k=> new Product() {
                ProductId = k.ProductId, ProductName = k.ProductName,ProductCategories = k.ProductCategories
            }).FirstOrDefaultAsync(x=>x.ProductId == id);

            if(product == null) {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product model) {
            if(id != model.ProductId) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(new Product() {ProductId=model.ProductId,ProductName=model.ProductName,ProductCategories=model.ProductCategories});
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateException) { 
                    if(!_context.Products.Any(o => o.ProductId == model.ProductId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                
                return RedirectToAction("Index","Product");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if(product == null) {
                return NotFound();
            }

            return View(product);
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id) {
            var product = await _context.Products.FindAsync(id);
            if(product == null) {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}