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

        public IActionResult verify(Member m)
        {
            string s = "";
            if (m.Name == null)
            {
                s = $@"<div class='alert-danger'>";
                s += "請輸入姓名";
                s += $@"</div>";
            }
            else
            {
                var datas = _db.Members.FirstOrDefault(t => t.Name == m.Name);
                if (datas != null)
                {
                    s = $@"<div class='alert-danger'>";
                    s += "此姓名已有人";
                    s += $@"</div>";
                }
                else
                {
                    s = $@"<div class='alert-success'>";
                    s += "可以使用";
                    s += $@"</div>";
                }
            }
            return Content(s,"text/html", Encoding.UTF8);
        }
        public IActionResult Create(Member m, IFormFile photo)
        {
            string s = "";
            if (m.Name==null || m.Email==null || m.Age==null)
            {
                s = "資料請填寫完畢";
                //string path = Path.Combine(_host.WebRootPath, "photos", "OIP.jpg");
                //System.IO.File.Delete(path);
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
                    //如果已經有存過圖片就刪除
                    string oldPath = Path.Combine(_host.WebRootPath, "photos", photo.FileName);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                    //存放檔案(圖片)路徑
                    string photoName = Guid.NewGuid().ToString() + ".png"; //圖片名稱用Guid方法取代
                    string path = Path.Combine(_host.WebRootPath, "photos", photoName);
                    using (var filesStream = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(filesStream);
                    }
                    m.FileName = photoName;

                    //以下只是檔案資訊，不一定要傳
                    s += $"<li class=\"list-group-item\">檔案名稱:{photoName}</li>";
                    float len = Convert.ToInt32(photo.Length) / 1024;
                    string data = len.ToString("###,##0.00");
                    s += $"<li class=\"list-group-item\">檔案大小:{data}KB</li>";
                    s += $"<li class=\"list-group-item\">檔案類型:{photo.ContentType}</li>";
                }
                s += "</ul>";
                _db.Add(m);
                _db.SaveChanges();
            }
            return Content(s, "text/html", Encoding.UTF8);
        }

    }
}
