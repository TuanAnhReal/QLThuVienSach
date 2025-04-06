using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVienSach
{
    public class Users
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public Dictionary<string, DateTime> ThoiGianMuon { get; set; }
        public Dictionary<string, DateTime> ThoiGianTra { get; set; }
        public List<string> DanhSachDaMuon { get; set; }
        public List<string> DanhSachDangMuon { get; set; }
        public bool TrangThaiViPham { get; set; }

        public Users(string maKH, string tenKH, List<string> danhSachDaMuon,
            List<string> danhSachDangMuon, Dictionary<string, DateTime> thoiGianMuon,
            Dictionary<string, DateTime> thoiGianTra, bool trangThaiViPham)
        {
            MaKH = maKH;
            TenKH = tenKH;
            DanhSachDaMuon = danhSachDaMuon;
            DanhSachDangMuon = danhSachDangMuon;
            ThoiGianMuon = thoiGianMuon;
            ThoiGianTra = thoiGianTra;
            TrangThaiViPham = trangThaiViPham;
        }
    }
}

