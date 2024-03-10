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
using Microsoft.AspNetCore.Identity;

namespace EquiMarketApp.Controllers
{
    public class AdController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public AdController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Ad
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ads.Include(a => a.AdType).Include(a => a.Breed).Include(a => a.Location).Include(a => a.Origin).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.AdType)
                .Include(a => a.Breed)
                .Include(a => a.Location)
                .Include(a => a.Origin)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ad/Create
        public IActionResult Create()
        {
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "AdTypeId", "Name");
            ViewData["BreedId"] = new SelectList(_context.Breeds, "BreedId", "Name");
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name");
            ViewData["OriginId"] = new SelectList(_context.Origins, "OriginId", "Country");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Ad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdId,UserId,Title,Description,Price,BirthYear,Height,Name,Gender,IsApproved,BreedId,OriginId,AdTypeId,CityId")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "AdTypeId", "Name", ad.AdTypeId);
            ViewData["BreedId"] = new SelectList(_context.Breeds, "BreedId", "Name", ad.BreedId);
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", ad.CityId);
            ViewData["OriginId"] = new SelectList(_context.Origins, "OriginId", "Country", ad.OriginId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ad.UserId);
            return View(ad);
        }

        // GET: Ad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }

            if (ad.UserId != _userManager.GetUserId(User) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "AdTypeId", "Name", ad.AdTypeId);
            ViewData["BreedId"] = new SelectList(_context.Breeds, "BreedId", "Name", ad.BreedId);
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", ad.CityId);
            ViewData["OriginId"] = new SelectList(_context.Origins, "OriginId", "Country", ad.OriginId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ad.UserId);
            return View(ad);
        }

        // POST: Ad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdId,UserId,Title,Description,Price,BirthYear,Height,Name,Gender,IsApproved,BreedId,OriginId,AdTypeId,CityId")] Ad ad)
        {
            if (id != ad.AdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ad.UserId != _userManager.GetUserId(User) && !User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }

                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.AdId))
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
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "AdTypeId", "Name", ad.AdTypeId);
            ViewData["BreedId"] = new SelectList(_context.Breeds, "BreedId", "Name", ad.BreedId);
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", ad.CityId);
            ViewData["OriginId"] = new SelectList(_context.Origins, "OriginId", "Country", ad.OriginId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ad.UserId);
            return View(ad);
        }

        // GET: Ad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.AdType)
                .Include(a => a.Breed)
                .Include(a => a.Location)
                .Include(a => a.Origin)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (ad == null)
            {
                return NotFound();
            }

            if (ad.UserId != _userManager.GetUserId(User) && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(ad);
        }

        // POST: Ad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad != null)
            {
                _context.Ads.Remove(ad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(int id)
        {
            return _context.Ads.Any(e => e.AdId == id);
        }
    }
}
