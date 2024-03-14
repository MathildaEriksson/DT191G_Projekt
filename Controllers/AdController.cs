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
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string wwwRootPath;

        public AdController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            wwwRootPath = hostingEnvironment.WebRootPath;
        }

        // GET: Ad
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Ad> adsQuery;
            if (User.IsInRole("Admin"))
            {
                // Admin can see all ads
                adsQuery = _context.Ads.OrderByDescending(a => a.CreatedAt).Include(a => a.AdType).Include(a => a.Breed).Include(a => a.Location).Include(a => a.Origin).Include(a => a.User).Include(a => a.Images);
            }
            else
            {
                // Regular user can only see their own ads
                var userId = _userManager.GetUserId(User);
                adsQuery = _context.Ads.Where(a => a.UserId == userId).OrderByDescending(a => a.CreatedAt).Include(a => a.AdType).Include(a => a.Breed).Include(a => a.Location).Include(a => a.Origin).Include(a => a.User).Include(a => a.Images);
            }

            return View(await PaginatedList<Ad>.CreateAsync(adsQuery.AsNoTracking(), pageNumber, pageSize));
        }

        // GET: Approved ads
        public async Task<IActionResult> ApprovedAds(string searchString, int? adTypeId, int? breedId, Gender? gender, int? originId, int? minPrice, int? maxPrice, int? minHeight, int? maxHeight, int? minBirthyear, int? maxBirthyear, int? countyId, int pageNumber = 1, int pageSize = 6)
        {
            var approvedAdsQuery = _context.Ads.Where(a => a.IsApproved);

            if (!String.IsNullOrEmpty(searchString))
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.Title.Contains(searchString) || a.Description.Contains(searchString));
            }

            if (adTypeId.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.AdTypeId == adTypeId.Value);
            }

            if (breedId.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.BreedId == breedId.Value);
            }

            if (gender.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.Gender == gender.Value);
            }

            if (originId.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.OriginId == originId.Value);
            }

            if (minPrice.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.Price <= maxPrice.Value);
            }

            if (minHeight.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.Height >= minHeight.Value);
            }

            if (maxHeight.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.Height <= maxHeight.Value);
            }

            if (countyId.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.Location.CountyId == countyId.Value);
            }

            if (minBirthyear.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.BirthYear >= minBirthyear.Value);
            }

            if (maxBirthyear.HasValue)
            {
                approvedAdsQuery = approvedAdsQuery.Where(a => a.BirthYear <= maxBirthyear.Value);
            }

            approvedAdsQuery = approvedAdsQuery.OrderByDescending(a => a.CreatedAt)
                      .Include(a => a.AdType)
                      .Include(a => a.Breed)
                      .Include(a => a.Location)
                      .Include(a => a.Origin)
                      .Include(a => a.User)
                      .Include(a => a.Images);

            ViewBag.AdTypeId = new SelectList(_context.AdTypes, "AdTypeId", "Name", adTypeId);
            ViewBag.BreedId = new SelectList(_context.Breeds, "BreedId", "Name", breedId);
            ViewBag.OriginId = new SelectList(_context.Origins, "OriginId", "Country", originId);
            ViewBag.CountyId = new SelectList(_context.Counties, "CountyId", "Name", countyId);
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            ViewBag.CurrentSearchString = searchString;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.minBirthyear = minBirthyear;
            ViewBag.maxBirthyear = maxBirthyear;
            ViewBag.minHeight = minHeight;
            ViewBag.maxHeight = maxHeight;

            var approvedAds = await PaginatedList<Ad>.CreateAsync(approvedAdsQuery, pageNumber, pageSize);

            return View(approvedAds);
        }

        // GET: Unapproved ads
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> NonApprovedAds(int pageNumber = 1, int pageSize = 10)
        {
            var nonApprovedAdsQuery = _context.Ads.Where(a => !a.IsApproved)
                                                    .OrderByDescending(a => a.CreatedAt)
                                                   .Include(a => a.AdType)
                                                   .Include(a => a.Breed)
                                                   .Include(a => a.Location)
                                                   .Include(a => a.Origin)
                                                   .Include(a => a.User)
                                                   .Include(a => a.Images)
                                                   .AsNoTracking();

            var nonApprovedAds = await PaginatedList<Ad>.CreateAsync(nonApprovedAdsQuery, pageNumber, pageSize);

            return View(nonApprovedAds);
        }

        // GET: Ad/Details/5
        public async Task<IActionResult> Details(int? id, string returnUrl)
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
                .Include(a => a.Images)
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (ad == null)
            {
                return NotFound();
            }
            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index");
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
            ViewBag.CountyId = new SelectList(_context.Counties.OrderBy(c => c.Name), "CountyId", "Name");
            return View();
        }

        // POST: Ad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,BirthYear,Height,Name,Gender,IsApproved,BreedId,OriginId,AdTypeId,CityId")] Ad ad, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    ad.UserId = user.Id;
                }

                _context.Add(ad);
                await _context.SaveChangesAsync();

                // If there are images, save them to the database
                if (images != null)
                {
                    foreach (var image in images)
                    {
                        var uniqueFileName = GetUniqueFileName(image.FileName);
                        var uploadsFolder = Path.Combine(wwwRootPath, "images/ads");
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        var imageRecord = new Image
                        {
                            ImagePath = "images/ads/" + uniqueFileName,
                            AdId = ad.AdId
                        };
                        _context.Images.Add(imageRecord);
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "AdTypeId", "Name", ad.AdTypeId);
            ViewData["BreedId"] = new SelectList(_context.Breeds, "BreedId", "Name", ad.BreedId);
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", ad.CityId);
            ViewData["OriginId"] = new SelectList(_context.Origins, "OriginId", "Country", ad.OriginId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View(ad);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        // GET: Ad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Images)
                .Include(a => a.AdType)
                .Include(a => a.Breed)
                .Include(a => a.Location)
                .ThenInclude(l => l.County)
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

            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "AdTypeId", "Name", ad.AdTypeId);
            ViewData["BreedId"] = new SelectList(_context.Breeds, "BreedId", "Name", ad.BreedId);
            ViewData["CityId"] = new SelectList(_context.Cities.Where(c => c.CountyId == ad.Location.CountyId), "CityId", "Name", ad.CityId);
            ViewData["OriginId"] = new SelectList(_context.Origins, "OriginId", "Country", ad.OriginId);
            ViewBag.CountyId = new SelectList(_context.Counties.OrderBy(c => c.Name), "CountyId", "Name", ad.Location.CountyId);

            return View(ad);
        }

        // POST: Ad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdId,Title,Description,Price,BirthYear,Height,Name,Gender,IsApproved,BreedId,OriginId,AdTypeId,CityId")] Ad ad, List<IFormFile> newImages)
        {
            if (id != ad.AdId)
            {
                return NotFound();
            }

            var existingAd = await _context.Ads.AsNoTracking().FirstOrDefaultAsync(a => a.AdId == id);
            if (existingAd == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Keep current userID and images
                    ad.UserId = existingAd.UserId;
                    ad.Images = existingAd.Images;

                    // Check if user is owner of ad or admin
                    if (ad.UserId != _userManager.GetUserId(User) && !User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }

                    if (newImages != null && newImages.Count > 0)
                    {
                        foreach (var image in newImages)
                        {
                            var uniqueFileName = GetUniqueFileName(image.FileName);
                            var uploadsFolder = Path.Combine(wwwRootPath, "images/ads");
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream);
                            }

                            ad.Images.Add(new Image { ImagePath = "images/ads/" + uniqueFileName });
                        }
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
            var ad = await _context.Ads
                .Include(a => a.Images)
                .FirstOrDefaultAsync(a => a.AdId == id);

            if (ad != null)
            {
                // Check if ad has images and delete them
                if (ad.Images != null && ad.Images.Any())
                {
                    foreach (var image in ad.Images)
                    {
                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, image.ImagePath);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        _context.Images.Remove(image);
                    }
                }

                _context.Ads.Remove(ad);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Delete image from ad
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImage(int imageId, int adId)
        {
            var image = await _context.Images.FindAsync(imageId);

            if (image != null)
            {
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, image.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Edit), new { id = adId });
        }

        public async Task<IActionResult> GetCitiesByCountyId(int countyId)
        {
            var cities = await _context.Cities
                .Where(c => c.CountyId == countyId)
                .Select(c => new { c.CityId, c.Name })
                .ToListAsync();

            return Json(cities);
        }

        private bool AdExists(int id)
        {
            return _context.Ads.Any(e => e.AdId == id);
        }
    }
}
