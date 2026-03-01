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
    public class PhysicalItemsController : Controller
    {
        private readonly BookSwapDbContext _context;

        public PhysicalItemsController(BookSwapDbContext context)
        {
            _context = context;
        }

        // GET: PhysicalItems
        public async Task<IActionResult> Index()
        {
            var bookSwapDbContext = _context.PhysicalItems.Include(p => p.Book).Include(p => p.Owner);
            return View(await bookSwapDbContext.ToListAsync());
        }

        // GET: PhysicalItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physicalItem = await _context.PhysicalItems
                .Include(p => p.Book)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (physicalItem == null)
            {
                return NotFound();
            }

            return View(physicalItem);
        }

        // GET: PhysicalItems/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.BookCatalogs, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PhysicalItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,OwnerId,Status,Description,CreatedAt,UpdatedAt")] PhysicalItem physicalItem)
        {
            var book = _context.BookCatalogs.FirstOrDefault(b => b.Id == physicalItem.BookId);
            var owner = _context.Users.FirstOrDefault(o => o.Id == physicalItem.OwnerId);
            
            physicalItem.Book = book;
            physicalItem.Owner = owner;

            ModelState.Clear();
            TryValidateModel(physicalItem);

            if (ModelState.IsValid)
            {
                _context.Add(physicalItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.BookCatalogs, "Id", "Title", physicalItem.BookId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Username", physicalItem.OwnerId);
            return View(physicalItem);
        }

        // GET: PhysicalItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physicalItem = await _context.PhysicalItems.FindAsync(id);
            if (physicalItem == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.BookCatalogs, "Id", "Id", physicalItem.BookId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", physicalItem.OwnerId);
            return View(physicalItem);
        }

        // POST: PhysicalItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,OwnerId,Status,Description,CreatedAt,UpdatedAt")] PhysicalItem physicalItem)
        {
            if (id != physicalItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(physicalItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhysicalItemExists(physicalItem.Id))
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
            ViewData["BookId"] = new SelectList(_context.BookCatalogs, "Id", "Id", physicalItem.BookId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", physicalItem.OwnerId);
            return View(physicalItem);
        }

        // GET: PhysicalItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physicalItem = await _context.PhysicalItems
                .Include(p => p.Book)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (physicalItem == null)
            {
                return NotFound();
            }

            return View(physicalItem);
        }

        // POST: PhysicalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var physicalItem = await _context.PhysicalItems.FindAsync(id);
            if (physicalItem != null)
            {
                _context.PhysicalItems.Remove(physicalItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhysicalItemExists(int id)
        {
            return _context.PhysicalItems.Any(e => e.Id == id);
        }
    }
}
