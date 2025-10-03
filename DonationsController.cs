using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GiftOfTheGivers.Models;
using System;
using System.Threading.Tasks;

namespace GiftOfTheGivers.Controllers
{
    public class DonationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonationsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Donations/Create
        public IActionResult Create() => View();

        // POST: Donations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (!ModelState.IsValid) return View(donation);

            var user = await _userManager.GetUserAsync(User);
            donation.DonorId = user?.Id;

            donation.ReceiptNumber = $"GOTG-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(100, 999)}";

            _context.Add(donation);
            await _context.SaveChangesAsync();

            return RedirectToAction("ThankYou");
        }

        // GET: Donations/ThankYou
        public IActionResult ThankYou() => View();
    }
}

