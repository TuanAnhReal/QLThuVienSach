using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVienSach
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            // Gán dữ liệu vào LibraryAdmin và AccountManager
            var libAdmin = new LibraryAdmin();
            var accountManager = new AccountManager(libAdmin);
          
            // Khởi tạo đối tượng đọc/ghi file
            var importer = new InPut();
            var exporter = new OutPut();

            // Đọc dữ liệu khi khởi động chương trình
            var bookList = importer.DocDanhSachSach();
            var userList = importer.DocDanhSachNguoiDung();
            var accountList = importer.DocDanhSachTaiKhoan();

            // Thêm dữ liệu đọc được vào hệ thống
            bookList.ForEach(libAdmin.AddBook);
            userList.ForEach(libAdmin.AddUser);
            accountList.ForEach(accountManager.AddPredefinedAccount);

            LoginForm loginForm = new LoginForm(accountManager, libAdmin);
            loginForm.ShowLoginMenu();

            // Lưu dữ liệu khi thoát chương trình (đặt ở cuối chương trình)
            exporter.LuuDanhSachSach(libAdmin.GetBooks());
            exporter.LuuDanhSachNguoiDung(libAdmin.GetUsers());
            exporter.LuuDanhSachTaiKhoan(accountManager.GetAccounts());

        }
    }
}

