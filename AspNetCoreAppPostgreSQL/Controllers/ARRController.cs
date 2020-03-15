using AspNetCoreAppPostgreSQL.Infrastructure;
using AspNetCoreAppPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAppPostgreSQL.Controllers
{
    public class ARRController : Controller
    {
        private readonly TvShowsContext _context;

        public ARRController(TvShowsContext context)
        {
            _context = context;
        }

        // GET: ARR
        public async Task<IActionResult> Index()
        {
            var investment = await _context.Investments.FirstOrDefaultAsync();

            ViewBag.Investments = investment?.Investments;
            ViewBag.Metrics = await CalculateMetrics();

            return View(await _context.ARR.ToListAsync());
        }

        // GET: ARR/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARR/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Profit,Investments")] ARR tvShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var data = await _context.ARR.ToListAsync();

            var metrics = await CalculateMetrics();
            var message = metrics.IsSuccess
                ? $"Calculated ARR value: {metrics.Value.Value:#.000}"
                : $"Cannot calculate ARR value. Error: {metrics.ErrorMessage}";

            return new CsvStreamResult<ARR>(data, new ARRClassMap(), message)
            {
                FileDownloadName = "indicators.csv"
            };
        }

        private async Task<Metrics> CalculateMetrics()
        {
            var data = await _context.ARR.ToListAsync();
            var investment = await _context.Investments.FirstOrDefaultAsync();

            if (investment == null)
                return Metrics.Create("Investments value not specified");
            if (investment.Investments <=0)
                return Metrics.Create("Investments value cannot be less or equal to zero");

            var summ = data.Sum(x => x.Profit);
            var month = (double) ((double) data.Count / (double) 12);

            var result = (summ / month) / investment.Investments;

            return Metrics.Create(result);
        }
    }

    public class Metrics
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public double? Value { get; set; }

        public static Metrics Create(string error)
        {
            return new Metrics(false, error, null);
        }

        public static Metrics Create(double value)
        {
            return new Metrics(true, null, value);
        }

        private Metrics(bool isSuccess, string error, double? value)
        {
            IsSuccess = isSuccess;
            ErrorMessage = error;
            Value = value;
        }
    }
}
