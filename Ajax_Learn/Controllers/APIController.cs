using Ajax_Learn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Text;

namespace Ajax_Learn.Controllers
{
    public class APIController : Controller
    {

        private readonly DemoContext _db;
        private readonly IWebHostEnvironment _host;


        public APIController(DemoContext db, IWebHostEnvironment host)
        {   
            _db = db;
            _host = host;
        }

        public IActionResult Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Steve";
            }
            return Content($"Hello Ajax,我是{name}", "text/plain", Encoding.UTF8);
        }
        public IActionResult Create(Member m,IFormFile photo)
        {
            string s = "";
            if (string.IsNullOrEmpty(m.Name) || string.IsNullOrEmpty(m.Name) || string.IsNullOrEmpty(m.Name))
            {
                s = "資料請填寫完畢";
                //string path = Path.Combine(_host.WebRootPath, "photos", "OIP.jpg");
                //System.IO.File.Delete(path);
            }
            else
            {
                s = $"姓名:{m.Name}<br>";
                s += $"Email:{m.Email}<br>";
                s += $"年齡:{m.Age}<br>";               

                //檔案(圖片)存放
                if (photo != null)
                {
                    //存放檔案(圖片)路徑
                    string path = Path.Combine(_host.WebRootPath, "photos", photo.FileName);
                    using (var filesStream = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(filesStream);                        
                    }
                    m.FileName = photo.FileName;

                    //將圖片轉成二進位
                    byte[]? imgByte = null;
                    using (var memoryStream = new MemoryStream())
                    {
                        photo.CopyTo(memoryStream);
                        imgByte = memoryStream.ToArray();
                    }
                    m.FileData = imgByte;

                    //以下只是檔案資訊，不一定要傳
                    s += $"檔案名稱:{photo.FileName}<br>";
                    float len = Convert.ToInt32(photo.Length) / 1024;
                    string data = len.ToString("###,##0.00");
                    s += $"檔案大小:{data}KB<br>";
                    s += $"檔案類型:{photo.ContentType}";
                }

                //_db.Add(m);
                //_db.SaveChanges();

            }
            return Content(s, "text/html", Encoding.UTF8);
        }

        public IActionResult City()
        {
            var cities = _db.Addresses.Select(e => new
            {
                e.City,
            }).Distinct();
            return Json(cities);
        }
        public IActionResult Site(string city)
        {
            var sites = _db.Addresses.Where(e=>e.City==city).Select(e => new
            {
                e.SiteId,
            }).Distinct();
            return Json(sites);
        }
        public IActionResult Road(string site)
        {
            var roads = _db.Addresses.Where(e => e.SiteId == site).Select(e => new
            {
                e.Road,
            }).Distinct();
            return Json(roads);
        }

    }
}
