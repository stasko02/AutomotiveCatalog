using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutomotiveCatalog.Data;
using AutomotiveCatalog.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutomotiveCatalog.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehiclecs
        public async Task<IActionResult> Index()
        {
            return _context.Vehiclecs != null ?
                        View(await _context.Vehiclecs.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Vehiclecs'  is null.");
        }
        // GET: Vehiclecs/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        // Post: Vehiclecs/ShowSearchResults
        //Метод който, чрез подаване на стрингова променлива, намира модел от Vehiclecs и подава нужната информация от листа с превозни средства
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Vehiclecs.Where(j => j.Model.Contains(SearchPhrase)).ToListAsync());
        }


        // GET: Vehiclecs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehiclecs == null)
            {
                return NotFound();
            }

            var vehiclecs = await _context.Vehiclecs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehiclecs == null)
            {
                return NotFound();
            }

            return View(vehiclecs);
        }

        // GET: Vehiclecs/Create
         
     
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehiclecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Maker,Model,ProductionYear,Displacement,Power,TopSpeed")] Vehicles vehiclecs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehiclecs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehiclecs);
        }

        // GET: Vehiclecs/Edit/5
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehiclecs == null)
            {
                return NotFound();
            }

            var vehiclecs = await _context.Vehiclecs.FindAsync(id);
            if (vehiclecs == null)
            {
                return NotFound();
            }
            return View(vehiclecs);
        }

        // POST: Vehiclecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Maker,Model,ProductionYear,Displacement,Power,TopSpeed")] Vehicles vehiclecs)
        {
            if (id != vehiclecs.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiclecs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclecsExists(vehiclecs.ID))
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
            return View(vehiclecs);
        }

        // GET: Vehiclecs/Delete/5
      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehiclecs == null)
            {
                return NotFound();
            }

            var vehiclecs = await _context.Vehiclecs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehiclecs == null)
            {
                return NotFound();
            }

            return View(vehiclecs);
        }

        // POST: Vehiclecs/Delete/5
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehiclecs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehiclecs'  is null.");
            }
            var vehiclecs = await _context.Vehiclecs.FindAsync(id);
            if (vehiclecs != null)
            {
                _context.Vehiclecs.Remove(vehiclecs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclecsExists(int id)
        {
          return (_context.Vehiclecs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
