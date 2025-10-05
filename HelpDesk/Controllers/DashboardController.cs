using HelpDesk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller {

        private readonly DataContext _context;

        public DashboardController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            ViewBag.TicketCount = _context.Tickets.Count();
            return View(await _context.Tickets.Include(x=>x.Product).Include(x=>x.ProductCategory).ToListAsync());
        }
    }
}