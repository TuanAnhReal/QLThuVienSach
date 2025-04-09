using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace QLThuVienSach
{
    public class Books
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public int NamXuatBan { get; set; }
        public int GiaSP { get; set; }
        public string TheLoai { get; set; }
        public int SoLuong { get; set; }
        public bool TrangThai { get; set; } // True nếu không cho mượn

        public Books(string maSach, string tenSach, string tacGia, int namXuatBan, int giaSP, string theLoai, int soLuong)
        {
            MaSach = maSach;
            TenSach = tenSach;
            TacGia = tacGia;
            NamXuatBan = namXuatBan;
            GiaSP = giaSP;
            TheLoai = theLoai;
            SoLuong = soLuong;
            TrangThai = (soLuong == 0);
        }

        public string GetFormattedTenSach()
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TenSach.ToLower());
        }

        public string GetFormattedTacGia()
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TacGia.ToLower());
        }

        public string GetFormattedTheLoai()
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TheLoai.ToLower());
        }
    }
}

