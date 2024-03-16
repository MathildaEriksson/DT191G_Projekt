using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EquiMarketApp.Data;
using EquiMarketApp.Models;
using EquiMarketApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EquiMarketApp.Controllers
{
    public class ConversationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConversationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Conversation
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var applicationDbContext = _context.Conversations
                .Include(c => c.Ad)
                .Include(c => c.InitiatorUser)
                .Include(c => c.ReceiverUser)
                .Where(c => c.InitiatorUserId == userId || c.ReceiverUserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Conversation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var conversation = await _context.Conversations
                .Include(c => c.Ad)
                .Include(c => c.InitiatorUser)
                .Include(c => c.ReceiverUser)
                .Include(c => c.Messages.OrderBy(m => m.CreatedAt)) // Sortera meddelanden
                .FirstOrDefaultAsync(m => m.ConversationId == id);

            if (conversation == null || (conversation.InitiatorUserId != userId && conversation.ReceiverUserId != userId))
            {
                return NotFound();
            }

            return View(conversation);
        }

        // GET: Conversation/Create
        public IActionResult Create(int adId)
        {
            var ad = _context.Ads.FirstOrDefault(a => a.AdId == adId);
            if (ad == null)
            {
                return NotFound();
            }

            var receiverUserId = ad.UserId; // Create conversation with ad owner
            var viewModel = new ConversationCreateViewModel
            {
                AdId = adId,
                AdDescription = ad.Description,
                ReceiverUserId = receiverUserId
            };

            return View(viewModel);
        }

        // POST: Conversation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConversationCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conversation = new Conversation
                {
                    AdId = viewModel.AdId,
                    InitiatorUserId = _userManager.GetUserId(User),
                    ReceiverUserId = viewModel.ReceiverUserId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Conversation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            ViewData["AdId"] = new SelectList(_context.Ads, "AdId", "Description", conversation.AdId);
            ViewData["InitiatorUserId"] = new SelectList(_context.Users, "Id", "Id", conversation.InitiatorUserId);
            ViewData["ReceiverUserId"] = new SelectList(_context.Users, "Id", "Id", conversation.ReceiverUserId);
            return View(conversation);
        }

        // GET: Conversation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .Include(c => c.Ad)
                .Include(c => c.InitiatorUser)
                .Include(c => c.ReceiverUser)
                .FirstOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation != null)
            {
                _context.Conversations.Remove(conversation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.ConversationId == id);
        }
    }
}
