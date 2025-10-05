using HelpDesk.Models;
using HelpDesk.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductCategoryController : Controller {

        private readonly DataContext _context;

        public ProductCategoryController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            return View(await _context.ProductCategories.Include(x => x.Product).ToListAsync());
        }

        public async Task<IActionResult> CreateProductCategory() {

            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(),"ProductId","ProductName");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductCategory(ProductCategoryViewModel model) {

            if(ModelState.IsValid) {
                _context.ProductCategories.Add(new ProductCategory() {ProductCategoryId = model.ProductCategoryId,CategoryContext = model.CategoryContext,ProductId = model.ProductId});
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","ProductCategory");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id) {
            if(id==null) {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories.Include(x=>x.Product).Select(k=> new ProductCategoryViewModel() {
                ProductCategoryId = k.ProductCategoryId, CategoryContext = k.CategoryContext,ProductId = k.ProductId
            }).FirstOrDefaultAsync(x=>x.ProductCategoryId == id);

            if(productCategory == null) {
                return NotFound();
            }
            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(),"ProductId","ProductName");
            return View(productCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCategoryViewModel model) {
            if(id != model.ProductCategoryId) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(new ProductCategory() {ProductCategoryId=model.ProductCategoryId,CategoryContext=model.CategoryContext,ProductId=model.ProductId});
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateException) { 
                    if(!_context.ProductCategories.Any(o => o.ProductCategoryId == model.ProductCategoryId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                
                return RedirectToAction("Index","ProductCategory");
            }
            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(),"ProductId","ProductName");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories.FindAsync(id);

            if(productCategory == null) {
                return NotFound();
            }

            return View(productCategory);
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id) {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if(productCategory == null) {
                return NotFound();
            }
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}