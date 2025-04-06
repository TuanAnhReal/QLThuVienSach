using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVienSach
{
    public class Account
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string MaKH { get; set; }
        public string LoaiTaiKhoan { get; set; } // "Admin" hoặc "User"

        public Account(string tenDangNhap, string matKhau, string maKH, string loaiTaiKhoan)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            MaKH = maKH;
            LoaiTaiKhoan = loaiTaiKhoan;
        }
    }
}

