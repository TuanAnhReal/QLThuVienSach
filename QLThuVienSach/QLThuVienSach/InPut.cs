// InPut.cs - Class để đọc dữ liệu từ file
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QLThuVienSach
{
    public class InPut
    {
        public List<Books> DocFileBooks()
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

        public List<Users> DocFileUsers()
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
                        if (parts.Length >= 9)
                        {
                            string maKH = parts[0].Trim();
                            string tenKH = parts[1].Trim();
                            string sdt = parts[2].Trim();
                            string diaChi = parts[3].Trim();

                            // Danh sách đã mượn
                            var danhSachDaMuon = parts[4].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim()).ToList();

                            // Danh sách đang mượn
                            var danhSachDangMuon = parts[5].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim()).ToList();

                            // Thời gian mượn
                            var thoiGianMuon = new Dictionary<string, DateTime>();
                            if (!string.IsNullOrWhiteSpace(parts[6]))
                            {
                                foreach (var entry in parts[6].Split(';'))
                                {
                                    var kvp = entry.Split(':');
                                    if (kvp.Length == 2 && DateTime.TryParse(kvp[1].Trim(), out DateTime ngayMuon))
                                    {
                                        thoiGianMuon.Add(kvp[0].Trim(), ngayMuon);
                                    }
                                }
                            }

                            // Thời gian trả
                            var thoiGianTra = new Dictionary<string, DateTime>();
                            if (!string.IsNullOrWhiteSpace(parts[7]))
                            {
                                foreach (var entry in parts[7].Split(';'))
                                {
                                    var kvp = entry.Split(':');
                                    if (kvp.Length == 2 && DateTime.TryParse(kvp[1].Trim(), out DateTime ngayTra))
                                    {
                                        thoiGianTra.Add(kvp[0].Trim(), ngayTra);
                                    }
                                }
                            }

                            // Trạng thái vi phạm
                            bool trangThaiViPham = bool.TryParse(parts[8].Trim(), out bool viPham) && viPham;

                            // Thêm user
                            danhSach.Add(new Users(
                                maKH, tenKH, sdt, diaChi,
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


        public List<Account> DocFileAccount()
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