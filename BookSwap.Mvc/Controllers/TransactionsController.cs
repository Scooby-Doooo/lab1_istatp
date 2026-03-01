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
    public class TransactionsController : Controller
    {
        private readonly BookSwapDbContext _context;

        public TransactionsController(BookSwapDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var bookSwapDbContext = _context.Transactions.Include(t => t.Requester).Include(t => t.TargetItem);
            return View(await bookSwapDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Requester)
                .Include(t => t.TargetItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["RequesterId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TargetItemId"] = new SelectList(_context.PhysicalItems, "Id", "Id");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequesterId,TargetItemId,Status,CreatedAt")] Transaction transaction)
        {
            var requester = _context.Users.FirstOrDefault(o => o.Id == transaction.RequesterId);
            var targetItem = _context.PhysicalItems.FirstOrDefault(o => o.Id == transaction.TargetItemId);

            transaction.Requester = requester;
            transaction.TargetItem = targetItem;

            ModelState.Clear();
            TryValidateModel(transaction);

            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequesterId"] = new SelectList(_context.Users, "Id", "Username", transaction.RequesterId);
            ViewData["TargetItemId"] = new SelectList(_context.PhysicalItems, "Id", "Description", transaction.TargetItemId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["RequesterId"] = new SelectList(_context.Users, "Id", "Id", transaction.RequesterId);
            ViewData["TargetItemId"] = new SelectList(_context.PhysicalItems, "Id", "Id", transaction.TargetItemId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequesterId,TargetItemId,Status,CreatedAt")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["RequesterId"] = new SelectList(_context.Users, "Id", "Id", transaction.RequesterId);
            ViewData["TargetItemId"] = new SelectList(_context.PhysicalItems, "Id", "Id", transaction.TargetItemId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Requester)
                .Include(t => t.TargetItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
