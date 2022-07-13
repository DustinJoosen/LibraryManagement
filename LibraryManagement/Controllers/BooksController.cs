using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using LibraryManagement.Helpers;

namespace LibraryManagement.Controllers
{
    [Route("/books/{action=Index}/{id?}")]
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _env;

        public BooksController(ApplicationDbContext context, INotyfService notyf, IWebHostEnvironment env)
        {
            _context = context;
            _notyf = notyf;
            _env = env;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .OrderBy(b => b.Title)
                .ToListAsync();

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CoverImage,Language,Pages,AuthorId,GenreId,FormFile")] Book book)
        {
            if (!string.IsNullOrWhiteSpace(book.Title) && book.AuthorId != Guid.Empty && book.GenreId != Guid.Empty)
            {
                book.Id = Guid.NewGuid();

                ImageHelper.UploadImage(ref book, this._env);

                _context.Add(book);
                await _context.SaveChangesAsync();

                _notyf.Information("Successfully added book");
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "LastName", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "LastName", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,CoverImage,Language,Pages,AuthorId,GenreId,FormFile")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(book.Title) && book.AuthorId != Guid.Empty && book.GenreId != Guid.Empty)
            {
                try
                {
                    if (book.FormFile != null)
                    {
                        ImageHelper.UploadImage(ref book, _env);
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();

                    _notyf.Information("Successfully updated book");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "LastName", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                ImageHelper.RemoveImage(ref book, _env);
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();

            _notyf.Information("Successfully deleted book");
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
