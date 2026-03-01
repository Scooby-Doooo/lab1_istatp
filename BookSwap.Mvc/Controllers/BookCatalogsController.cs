using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookSwap.Mvc.Models;

namespace BookSwap.Mvc.Controllers
{
    public class BookCatalogsController : Controller
    {
        private readonly BookSwapDbContext _context;

        public BookCatalogsController(BookSwapDbContext context)
        {
            _context = context;
        }

        // GET: BookCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookCatalogs.ToListAsync());
        }

        // GET: BookCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCatalog = await _context.BookCatalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCatalog == null)
            {
                return NotFound();
            }

            return View(bookCatalog);
        }

        // GET: BookCatalogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Isbn")] BookCatalog bookCatalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCatalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookCatalog);
        }

        // GET: BookCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCatalog = await _context.BookCatalogs.FindAsync(id);
            if (bookCatalog == null)
            {
                return NotFound();
            }
            return View(bookCatalog);
        }

        // POST: BookCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Isbn")] BookCatalog bookCatalog)
        {
            if (id != bookCatalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCatalogExists(bookCatalog.Id))
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
            return View(bookCatalog);
        }

        // GET: BookCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCatalog = await _context.BookCatalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCatalog == null)
            {
                return NotFound();
            }

            return View(bookCatalog);
        }

        // POST: BookCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCatalog = await _context.BookCatalogs.FindAsync(id);
            if (bookCatalog != null)
            {
                _context.BookCatalogs.Remove(bookCatalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCatalogExists(int id)
        {
            return _context.BookCatalogs.Any(e => e.Id == id);
        }
    }
}
