using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVienSach
{
    public class LoginForm
    {
        private AccountManager accountManager;
        private LibraryAdmin libAdmin;
        public Account loggedAccount = null; //Lưu thông tin khi user đăng nhập 

        public LoginForm(AccountManager accManager, LibraryAdmin admin)
        {
            accountManager = accManager;
            libAdmin = admin;
        }

        public void ShowLoginMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Đăng nhập");
                Console.WriteLine("2. Đăng ký");
                Console.WriteLine("3. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        loggedAccount = accountManager.Login();
                        if (loggedAccount != null)
                        {                          
                            ShowMainMenu(loggedAccount);
                        }
                        break;
                    case 2:
                        accountManager.Register();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        private void ShowMainMenu(Account account)
        {
            if (account.LoaiTaiKhoan.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                ShowAdminMenu();
            }
            else
            {
                ShowUserMenu();
            }
        }

        private void ShowAdminMenu()// hiển thị menu cho Admin
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu Admin:");
                Console.WriteLine("1. Quản lý sách");
                Console.WriteLine("2. Quản lý khách hàng");
                Console.WriteLine("3. Đăng xuất");
                Console.Write("Chọn một tùy chọn: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        ManageBooks();
                        break;
                    case 2:
                        ManageUsers();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        private void ShowUserMenu()// hiển thị menu cho KH
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu User:");
                Console.WriteLine("1. Mượn sách");
                Console.WriteLine("2. Trả sách");
                Console.WriteLine("3. Xem thông tin cá nhân");
                Console.WriteLine("4. Đăng xuất");
                Console.Write("Chọn một tùy chọn: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        libAdmin.DisPlayBooks();
                        Console.Write("Nhập mã sách: ");
                        string maMuon = Console.ReadLine().ToUpper();
                        libAdmin.MuonSach(loggedAccount.MaKH, maMuon);
                        break;
                    case 2:
                        Console.Write("Nhập mã sách: ");
                        string maTra = Console.ReadLine().ToUpper();
                        libAdmin.TraSach(loggedAccount.MaKH, maTra);
                        break;
                    case 3:
                        libAdmin.ShowInfoForUser(loggedAccount.MaKH);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private void ManageBooks()//Khởi tạo menu QL sách cho Admin
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quản lý sách:");
                Console.WriteLine("1. Thêm sách");
                Console.WriteLine("2. Xóa sách");
                Console.WriteLine("3. Sửa sách");
                Console.WriteLine("4. Tìm kiếm sách");
                Console.WriteLine("5. Hiển thị danh sách sách");
                Console.WriteLine("6. Quay lại");
                Console.Write("Chọn một tùy chọn: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        Console.Write("Nhập mã sách: ");
                        string maSach = Console.ReadLine().ToUpper();
                        Console.Write("Nhập tên sách: ");
                        string tenSach = Console.ReadLine();
                        Console.Write("Nhập tác giả: ");
                        string tacGia = Console.ReadLine();
                        Console.Write("Nhập năm xuất bản: ");
                        int namXB = int.Parse(Console.ReadLine());
                        Console.Write("Nhập giá sách: ");
                        int gia = int.Parse(Console.ReadLine());
                        Console.Write("Nhập thể loại: ");
                        string theLoai = Console.ReadLine();
                        Console.Write("Nhập số lượng: ");
                        int soLuong = int.Parse(Console.ReadLine());
                        Books book = new Books(maSach, tenSach, tacGia, namXB, gia, theLoai, soLuong);
                        libAdmin.AddBook(book);
                        break;
                    case 2:
                        libAdmin.RemoveBook();
                        break;
                    case 3:
                        libAdmin.EditBook();
                        break;
                    case 4:
                        libAdmin.SearchBook();
                        break;
                    case 5:
                        libAdmin.DisPlayBooks();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private void ManageUsers()//Khởi tạo menu QL KH cho Admin
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quản lý khách hàng:");
                Console.WriteLine("1. Thêm khách hàng");
                Console.WriteLine("2. Xóa khách hàng");
                Console.WriteLine("3. Sửa khách hàng");
                Console.WriteLine("4. Tìm kiếm khách hàng");
                Console.WriteLine("5. Hiển thị danh sách khách hàng");
                Console.WriteLine("6. Quay lại");
                Console.Write("Chọn một tùy chọn: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        Console.Write("Nhập mã khách hàng: ");
                        string maKH = Console.ReadLine().ToUpper();
                        Console.Write("Nhập tên khách hàng: ");
                        string tenKH = Console.ReadLine();
                        Users newUser = new Users(maKH, tenKH, new System.Collections.Generic.List<string>(),
                            new System.Collections.Generic.List<string>(),
                            new System.Collections.Generic.Dictionary<string, DateTime>(),
                            new System.Collections.Generic.Dictionary<string, DateTime>(), false);
                        libAdmin.AddUser(newUser);
                        break;
                    case 2:
                        Console.Write("Nhập mã khách hàng cần xóa: ");
                        string maXoa = Console.ReadLine().ToUpper();
                        libAdmin.RemoveUser(maXoa);
                        accountManager.RemoveAccount(maXoa);
                        break;
                    case 3:
                        Console.Write("Nhập mã khách hàng cần sửa: ");
                        string maSua = Console.ReadLine().ToUpper();
                        libAdmin.EditUser(maSua);
                        break;
                    case 4:
                        libAdmin.SearchUser();
                        break;
                    case 5:
                        libAdmin.DisPlayUsers();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }
    }
}


