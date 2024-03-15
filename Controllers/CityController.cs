using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EquiMarketApp.Data;
using EquiMarketApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace EquiMarketApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: City
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cities.Include(c => c.County);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: City/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.County)
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: City/Create
        public IActionResult Create()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "Name");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,Name,CountyId")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "Name", city.CountyId);
            return View(city);
        }

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "Name", city.CountyId);
            return View(city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,Name,CountyId")] City city)
        {
            if (id != city.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.CityId))
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
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "Name", city.CountyId);
            return View(city);
        }

        // GET: City/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.County)
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            // Check if any ad is associated with this city
            var associatedAds = await _context.Ads.AnyAsync(a => a.CityId == city.CityId);

            if (associatedAds)
            {
                TempData["ErrorMessage"] = "Radering är inte tillåtet eftersom det finns annonser associerade med denna stad.";
                return RedirectToAction(nameof(Index));
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.CityId == id);
        }
    }
}
