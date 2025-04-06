using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace QLThuVienSach
{
    public class LibraryAdmin
    {
        private List<Books> bookList = new List<Books>();
        private List<Users> userList = new List<Users>();
        

        // Các phương thức BOOK
        public void AddBook(Books book)
        {
            if (bookList.Any(b => b.MaSach.Equals(book.MaSach, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Mã sách đã tồn tại!");
                return;
            }
            bookList.Add(book);
            Console.WriteLine("Thêm sách thành công!");
        }

        public void RemoveBook()
        {
            Console.Write("Nhập mã sách cần xóa: ");
            string maSach = Console.ReadLine().ToUpper();

            int removed = bookList.RemoveAll(b => b.MaSach.Equals(maSach, StringComparison.OrdinalIgnoreCase));

            if (removed > 0)
                Console.WriteLine("Xóa sách thành công!");
            else
                Console.WriteLine("Không tìm thấy sách!");
        }

        public void EditBook()
        {
            Console.Write("Nhập mã sách cần sửa: ");
            string maSach = Console.ReadLine().ToUpper();

            var book = bookList.FirstOrDefault(b => b.MaSach.Equals(maSach, StringComparison.OrdinalIgnoreCase));

            if (book != null)
            {
                Console.Write("Nhập tên sách mới: ");
                book.TenSach = Console.ReadLine();
                Console.Write("Nhập tác giả mới: ");
                book.TacGia = Console.ReadLine();
                Console.Write("Nhập năm xuất bản mới: ");
                book.NamXuatBan = int.Parse(Console.ReadLine());
                Console.Write("Nhập giá mới: ");
                book.GiaSP = int.Parse(Console.ReadLine());
                Console.Write("Nhập thể loại mới: ");
                book.TheLoai = Console.ReadLine();
                Console.Write("Nhập số lượng mới: ");
                book.SoLuong = int.Parse(Console.ReadLine());
                book.TrangThai = (book.SoLuong == 0);
                Console.WriteLine("Cập nhật sách thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sách!");
            }
        }

        public void SearchBook()
        {
            Console.WriteLine("\nTìm kiếm sách theo:");
            Console.WriteLine("1. Mã sách");
            Console.WriteLine("2. Tên sách");
            Console.WriteLine("3. Tên tác giả");
            Console.WriteLine("4. Thể loại sách");
            Console.Write("Chọn tiêu chí tìm kiếm: ");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 4)
            {
                Console.WriteLine("Lựa chọn không hợp lệ!");
                return;
            }

            Console.Write("Nhập từ khóa tìm kiếm: ");
            string keyword = Console.ReadLine().ToLower();

            var ketQua = new List<Books>();

            switch (option)
            {
                case 1:
                    ketQua = bookList.Where(b => b.MaSach.ToLower().Contains(keyword)).ToList();
                    break;
                case 2:
                    ketQua = bookList.Where(b => b.TenSach.ToLower().Contains(keyword)).ToList();
                    break;
                case 3:
                    ketQua = bookList.Where(b => b.TacGia.ToLower().Contains(keyword)).ToList();
                    break;
                case 4:
                    ketQua = bookList.Where(b => b.TheLoai.ToLower().Contains(keyword)).ToList();
                    break;
            }
            Console.Clear();
            if (ketQua.Count > 0)
            {
                Console.WriteLine("\nKết quả tìm kiếm:");
                Console.WriteLine(new string('-', 140));
                Console.WriteLine($"| {"Mã Sách",-8} | {"Tên Sách",-30} | {"Tác Giả",-20} | {"Năm XB",-6} | {"Giá (VND)",-10} | {"Thể Loại",-15} | {"Số Lượng",-8} | {"Trạng Thái",-18} |");
                Console.WriteLine(new string('-', 140));
                foreach (var book in ketQua)
                {
                    string trangThai = book.TrangThai ? "Không thể cho mượn" : "Có thể cho mượn";
                    Console.WriteLine($"| {book.MaSach,-8} | {book.TenSach,-30} | {book.TacGia,-20} | {book.NamXuatBan,-6} | {book.GiaSP,-10} | {book.TheLoai,-15} | {book.SoLuong,-8} | {trangThai,-18} |");
                    Console.WriteLine(new string('-', 140));
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy sách nào phù hợp!");
            }
        }

        // Các phương thức USER
        public void AddUser(Users user)
        {
            if (userList.Any(u => u.MaKH.Equals(user.MaKH, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Mã khách hàng đã tồn tại!");
                return;
            }
            userList.Add(user);
            Console.WriteLine("Thêm khách hàng thành công!");
        }

        public void RemoveUser(string maKH)
        {
            int removed = userList.RemoveAll(u => u.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase));
            if (removed > 0)
                Console.WriteLine("Xóa khách hàng thành công!");
            else
                Console.WriteLine("Không tìm thấy khách hàng!");
        }

        public void EditUser(string maKH)
        {
            var user = userList.FirstOrDefault(u => u.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                Console.Write("Nhập tên khách hàng mới: ");
                user.TenKH = Console.ReadLine();
                Console.WriteLine("Cập nhật thông tin khách hàng thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng!");
            }
        }

        public void SearchUser()//Tìm kiếm thông tin khách hàng 
        {
            Console.Write("\nNhập mã khách hàng hoặc tên khách hàng: ");
            string keyword = Console.ReadLine().ToLower();//Từ khóa cần thiết để so sánh là mã KH hay tên KH 

            var ketQua = userList.Where(u => u.MaKH.ToLower().Contains(keyword) || u.TenKH.ToLower().Contains(keyword)).ToList();
            Console.Clear();
            if (ketQua.Count > 0)
            {
                Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
                Console.WriteLine("                         THÔNG TIN KHÁCH HÀNG");
                Console.WriteLine("═══════════════════════════════════════════════════════════════");

                foreach (var user in ketQua)
                {
                    Console.WriteLine($"Mã KH          : {user.MaKH}");
                    Console.WriteLine($"Tên KH         : {user.TenKH}");
                    Console.WriteLine($"Đã mượn        : ");
                    //kết nối thời gian mượn và thời gian trả 
                    if (user.DanhSachDaMuon.Count > 0)
                    {
                        foreach (var maSach in user.DanhSachDaMuon)
                        {
                            if (user.ThoiGianMuon.ContainsKey(maSach) && user.ThoiGianTra.ContainsKey(maSach))
                            {
                                Console.WriteLine($"{"",-17}{maSach}: {user.ThoiGianMuon[maSach]:dd/MM/yyyy} - {user.ThoiGianTra[maSach]:dd/MM/yyyy}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{"",-17}Không có!");
                    }

                    Console.WriteLine($"Đang mượn      :");
                    if (user.DanhSachDangMuon.Count > 0)
                    {

                        foreach (var maSach in user.DanhSachDangMuon)//kết nối thời gian mượn
                        {
                            if (user.ThoiGianMuon.ContainsKey(maSach))
                            {
                                Console.WriteLine($"{"",-17}{maSach}: {user.ThoiGianMuon[maSach]:dd/MM/yyyy} - Chưa trả");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{"",-17}Không có!");
                    }
                    Console.WriteLine($"Trạng thái vi phạm: {(user.TrangThaiViPham ? "Có" : "Không")}");
                    Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng nào phù hợp!");
            }
        }

        public List<Users> GetUsers()
        {
            return userList;
        }

        public List<Books> GetBooks()
        {
            return bookList;
        }

        // Các phương thức mượn - trả sách
        public void MuonSach(string maKH, string maSach)
        {
            var user = userList.FirstOrDefault(u => u.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase));
            var book = bookList.FirstOrDefault(b => b.MaSach.Equals(maSach, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                Console.WriteLine("Không tìm thấy khách hàng!");
                return;
            }
            if (book == null)
            {
                Console.WriteLine("Không tìm thấy sách!");
                return;
            }
            if (user.DanhSachDangMuon.Contains(maSach))
            {
                Console.WriteLine("Bạn đã mượn sách này rồi!");
                return;
            }
            if (user.DanhSachDangMuon.Count >= 3)
            {
                Console.WriteLine("Số lượng sách mượn đã đạt giới hạn!");
                return;
            }
            if (book.SoLuong > 0)
            {
                user.DanhSachDangMuon.Add(maSach);
                user.ThoiGianMuon[maSach] = DateTime.Now;
                book.SoLuong--;
                book.TrangThai = (book.SoLuong == 0);
                Console.WriteLine("Mượn sách thành công!");
            }
            else
            {
                Console.WriteLine("Sách đã hết!");
            }
        }

        public void TraSach(string maKH, string maSach)
        {
            var user = userList.FirstOrDefault(u => u.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase));
            var book = bookList.FirstOrDefault(b => b.MaSach.Equals(maSach, StringComparison.OrdinalIgnoreCase));
            if (user == null || book == null)
            {
                Console.WriteLine("Không tìm thấy thông tin cần thiết!");
                return;
            }
            if (!user.DanhSachDangMuon.Contains(maSach))
            {
                Console.WriteLine("Sách không có trong danh sách mượn của bạn!");
                return;
            }
            user.DanhSachDangMuon.Remove(maSach);
            user.DanhSachDaMuon.Add(maSach);
            user.ThoiGianTra[maSach] = DateTime.Now;
            book.SoLuong++;
            book.TrangThai = false;
            Console.WriteLine("Trả sách thành công!");

            if (user.ThoiGianMuon.TryGetValue(maSach, out DateTime thoiGianMuon))
            {
                TimeSpan diff = DateTime.Now - thoiGianMuon;
                if (diff.TotalDays > 30)
                {
                    user.TrangThaiViPham = true;
                    Console.WriteLine($"Sách '{book.GetFormattedTenSach()}' bị trả trễ {diff.TotalDays - 30:N0} ngày!");
                }
            }
        }

        public void DisPlayBooks()
        {
            Console.WriteLine(new string(' ', 57) + " Danh Sách Các Quyển Sách " + new string(' ', 57));
            Console.WriteLine(new string('-', 140));
            Console.WriteLine($"| {"Mã Sách",-8} | {"Tên Sách",-30} | {"Tác Giả",-20} | {"Năm XB",-6} | {"Giá (VND)",-10} | {"Thể Loại",-15} | {"Số Lượng",-8} | {"Trạng Thái",-18} |");
            Console.WriteLine(new string('-', 140));

            foreach (var book in bookList)
            {
                string trangThai = book.SoLuong > 0 ? "Có thể cho mượn" : "Không thể cho mượn";
                Console.WriteLine($"| {book.MaSach,-8} | {book.TenSach,-30} | {book.TacGia,-20} | {book.NamXuatBan,-6} | {book.GiaSP,-10} | {book.TheLoai,-15} | {book.SoLuong,-8} | {trangThai,-18} |");
                Console.WriteLine(new string('-', 140));
            }
        }

        public void DisPlayUsers()
        {
            Console.WriteLine(new string('-', 20) + " Danh Sách Khách Hàng " + new string('-', 18));
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("| Ma KH  | Ten KH          | Đã Mượn | Đang Mượn | Vi Phạm  |");
            Console.WriteLine(new string('-', 60));
            foreach (var user in userList)
            {
                string trangThaiViPham = user.TrangThaiViPham ? "Có" : "Không";
                string danhSachDaMuon = string.Join(",", user.DanhSachDaMuon);
                string danhSachDangMuon = string.Join(",", user.DanhSachDangMuon);

                Console.WriteLine($"| {user.MaKH,-6} | {user.TenKH,-15} | {danhSachDaMuon,-7} | {danhSachDangMuon,-9} | {trangThaiViPham,-8} |");
                Console.WriteLine(new string('-', 60));
            }
        }

        public void ShowInfoForUser(string maKH)//Lấy thông tin của khách hàng khi muốn xem thông tin
        {
            var ketQua = userList.Where(u => !string.IsNullOrEmpty(u.MaKH) && u.MaKH.ToLower().Contains(maKH.ToLower())).ToList();
            //thay vì dùng câu điều kiện ở dưới thì dùng ở trên sẽ giúp kiểm soát các lỗi khi makh báo lỗi null 
            //var ketQua = userList.Where(u => u.MaKH.ToLower().Contains(maKH) || u.MaKH.ToLower().Contains(maKH.ToLower())).ToList();
            Console.Clear();
            if (ketQua.Count > 0)
            {
                Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
                Console.WriteLine("                         THÔNG TIN KHÁCH HÀNG");
                Console.WriteLine("═══════════════════════════════════════════════════════════════");

                foreach (var user in ketQua)
                {
                    Console.WriteLine($"Mã KH             : {user.MaKH}");
                    Console.WriteLine($"Tên KH            : {user.TenKH}");
                    Console.WriteLine($"Đã mượn           : ");
                    //kết nối thời gian mượn và thời gian trả 
                    if (user.DanhSachDaMuon.Count > 0)
                    {
                        foreach (var maSach in user.DanhSachDaMuon)
                        {
                            if (user.ThoiGianMuon.ContainsKey(maSach) && user.ThoiGianTra.ContainsKey(maSach))
                            {
                                Console.WriteLine($"{"",-17}{maSach}: {user.ThoiGianMuon[maSach]:dd/MM/yyyy} - {user.ThoiGianTra[maSach]:dd/MM/yyyy}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{"",-20}Không có!");
                    }

                    Console.WriteLine($"Đang mượn         :");
                    if (user.DanhSachDangMuon.Count > 0)
                    {

                        foreach (var maSach in user.DanhSachDangMuon)//kết nối thời gian mượn
                        {
                            if (user.ThoiGianMuon.ContainsKey(maSach))
                            {
                                Console.WriteLine($"{"",-20}{maSach}: {user.ThoiGianMuon[maSach]:dd/MM/yyyy} - Chưa trả");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{"",-20}Không có!");
                    }
                    Console.WriteLine($"Trạng thái vi phạm: {(user.TrangThaiViPham ? "Có" : "Không")}");
                    Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
                }
            }
        }
    }
}

