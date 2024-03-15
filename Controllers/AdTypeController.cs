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
    public class AdTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdType
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdTypes.ToListAsync());
        }

        // GET: AdType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adType = await _context.AdTypes
                .FirstOrDefaultAsync(m => m.AdTypeId == id);
            if (adType == null)
            {
                return NotFound();
            }

            return View(adType);
        }

        // GET: AdType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdTypeId,Name")] AdType adType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adType);
        }

        // GET: AdType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adType = await _context.AdTypes.FindAsync(id);
            if (adType == null)
            {
                return NotFound();
            }
            return View(adType);
        }

        // POST: AdType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdTypeId,Name")] AdType adType)
        {
            if (id != adType.AdTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdTypeExists(adType.AdTypeId))
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
            return View(adType);
        }

        // GET: AdType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adType = await _context.AdTypes
                .FirstOrDefaultAsync(m => m.AdTypeId == id);
            if (adType == null)
            {
                return NotFound();
            }

            return View(adType);
        }

        // POST: AdType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adType = await _context.AdTypes
                .Include(at => at.Ads) 
                .FirstOrDefaultAsync(m => m.AdTypeId == id);

            if (adType == null)
            {
                return NotFound();
            }

            if (adType.Ads.Any())
            {
                TempData["ErrorMessage"] = "Radering är inte tillåtet, det finns annonser av denna typ.";
                return RedirectToAction(nameof(Index));
            }

            _context.AdTypes.Remove(adType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdTypeExists(int id)
        {
            return _context.AdTypes.Any(e => e.AdTypeId == id);
        }
    }
}
