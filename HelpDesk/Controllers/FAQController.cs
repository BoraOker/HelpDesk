using HelpDesk.Models;
using HelpDesk.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    [Authorize(Roles ="Admin")]
    public class FAQController : Controller {
        private readonly DataContext _context;

        public FAQController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            return View(await _context.FAQs.Include(x=>x.Product).ToListAsync());
        }

        public async Task<IActionResult> Create() {
            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(),"ProductId","ProductName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQViewModel model) {
            if(ModelState.IsValid) {
                _context.FAQs.Add(new FAQ(){
                    FAQId = model.FAQId,
                    Header = model.Header,
                    Context = model.Context,
                    ProductId = model.ProductId
                });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","FAQ");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id) {
            if(id==null) {
                return NotFound();
            }

            var faq = await _context.FAQs.Include(x=>x.Product).Select(k=> new FAQViewModel() {
                FAQId = k.FAQId, Header = k.Header,Context = k.Context,ProductId = k.ProductId
            }).FirstOrDefaultAsync(x=>x.FAQId == id);

            if(faq == null) {
                return NotFound();
            }
            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(),"ProductId","ProductName");
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FAQViewModel model) {
            if(id != model.FAQId) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(new FAQ() {FAQId=model.FAQId,Header=model.Header,Context = model.Context,ProductId=model.ProductId});
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateException) { 
                    if(!_context.FAQs.Any(o => o.FAQId == model.FAQId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                
                return RedirectToAction("Index","FAQ");
            }
            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(),"ProductId","ProductName");
            return View(model);
        }
    }
}