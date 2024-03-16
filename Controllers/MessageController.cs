using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EquiMarketApp.Data;
using EquiMarketApp.Models;
using Microsoft.AspNetCore.Identity;
using EquiMarketApp.ViewModels;

namespace EquiMarketApp.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Message/Create
        public IActionResult Create(int conversationId)
        {
            var viewModel = new MessageCreateViewModel
            {
                ConversationId = conversationId
            };

            return View(viewModel);
        }

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    ConversationId = viewModel.ConversationId,
                    SenderId = _userManager.GetUserId(User),
                    MessageText = viewModel.MessageText,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Add(message);
                await _context.SaveChangesAsync();

                // Redirect to the conversation details view to see the new message
                return RedirectToAction("Details", "Conversation", new { id = viewModel.ConversationId });
            }

            return View(viewModel);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
