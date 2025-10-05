using HelpDesk.Models;
using HelpDesk.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;
        private readonly IEmailSender _emailSender;
        public TicketController(DataContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.Include(x => x.Product).Include(x => x.ProductCategory).ToListAsync());
        }

        public async Task<IActionResult> GetProductCategories(int productId)
        {
            var categories = await _context.ProductCategories
                                           .Where(pc => pc.ProductId == productId)
                                           .Select(pc => new { value = pc.ProductCategoryId, text = pc.CategoryContext })
                                           .ToListAsync();
            return Json(categories);
        }

        public async Task<IActionResult> CreateTicket()
        {

            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(), "ProductId", "ProductName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket(TicketViewModel model, IFormFile? ticketFile)
        {

            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && user.Email != null)
                {
                    model.Email = user.Email;
                }
            }

            if (ticketFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".PNG", ".pdf", ".JPG" };
                var extension = Path.GetExtension(ticketFile.FileName);

                var randomFileName = string.Format($"{Guid.NewGuid()}{extension}");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Please select a valid file extension (jpg, jpeg, png, PNG, pdf, JPG)");
                }
                if (ModelState.IsValid)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ticketFile!.CopyToAsync(stream);
                    }
                    model.TicketFile = randomFileName;
                }
            }


            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    TicketId = model.TicketId,
                    ProductId = model.ProductId,
                    ProductCategoryId = model.ProductCategoryId,
                    ProblemSummary = model.ProblemSummary,
                    Problem = model.Problem,
                    TicketDate = DateTime.Now,
                    TicketFile = model.TicketFile,
                    Email = model.Email
                };

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();

                var emailSubject = "Your request has been received.";
                var emailMessage = "Your request has been successfully received. We will contact you as soon as possible.";

                if (ticket.Email != null)
                {
                    await _emailSender.SendEmailAsync(ticket.Email, emailSubject, emailMessage);
                }

                return RedirectToAction("MyTickets", "Ticket");
            }

            ViewBag.Products = new SelectList(await _context.Products.ToListAsync(), "ProductId", "ProductName");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> MyTickets()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var userTickets = await _context.Tickets
                                                    .Include(x => x.Product)
                                                    .Include(x => x.ProductCategory)
                                                    .Where(t => t.Email == user.Email)
                                                    .ToListAsync();
                    return View(userTickets);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}