using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutomotiveCatalog.Data;
using AutomotiveCatalog.Models;

namespace AutomotiveCatalog.Controllers
{
    public class AutoPartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutoPartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AutoParts
        public async Task<IActionResult> Index()
        {
              return _context.AutoParts != null ? 
                          View(await _context.AutoParts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AutoParts'  is null.");
        }
        // GET: AutoParts/ShowSearchForm
        public async Task<IActionResult> ShowPartsSearchForm()
        {
            return View();
        }
        // Post: AutoParts/ShowSearchResults
        //Метод който, чрез подаване на стрингова променлива, намира всички части за този модел
        public async Task<IActionResult> ShowPartsSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.AutoParts.Where(j => j.VehicleFor.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: AutoParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AutoParts == null)
            {
                return NotFound();
            }

            var autoParts = await _context.AutoParts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autoParts == null)
            {
                return NotFound();
            }

            return View(autoParts);
        }

        // GET: AutoParts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Maker,Model,VehicleFor,Price")] AutoParts autoParts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autoParts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autoParts);
        }

        // GET: AutoParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AutoParts == null)
            {
                return NotFound();
            }

            var autoParts = await _context.AutoParts.FindAsync(id);
            if (autoParts == null)
            {
                return NotFound();
            }
            return View(autoParts);
        }

        // POST: AutoParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Maker,Model,VehicleFor,Price")] AutoParts autoParts)
        {
            if (id != autoParts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autoParts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoPartsExists(autoParts.Id))
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
            return View(autoParts);
        }

        // GET: AutoParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AutoParts == null)
            {
                return NotFound();
            }

            var autoParts = await _context.AutoParts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autoParts == null)
            {
                return NotFound();
            }

            return View(autoParts);
        }

        // POST: AutoParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AutoParts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AutoParts'  is null.");
            }
            var autoParts = await _context.AutoParts.FindAsync(id);
            if (autoParts != null)
            {
                _context.AutoParts.Remove(autoParts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoPartsExists(int id)
        {
          return (_context.AutoParts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
