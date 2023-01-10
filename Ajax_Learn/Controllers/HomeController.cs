using Ajax_Learn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ajax_Learn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext  _db;

        public HomeController(ILogger<HomeController> logger,DemoContext db)
        {
            _logger = logger;
            _db =db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult fAjax()
        {
            return View();
        }
        public IActionResult pAjax()
        {
            return View();
        }
        public IActionResult json()
        {
            return View();
        }
        public IActionResult fetch()
        {
            return View();
        }

        public IActionResult DataMember()
        {
            var q = from m in _db.Members
                    select m;
            return View(q);
        }
        public IActionResult jQuery()
        {
            return View();
        }
        public IActionResult Partial()
        {
            ViewBag.W = "這裡是動態資料";
            return PartialView();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}