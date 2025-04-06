// InPut.cs - Class để đọc dữ liệu từ file
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QLThuVienSach
{
    public class InPut
    {
        public List<Books> DocDanhSachSach()
        {
            var danhSach = new List<Books>();
            string filePath = @"..\..\dulieu\books.txt";

            try
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split('|');
                        if (parts.Length == 7)
                        {
                            danhSach.Add(new Books(
                                parts[0].Trim(),
                                parts[1].Trim(),
                                parts[2].Trim(),
                                int.Parse(parts[3].Trim()),
                                int.Parse(parts[4].Trim()),
                                parts[5].Trim(),
                                int.Parse(parts[6].Trim())));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file sách: {ex.Message}");
            }

            return danhSach;
        }

        public List<Users> DocDanhSachNguoiDung()
        {
            var danhSach = new List<Users>();
            string filePath = @"..\..\dulieu\users.txt";

            try
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split('|');
                        if (parts.Length >= 4)
                        {
                            // Xử lý danh sách đã mượn và đang mượn
                            var danhSachDaMuon = parts[2].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim()).ToList();

                            var danhSachDangMuon = parts[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim()).ToList();

                            // Xử lý thời gian mượn
                            var thoiGianMuon = new Dictionary<string, DateTime>();
                            if (parts.Length > 4 && !string.IsNullOrWhiteSpace(parts[4]))
                            {
                                foreach (var entry in parts[4].Split(';'))
                                {
                                    if (!string.IsNullOrWhiteSpace(entry))
                                    {
                                        var kvp = entry.Split(':');
                                        if (kvp.Length == 2 && DateTime.TryParse(kvp[1].Trim(), out DateTime ngayMuon))
                                        {
                                            thoiGianMuon.Add(kvp[0].Trim(), ngayMuon);
                                        }
                                    }
                                }
                            }

                            // Xử lý thời gian trả
                            var thoiGianTra = new Dictionary<string, DateTime>();
                            if (parts.Length > 5 && !string.IsNullOrWhiteSpace(parts[5]))
                            {
                                foreach (var entry in parts[5].Split(';'))
                                {
                                    if (!string.IsNullOrWhiteSpace(entry))
                                    {
                                        var kvp = entry.Split(':');
                                        if (kvp.Length == 2 && DateTime.TryParse(kvp[1].Trim(), out DateTime ngayTra))
                                        {
                                            thoiGianTra.Add(kvp[0].Trim(), ngayTra);
                                        }
                                    }
                                }
                            }

                            // Xử lý trạng thái vi phạm
                            bool trangThaiViPham = parts.Length > 6 && bool.TryParse(parts[6].Trim(), out bool viPham)
                                ? viPham : false;

                            danhSach.Add(new Users(
                                parts[0].Trim(),
                                parts[1].Trim(),
                                danhSachDaMuon,
                                danhSachDangMuon,
                                thoiGianMuon,
                                thoiGianTra,
                                trangThaiViPham
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file người dùng: {ex.Message}");
            }

            return danhSach;
        }

        public List<Account> DocDanhSachTaiKhoan()
        {
            var danhSach = new List<Account>();
            string filePath = @"..\..\dulieu\account.txt";

            try
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split('|');
                        if (parts.Length == 4)
                        {
                            danhSach.Add(new Account(
                                parts[0].Trim(),
                                parts[1].Trim(),
                                parts[2].Trim(),
                                parts[3].Trim()
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file tài khoản: {ex.Message}");
            }

            return danhSach;
        }
    }
}