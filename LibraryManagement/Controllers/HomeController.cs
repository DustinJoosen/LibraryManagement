using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .Include(b => b.BookHistories)
                .ToListAsync();

            return View(books);
        }

        [Route("/{id}")]
        public async Task<IActionResult> BookDetais(Guid id)
        {
            var book = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .Include(b => b.BookHistories)
                .ThenInclude(h => h.User)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var available = true;
            if (book.BookHistories.Any())
            {
                available = book?.BookHistories.Last().ReturnedAt != null;

            }
            ViewData["Available"] = available;
            return View(book);
        }

        [Route("/{id}/loan")]
        public async Task<IActionResult> Loan(Guid id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}