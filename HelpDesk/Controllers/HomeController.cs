using HelpDesk.Models;
using HelpDesk.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Controllers
{

    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(), "ProductId", "ProductName");
            return View(await _context.FAQs.Include(x => x.Product).ToListAsync());
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public async Task<IActionResult> GetFAQsByProduct(int productId)
        {
            var faqs = await _context.FAQs.Where(faq => faq.ProductId == productId).Select(faq => new
            {
                faq.Header,
                faq.Context
            }).ToListAsync();

            return Json(faqs);
        }
    }
}