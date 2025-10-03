using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGivers.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GiftOfTheGivers.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VolunteerController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Volunteer/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Volunteer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(VolunteerProfile model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            model.UserId = user.Id;

            _context.VolunteerProfiles.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }

        // GET: Volunteer/Profile
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.VolunteerProfiles
                .Include(v => v.Assignments)
                    .ThenInclude(a => a.Incident)
                .FirstOrDefaultAsync(v => v.UserId == user.Id);

            return View(profile);
        }

        // ADMIN: Approve volunteers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var volunteer = await _context.VolunteerProfiles.FindAsync(id);
            if (volunteer != null)
            {
                volunteer.Approved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AdminList");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminList()
        {
            var pending = _context.VolunteerProfiles
                .Include(v => v.User)
                .Where(v => !v.Approved)
                .ToList();
            return View(pending);
        }

        // Volunteer Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            var profile = await _context.VolunteerProfiles
                .Include(v => v.Assignments)
                    .ThenInclude(a => a.Incident)
                .FirstOrDefaultAsync(v => v.UserId == user.Id);

            return View(profile);
        }

        // OPTIONAL: Mark assignment completed
        [HttpPost]
        public async Task<IActionResult> MarkCompleted(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment != null)
            {
                assignment.Completed = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard");
        }

        // OPTIONAL: Update volunteer availability
        public async Task<IActionResult> EditAvailability()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.VolunteerProfiles.FirstOrDefaultAsync(v => v.UserId == user.Id);
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAvailability(VolunteerProfile model)
        {
            if (!ModelState.IsValid) return View(model);

            var profile = await _context.VolunteerProfiles.FindAsync(model.VolunteerProfileId);
            if (profile != null)
            {
                profile.Availability = model.Availability;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard");
        }

        // OPTIONAL: Upload proof of participation
        [HttpPost]
        public async Task<IActionResult> UploadProof(int assignmentId, IFormFile ProofFile)
        {
            var assignment = await _context.Assignments.FindAsync(assignmentId);
            if (assignment != null && ProofFile != null)
            {
                // Save file to wwwroot/uploads (ensure folder exists)
                var filePath = Path.Combine("wwwroot/uploads", ProofFile.FileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await ProofFile.CopyToAsync(stream);
                }
                assignment.ProofFilePath = "/uploads/" + ProofFile.FileName;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard");
        }
    }
}
