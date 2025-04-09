using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions; // Thêm thư viện Regex

namespace QLThuVienSach
{
    public class AccountManager
    {
        private List<Account> accountList = new List<Account>();
        private LibraryAdmin libAdmin;

        public AccountManager(LibraryAdmin admin)
        {
            libAdmin = admin;
        }

        public List<Account> GetAccounts()
        {
            return accountList;
        }

        // Kiểm tra mật khẩu hợp lệ: ít nhất 8 ký tự, có chữ hoa, chữ thường và số
        private bool IsValidPassword(string matKhau)
        {
            if (matKhau.Length < 8) return false;
            if (!matKhau.Any(char.IsUpper)) return false;
            if (!matKhau.Any(char.IsLower)) return false;
            if (!matKhau.Any(char.IsDigit)) return false;
            return true;
        }
        // Phương thức kiểm tra tên hợp lệ (chỉ chứa chữ cái và khoảng trắng)
        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-ZÀ-Ỹà-ỹ\s]+$");
        }

        bool IsValidSDT(string sdt)
        {
            return sdt.Length == 10 && sdt.All(char.IsDigit) && sdt.StartsWith("0");
        }


        //Phương thức ẩn mật khẩu khi đăng nhập
        public string GetHiddenPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true); // Ẩn ký tự nhập vào

                if (key.Key == ConsoleKey.Enter) // Khi nhấn Enter -> Kết thúc nhập
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0) // Xóa ký tự cuối cùng
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b"); // Xóa dấu '*' trên màn hình
                }
                else if (!char.IsControl(key.KeyChar)) // Chỉ nhận ký tự chữ, số
                {
                    password.Append(key.KeyChar);
                    Console.Write("*"); // Hiển thị dấu '*'
                }

            } while (true);

            Console.WriteLine();
            return password.ToString();
        }

        // Tạo mã KH duy nhất từ danh sách người dùng trong LibAdmin
        private string GenerateUniqueMaKH()
        {
            string maKH;
            var users = libAdmin.GetUsers();
            Random random = new Random();
            do
            {
                maKH = "KH" + random.Next(100, 10000);
            } while (users.Any(u => u.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase)));
            return maKH;
        }

        public void RemoveAccount(string maKH)
        {
            //Khi lấy MaKH có thể đang là null, nên dùng "string.Equals" thay vì "a.MaKH.Equals"
            int removed = accountList.RemoveAll(a => string.Equals(a.MaKH, maKH, StringComparison.OrdinalIgnoreCase));

            if (removed > 0)
                Console.WriteLine("Xóa account thành công!");
            else
                Console.WriteLine("Không tìm thấy account!");
        }


        public void Register()
        {
            while (true)
            {
                Console.Write("Nhập tên đăng nhập: ");
                string tenDangNhap = Console.ReadLine();
                if (accountList.Any(a => a.TenDangNhap.Equals(tenDangNhap, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Tài khoản này đã tồn tại, hãy nhập tên khác.");
                    continue;
                }

                string matKhau;
                while (true)
                {
                    Console.Write("Nhập mật khẩu (ít nhất 8 ký tự, có chữ hoa, chữ thường và số): ");
                    matKhau = Console.ReadLine();
                    if (!IsValidPassword(matKhau))
                    {
                        Console.WriteLine("Lỗi: Mật khẩu không hợp lệ, vui lòng nhập lại.");
                    }
                    else break;
                }

                string tenKH;
                bool KQ = true;
                do
                {
                    Console.Write("Nhập họ và tên: ");
                    tenKH = Console.ReadLine();
                    char[] tenchar = tenKH.ToCharArray();
                    if(string.IsNullOrEmpty(tenKH))
                    {
                        Console.WriteLine("Lỗi: Tên khách hàng không hợp lệ, vui lòng nhập lại.");
                        KQ = false;
                    }
                    else if (tenchar.Any(c => Char.IsDigit(c)))
                    {
                        Console.WriteLine("Lỗi: Tên khách hàng không được chứa kí tự số, vui lòng nhập lại.");
                        KQ = false;
                    }
                    else if (!IsValidName(tenKH))
                    {
                        Console.WriteLine("Lỗi: Tên khách hàng không được chứa ký tự đặc biệt! Vui lòng nhập lại.");
                        KQ = false;
                    }
                    else
                    {
                        tenKH = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tenKH.ToLower());
                        KQ = true;
                    }
                } while (KQ == false);

                string sdt;//nhập sdt
                do
                {
                    Console.Write("Nhập số điện thoại (10 số, bắt đầu bằng 0): ");
                    sdt = Console.ReadLine().Trim();

                    if (!IsValidSDT(sdt))
                    {
                        Console.WriteLine("SĐT không hợp lệ! Vui lòng nhập lại.");
                    }

                } while (!IsValidSDT(sdt));


                Console.Write("Nhập địa chỉ: ");
                string diachi = Console.ReadLine();

                string maKH = GenerateUniqueMaKH();

                // Tạo user và thêm vào LibAdmin
                Users newUser = new Users(maKH, tenKH, sdt, diachi,
                    new List<string>(), new List<string>(),
                    new Dictionary<string, DateTime>(), new Dictionary<string, DateTime>(), false);
                libAdmin.AddUser(newUser);

                // Tạo tài khoản
                Account newAccount = new Account(tenDangNhap, matKhau, maKH, "User");
                accountList.Add(newAccount);

                Console.WriteLine("Đăng ký thành công! Mã KH của bạn: " + maKH);
                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
                break;
            }
        }

        public Account Login()
        {
            while (true)
            {
                Console.Write("Nhập tên đăng nhập: ");
                string tenDangNhap = Console.ReadLine();
                Console.Write("Nhập mật khẩu: ");
                string matKhau = GetHiddenPassword();

                var account = accountList.FirstOrDefault(a =>
                    a.TenDangNhap.Equals(tenDangNhap, StringComparison.OrdinalIgnoreCase) &&
                    a.MatKhau == matKhau);
                if (account != null)
                {
                    Console.WriteLine($"Đăng nhập thành công với vai trò: {account.LoaiTaiKhoan}");
                    return account;
                }
                else
                {
                    Console.WriteLine("Sai tên đăng nhập hoặc mật khẩu. Vui lòng thử lại.");
                }
            }
        }

        public void ForgotPassword()
        {
            Console.Clear();
            Console.WriteLine("===== LẤY LẠI MẬT KHẨU =====");
            Console.Write("Nhập tên đăng nhập: ");
            string tenDangNhap = Console.ReadLine().Trim();

            Console.Write("Nhập số điện thoại đã đăng ký: ");
            string sdt = Console.ReadLine().Trim();

            // Tìm tài khoản theo tên đăng nhập
            var account = accountList.FirstOrDefault(a => a.TenDangNhap.Equals(tenDangNhap, StringComparison.OrdinalIgnoreCase));

            if (account == null)
            {
                Console.WriteLine("Tài khoản không tồn tại!");
                return;
            }

            // Lấy user tương ứng với account đó
            var user = libAdmin.GetUsers().FirstOrDefault(u =>
                u.MaKH == account.MaKH && u.SDT == sdt);

            if (user == null)
            {
                Console.WriteLine("Thông tin số điện thoại không trùng khớp với tài khoản!");
                return;
            }

            Console.WriteLine("Xác minh thành công! Hãy đặt lại mật khẩu mới.\n");

            string newPassword;
            do
            {
                Console.Write("Nhập mật khẩu mới: ");
                newPassword = Console.ReadLine();
                if (!IsValidPassword(newPassword))
                {
                    Console.WriteLine("Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số.");
                }
            } while (!IsValidPassword(newPassword));

            account.MatKhau = newPassword;
            Console.WriteLine("Mật khẩu đã được cập nhật thành công!");
            Console.WriteLine("Nhấn Enter để tiếp tục....");
            Console.ReadLine();
        }


        // Phương thức khởi tạo sẵn tài khoản Admin và 1 vài tài khoản mặc định
        public void AddPredefinedAccount(Account account)
        {
            accountList.Add(account);
        }
    }
}

