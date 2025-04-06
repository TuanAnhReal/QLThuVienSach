using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QLThuVienSach
{
    public class OutPut
    {
        public void LuuDanhSachSach(List<Books> danhSach)
        {
            string filePath = @"..\..\dulieu\books.txt";

            try
            {
                var lines = danhSach.Select(book =>
                    $"{book.MaSach}|{book.TenSach}|{book.TacGia}|" +
                    $"{book.NamXuatBan}|{book.GiaSP}|{book.TheLoai}|{book.SoLuong}"
                ).ToList();

                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Lưu danh sách sách thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu file sách: {ex.Message}");
            }
        }

        public void LuuDanhSachNguoiDung(List<Users> danhSach)
        {
            string filePath = @"..\..\dulieu\users.txt";

            try
            {
                var lines = new List<string>();
                foreach (var user in danhSach)
                {
                    // Chuẩn hóa danh sách sách đã mượn/đang mượn
                    string daMuon = user.DanhSachDaMuon != null ? string.Join(",", user.DanhSachDaMuon) : "";
                    string dangMuon = user.DanhSachDangMuon != null ? string.Join(",", user.DanhSachDangMuon) : "";

                    // Chuẩn hóa thời gian mượn
                    string thoiGianMuonStr = "";
                    if (user.ThoiGianMuon != null && user.ThoiGianMuon.Count > 0)
                    {
                        thoiGianMuonStr = string.Join(";",
                            user.ThoiGianMuon.Select(kvp => $"{kvp.Key}:{kvp.Value:dd/MM/yyyy}"));
                    }

                    // Chuẩn hóa thời gian trả
                    string thoiGianTraStr = "";
                    if (user.ThoiGianTra != null && user.ThoiGianTra.Count > 0)
                    {
                        thoiGianTraStr = string.Join(";",
                            user.ThoiGianTra.Select(kvp => $"{kvp.Key}:{kvp.Value:dd/MM/yyyy}"));
                    }

                    lines.Add(
                        $"{user.MaKH}|{user.TenKH}|{daMuon}|{dangMuon}|{thoiGianMuonStr}|{thoiGianTraStr}|{user.TrangThaiViPham}"
                    );
                }

                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Lưu danh sách người dùng thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu file người dùng: {ex.Message}");
            }
        }

        public void LuuDanhSachTaiKhoan(List<Account> danhSach)
        {
            string filePath = @"..\..\dulieu\account.txt";

            try
            {
                var lines = danhSach.Select(acc =>
                    $"{acc.TenDangNhap}|{acc.MatKhau}|{acc.MaKH}|{acc.LoaiTaiKhoan}"
                ).ToList();

                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Lưu danh sách tài khoản thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu file tài khoản: {ex.Message}");
            }
        }
    }
}