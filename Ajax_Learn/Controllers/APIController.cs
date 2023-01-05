using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Ajax_Learn.Controllers
{
    public class APIController : Controller
    {
        public IActionResult Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Hello Ajax";
            }
            return Content($"Hello Ajax,我是{name}", "text/plain", Encoding.UTF8);
        }
    }
}
