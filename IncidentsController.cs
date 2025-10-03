using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGivers.Models;
using System.Threading.Tasks;

namespace GiftOfTheGivers.Controllers
{
    [Authorize] // require login for all actions (adjust later if needed)
    public class IncidentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncidentsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Incidents
        public async Task<IActionResult> Index()
        {
            var incidents = await _context.Incidents.Include(i => i.ReportedBy).ToListAsync();
            return View(incidents);
        }

        // GET: Incidents/Create
        public IActionResult Create() => View();

        // POST: Incidents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Incident incident)
        {
            if (!ModelState.IsValid) return View(incident);

            var user = await _userManager.GetUserAsync(User);
            incident.ReportedById = user.Id;

            _context.Add(incident);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}