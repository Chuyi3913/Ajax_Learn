using Ajax_Learn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Ajax_Learn.Controllers
{
    public class HWAPIController : Controller
    {
        private readonly DemoContext _db;
        private readonly IWebHostEnvironment _host;

        public HWAPIController(DemoContext db, IWebHostEnvironment host)
        {
            _db = db;
            _host = host;
        }

        #region//作業2
       
        public IActionResult verify(Member m)
        {
            string s = "";
            if (m.Name == null)
                s = "null";
            else
            {
                var datas = _db.Members.Any(t => t.Name == m.Name);
                if (datas)
                    s = "false";                    
                else
                    s = "true";
            }
            return Content(s,"text/html", Encoding.UTF8);
        }
        public IActionResult Create(Member m, IFormFile photo)
        {
            //這裡如果使用FileReader可以直接在client端預覽，這樣回傳的方程式可以不必像下面這樣
            string s = "";   
            if (m.Name==null || m.Email==null || m.Age==null)
            {
                s = "資料有誤或未填寫完畢";         
            }
            else
            {
                s = $@"<div class='card' style='width: 18rem;'>";
                s += $@"<div class='card-header'>";
                s += "請確認以下資料";
                s += "</div>";
                s += "<ul class=\"list-group list-group-flush\">";
                s += $"<li class=\"list-group-item\">姓名:{m.Name}</li>";
                s += $"<li class=\"list-group-item\">Email:{m.Email}</li>";
                s += $"<li class=\"list-group-item\">年齡:{m.Age}</li>";

                //檔案(圖片)存放
                if (photo != null)
                {
                    //存放檔案(圖片)路徑
                    //圖片名稱用Guid方法取代
                    string photoName = Guid.NewGuid().ToString() + ".png"; 
                    string path = Path.Combine(_host.WebRootPath, "photos", photoName);
                    using (var filesStream = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(filesStream);
                    }
                    m.FileName = photoName;

                    //以下只是檔案資訊，不一定要傳
                    s += $"<li class=\"list-group-item\">檔案名稱:{photo.FileName}</li>";
                    float len = Convert.ToInt32(photo.Length) / 1024;
                    string data = len.ToString("###,##0.00");
                    s += $"<li class=\"list-group-item\">檔案大小:{data}KB</li>";
                    s += $"<li class=\"list-group-item\">檔案類型:{photo.ContentType}</li>";
                }
                s += "</ul></div>";
                _db.Add(m);
                _db.SaveChanges();              
            }           
            return Content(s, "text/html", Encoding.UTF8);
        }
        #endregion

        #region//作業3
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
            var sites = _db.Addresses.Where(e => e.City == city).Select(e => new
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
        #endregion
    }
}
