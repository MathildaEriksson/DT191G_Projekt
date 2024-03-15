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
    public class CountyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: County
        public async Task<IActionResult> Index()
        {
            return View(await _context.Counties.ToListAsync());
        }

        // GET: County/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties
                .FirstOrDefaultAsync(m => m.CountyId == id);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // GET: County/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: County/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountyId,Name")] County county)
        {
            if (ModelState.IsValid)
            {
                _context.Add(county);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(county);
        }

        // GET: County/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }
            return View(county);
        }

        // POST: County/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountyId,Name")] County county)
        {
            if (id != county.CountyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(county);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountyExists(county.CountyId))
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
            return View(county);
        }

        // GET: County/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties
                .FirstOrDefaultAsync(m => m.CountyId == id);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // POST: County/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var county = await _context.Counties
                .Include(c => c.Cities)
                .FirstOrDefaultAsync(m => m.CountyId == id);

            if (county == null)
            {
                return NotFound();
            }

            // Check if the county has any cities
            if (county.Cities.Any())
            {
                TempData["ErrorMessage"] = "Radering av län som har städer är inte tillåtet.";
                return RedirectToAction(nameof(Index));
            }

            // Check if any city within this county has any ads.
            foreach (var city in county.Cities)
            {
                var adsInCity = await _context.Ads.AnyAsync(ad => ad.CityId == city.CityId);
                if (adsInCity)
                {
                    TempData["ErrorMessage"] = "Radering av län som har annonser i dess städer är inte tillåtet.";
                    return RedirectToAction(nameof(Index));
                }
            }

            _context.Counties.Remove(county);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountyExists(int id)
        {
            return _context.Counties.Any(e => e.CountyId == id);
        }
    }
}
