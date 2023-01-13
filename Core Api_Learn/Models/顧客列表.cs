using System;
using System.Collections.Generic;

namespace Core_Api_Learn.Models
{
    public partial class 顧客列表
    {
        public int 顧客id { get; set; }
        public string 會員帳號 { get; set; } = null!;
        public string 會員密碼 { get; set; } = null!;
        public string 會員類型 { get; set; } = null!;
        public int? 認證 { get; set; }
        public string? 驗證碼 { get; set; }
        public string 顧客名稱 { get; set; } = null!;
        public string? 聯絡人 { get; set; }
        public string? 統一編號 { get; set; }
        public string? 聯絡電話 { get; set; }
        public string? 地址 { get; set; }
        public string? 機構代碼 { get; set; }
        public string? 銀行帳號 { get; set; }
        public string? 信用額度 { get; set; }
        public string? 信用評等 { get; set; }
        public string? 備註 { get; set; }
    }
}
