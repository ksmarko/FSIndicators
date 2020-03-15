using AspNetCoreAppPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAppPostgreSQL.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly TvShowsContext _context;

        public InvestmentsController(TvShowsContext context)
        {
            _context = context;
        }

        // GET: ARR
        public async Task<IActionResult> Index()
        {
            var investments = await _context.Investments.FirstOrDefaultAsync();
            return View(investments);
        }

        // GET: ARR/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARR/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Investments")] Investment tvShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }
    }
}
