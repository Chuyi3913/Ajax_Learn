using Ajax_Learn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Ajax_Learn.Controllers
{
    public class 作業Controller : Controller
    {
        public IActionResult 作業1()
        {
            return View();
        }
        public IActionResult 作業2()
        {
            return View();
        }
        public IActionResult verify(Member m)
        {
            if (m.Name == null)
                m.Name = "";
            return Content(m.Name, "text/html", Encoding.UTF8);
        }
        public IActionResult 作業3()
        {
            return View();
        }
    }
}
