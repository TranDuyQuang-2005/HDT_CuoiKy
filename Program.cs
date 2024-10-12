using System;
using System.Collections.Generic;
using QuanLyDeTaiNghienCuu_DTO;
using QuanLyDeTaiNghienCuu_BLL;
using System.Xml.Linq;


namespace QuanLyDeTaiNghienCuu_GUI
{
    class Program
    {
      
        static DeTaiBLL deTaiBLL = new DeTaiBLL();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string choice;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===========================================");
                Console.WriteLine("                MENU CHÍNH                 ");
                Console.WriteLine("===========================================");
                Console.WriteLine("  0. Load thông tin.");
                Console.WriteLine("  1. Xuất danh sách đề tài.");
                Console.WriteLine("  2. Thêm đề tài.");
                Console.WriteLine("  3. Tìm kiếm đề tài.");
                Console.WriteLine("  4. Xuất đề tài theo giảng viên.");
                Console.WriteLine("  5. Cập nhật kinh phí đề tài (tăng 10%).");
                Console.WriteLine("  6. Xuất danh sách đề tài có kinh phí > 10 triệu.");
                Console.WriteLine("  7. Xuất danh sách đề tài lý thuyết có áp dụng thực tế.");
                Console.WriteLine("  8. Xuất đề tài kinh tế có > 100 câu hỏi.");
                Console.WriteLine("  9. Xuất đề tài có thời gian thực hiện > 4 tháng.");
                Console.WriteLine(" 10. Thoát chương trình.");
                Console.WriteLine("===========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nNHẬP LỰA CHỌN: ");
                choice = Console.ReadLine();
                Console.ResetColor();
                switch (choice)
                {               
                    case "0":
                        List<DeTaiNCKH> danhSach = deTaiBLL.DocDanhSachDeTai();
                        deTaiBLL.LuuDanhSachDeTai();
                        break;
                    case "1":
                        XuatDanhSachDeTai();
                        break;
                    case "2":
                        ThemDeTai();
                        break;                     
                    case "3":
                        TimKiemDeTai();
                        break;
                    case "4":
                        XuatDeTaiTheoGiangVien();
                        break;
                    case "5":
                        CapNhatKinhPhi();
                        break;
                    case "6":
                        XuatDeTaiKinhPhiTren10Trieu();
                        break;
                    case "7":
                        XuatDeTaiLyThuyetApDungThucTe();
                        break;
                    case "8":
                        XuatDeTaiKinhTeSoCauHoiTren100();
                        break;
                    case "9":
                        XuatDeTaiThoiGianTren4Thang();
                        break;                      
                    case "10":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
        static void ThemDeTai()
        {
            Console.WriteLine("DANH SÁCH ĐỀ TÀI:");
            Console.WriteLine("\t1. Nghiên cứu lý thuyết");
            Console.WriteLine("\t2. Kinh tế");
            Console.WriteLine("\t3. Công nghệ");
            Console.Write("Nhập loại đề tài cần thêm:  ");
            string loaiDeTai = Console.ReadLine();
            DeTaiNCKH deTai = null;

            switch (loaiDeTai)
            {
                case "1":
                    deTai = new DeTaiLyThuyet();
                    break;
                case "2":
                    deTai = new DeTaiKinhTe();
                    break;
                case "3":
                    deTai = new DeTaiCongNghe();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    return;
            }

            Console.Write("Nhập mã số đề tài: ");
            deTai.MaSo = Console.ReadLine();

            Console.Write("Nhập tên đề tài: ");
            deTai.TenDeTai = Console.ReadLine();

            Console.Write("Nhập tên người chủ trì: ");
            deTai.NguoiChuTri = Console.ReadLine();

            Console.Write("Nhập tên giảng viên hướng dẫn: ");
            deTai.GiangVienHuongDan = Console.ReadLine();

            Console.Write("Nhập thời gian bắt đầu (yyyy-MM-dd): ");
            deTai.ThoiGianBatDau = DateTime.Parse(Console.ReadLine());

            DateTime thoiGianKetThuc;
            while (true)
            {
                Console.Write("Nhập thời gian kết thúc (yyyy-MM-dd): ");
                thoiGianKetThuc = DateTime.Parse(Console.ReadLine());
                if (thoiGianKetThuc >= deTai.ThoiGianBatDau)
                {
                    deTai.ThoiGianKetThuc = thoiGianKetThuc;
                    break; 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ngày kết thúc phải sau hoặc bằng ngày bắt đầu. Vui lòng nhập lại!");
                    Console.ResetColor();
                }
            }
            if (deTai is DeTaiLyThuyet lyThuyet)
            {
                Console.Write("Có áp dụng thực tế không? (true/false): ");
                lyThuyet.ApDungThucTe = bool.Parse(Console.ReadLine());
            }
            else if (deTai is DeTaiKinhTe kinhTe)
            {
                Console.Write("Nhập số câu hỏi khảo sát: ");
                kinhTe.SoCauHoiKhaoSat = int.Parse(Console.ReadLine());
            }
            else if (deTai is DeTaiCongNghe congNghe)
            {
                Console.Write("Nhập môi trường (web/mobile/window): ");
                congNghe.MoiTruong = Console.ReadLine();
            }

            deTaiBLL.ThemDeTai(deTai);
            Console.WriteLine("Đề tài đã được thêm thành công.");
        }
        static void XuatDanhSachDeTai()
        {
            DeTaiBLL deTaiBLL2 = new DeTaiBLL();
            List<DeTaiNCKH> danhSach = deTaiBLL2.DocDanhSachDeTai();
            foreach (var deTai in danhSach)
            {
                Console.WriteLine("Thông tin Đề tài Nghiên cứu:");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Mã số:                {deTai.MaSo}");
                Console.WriteLine($"Tên đề tài:           {deTai.TenDeTai}");
                Console.WriteLine($"Người chủ trì:        {deTai.NguoiChuTri}");
                Console.WriteLine($"Giảng viên hướng dẫn: {deTai.GiangVienHuongDan}");
                Console.WriteLine($"Kinh phí:             {deTai.KinhPhi}");
                Console.WriteLine($"Thời gian bắt đầu:    {deTai.ThoiGianBatDau.ToShortDateString()}");
                Console.WriteLine($"Thời gian kết thúc:   {deTai.ThoiGianKetThuc.ToShortDateString()}");
                if (deTai is DeTaiKinhTe deTaiKinhTe)
                {
                    Console.WriteLine($"Phí nghiên cứu:       {deTaiKinhTe.TinhPhiNghienCuu()}");
                }
                else if (deTai is DeTaiCongNghe deTaiCongNghe)
                {
                    Console.WriteLine($"Phí nghiên cứu:       {deTaiCongNghe.TinhPhiNghienCuu()}");
                }
                Console.WriteLine("--------------------------------------------------");
            }
        }

        static void TimKiemDeTai()
        {
            Console.Write("Nhập từ khóa tìm kiếm (tên đề tài/mã số/giảng viên/chủ trì): ");
            string keyword = Console.ReadLine();
            List<DeTaiNCKH> ketQua = deTaiBLL.TimKiemDeTaiTheoTen(keyword);

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy đề tài nào.");
            }
            else
            {
                foreach (var deTai in ketQua)
                {
                    string nguoiChuTri = deTai.NguoiChuTri ?? "Không có";
                    string giangVienHuongDan = deTai.GiangVienHuongDan ?? "Không có";
                    string thoiGianBatDau = deTai.ThoiGianBatDau != null ? deTai.ThoiGianBatDau.ToShortDateString() : "Chưa xác định";
                    string thoiGianKetThuc = deTai.ThoiGianKetThuc != null ? deTai.ThoiGianKetThuc.ToShortDateString() : "Chưa xác định";

                    Console.WriteLine("Thông tin Đề tài Nghiên cứu:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Mã số:                {deTai.MaSo}");
                    Console.WriteLine($"Tên đề tài:           {deTai.TenDeTai}");
                    Console.WriteLine($"Người chủ trì:        {deTai.NguoiChuTri}");
                    Console.WriteLine($"Giảng viên hướng dẫn: {deTai.GiangVienHuongDan}");
                    Console.WriteLine($"Kinh phí:             {deTai.KinhPhi}");
                    Console.WriteLine($"Thời gian bắt đầu:    {deTai.ThoiGianBatDau.ToShortDateString()}");
                    Console.WriteLine($"Thời gian kết thúc:   {deTai.ThoiGianKetThuc.ToShortDateString()}");
                    if (deTai is DeTaiKinhTe deTaiKinhTe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiKinhTe.TinhPhiNghienCuu()}");
                    }
                    else if (deTai is DeTaiCongNghe deTaiCongNghe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiCongNghe.TinhPhiNghienCuu()}");
                    }
                    Console.WriteLine("--------------------------------------------------");
                }
            }
        }
        static void XuatDeTaiTheoGiangVien()
        {
            Console.Write("Nhập tên giảng viên hướng dẫn: ");
            string tenGV = Console.ReadLine();
            List<DeTaiNCKH> ketQua = deTaiBLL.TimKiemDeTaiTheoGiangVien(tenGV);
            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy đề tài nào của giảng viên này.");
            }
            else
            {
                foreach (var deTai in ketQua)
                {
                    Console.WriteLine("Thông tin Đề tài Nghiên cứu:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Mã số:                {deTai.MaSo}");
                    Console.WriteLine($"Tên đề tài:           {deTai.TenDeTai}");
                    Console.WriteLine($"Người chủ trì:        {deTai.NguoiChuTri}");
                    Console.WriteLine($"Giảng viên hướng dẫn: {deTai.GiangVienHuongDan}");
                    Console.WriteLine($"Kinh phí:             {deTai.KinhPhi}");
                    Console.WriteLine($"Thời gian bắt đầu:    {deTai.ThoiGianBatDau.ToShortDateString()}");
                    Console.WriteLine($"Thời gian kết thúc:   {deTai.ThoiGianKetThuc.ToShortDateString()}");
                    if (deTai is DeTaiKinhTe deTaiKinhTe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiKinhTe.TinhPhiNghienCuu()}");
                    }
                    else if (deTai is DeTaiCongNghe deTaiCongNghe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiCongNghe.TinhPhiNghienCuu()}");
                    }
                    Console.WriteLine("--------------------------------------------------");
                }
            }
        }
        static void CapNhatKinhPhi()
        {
            deTaiBLL.CapNhatToanBoKinhPhi(1.1);
            Console.WriteLine("Đã cập nhật kinh phí của các đề tài lên 10%.");

        }
        static void XuatDeTaiKinhPhiTren10Trieu()
        {
            List<DeTaiNCKH> ketQua = deTaiBLL.LocDeTaiKinhPhiTren10Trieu();
            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không có đề tài nào có kinh phí > 10 triệu.");
            }
            else
            {
                foreach (var deTai in ketQua)
                {
                    Console.WriteLine("Thông tin Đề tài Nghiên cứu:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Mã số:                {deTai.MaSo}");
                    Console.WriteLine($"Tên đề tài:           {deTai.TenDeTai}");
                    Console.WriteLine($"Người chủ trì:        {deTai.NguoiChuTri}");
                    Console.WriteLine($"Giảng viên hướng dẫn: {deTai.GiangVienHuongDan}");
                    Console.WriteLine($"Kinh phí:             {deTai.KinhPhi}");
                    Console.WriteLine($"Thời gian bắt đầu:    {deTai.ThoiGianBatDau.ToShortDateString()}");
                    Console.WriteLine($"Thời gian kết thúc:   {deTai.ThoiGianKetThuc.ToShortDateString()}");
                    if (deTai is DeTaiKinhTe deTaiKinhTe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiKinhTe.TinhPhiNghienCuu()}");
                    }
                    else if (deTai is DeTaiCongNghe deTaiCongNghe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiCongNghe.TinhPhiNghienCuu()}");
                    }
                    Console.WriteLine("--------------------------------------------------");
                }
            }
        }
        static void XuatDeTaiLyThuyetApDungThucTe()
        {
            List<DeTaiNCKH> ketQua = deTaiBLL.LocDeTaiLyThuyetApDungThucTe();
            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không có đề tài lý thuyết nào có khả năng áp dụng thực tế.");
            }
            else
            {
                foreach (var deTai in ketQua)
                {
                    Console.WriteLine("Thông tin Đề tài Nghiên cứu:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Mã số:                {deTai.MaSo}");
                    Console.WriteLine($"Tên đề tài:           {deTai.TenDeTai}");
                    Console.WriteLine($"Người chủ trì:        {deTai.NguoiChuTri}");
                    Console.WriteLine($"Giảng viên hướng dẫn: {deTai.GiangVienHuongDan}");
                    Console.WriteLine($"Kinh phí:             {deTai.KinhPhi}");
                    Console.WriteLine($"Thời gian bắt đầu:    {deTai.ThoiGianBatDau.ToShortDateString()}");
                    Console.WriteLine($"Thời gian kết thúc:   {deTai.ThoiGianKetThuc.ToShortDateString()}");
                    if (deTai is DeTaiKinhTe deTaiKinhTe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiKinhTe.TinhPhiNghienCuu()}");
                    }
                    else if (deTai is DeTaiCongNghe deTaiCongNghe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiCongNghe.TinhPhiNghienCuu()}");
                    }
                    Console.WriteLine("--------------------------------------------------");
                }
            }
        }
        static void XuatDeTaiKinhTeSoCauHoiTren100()
        {
            List<DeTaiNCKH> ketQua = deTaiBLL.LocDeTaiKinhTeSoCauHoiTren100();
            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không có đề tài kinh tế nào có số câu hỏi khảo sát > 100.");
            }
            else
            {
                foreach (var deTai in ketQua)
                {
                    Console.WriteLine("Thông tin Đề tài Nghiên cứu:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Mã số:                {deTai.MaSo}");
                    Console.WriteLine($"Tên đề tài:           {deTai.TenDeTai}");
                    Console.WriteLine($"Người chủ trì:        {deTai.NguoiChuTri}");
                    Console.WriteLine($"Giảng viên hướng dẫn: {deTai.GiangVienHuongDan}");
                    Console.WriteLine($"Kinh phí:             {deTai.KinhPhi}");
                    Console.WriteLine($"Thời gian bắt đầu:    {deTai.ThoiGianBatDau.ToShortDateString()}");
                    Console.WriteLine($"Thời gian kết thúc:   {deTai.ThoiGianKetThuc.ToShortDateString()}");
                    if (deTai is DeTaiKinhTe deTaiKinhTe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiKinhTe.TinhPhiNghienCuu()}");
                    }
                    else if (deTai is DeTaiCongNghe deTaiCongNghe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiCongNghe.TinhPhiNghienCuu()}");
                    }
                    Console.WriteLine("--------------------------------------------------");
                }
            }
        }
        static void XuatDeTaiThoiGianTren4Thang()
        {
            List<DeTaiNCKH> ketQua = deTaiBLL.LocDeTaiThoiGianTren4Thang();
            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không có đề tài nào có thời gian thực hiện > 4 tháng.");
            }
            else
            {
                foreach (var deTai in ketQua)
                {
                    Console.WriteLine("Thông tin Đề tài Nghiên cứu:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Mã số:                {deTai.MaSo}");
                    Console.WriteLine($"Tên đề tài:           {deTai.TenDeTai}");
                    Console.WriteLine($"Người chủ trì:        {deTai.NguoiChuTri}");
                    Console.WriteLine($"Giảng viên hướng dẫn: {deTai.GiangVienHuongDan}");
                    Console.WriteLine($"Kinh phí:             {deTai.KinhPhi}");
                    Console.WriteLine($"Thời gian bắt đầu:    {deTai.ThoiGianBatDau.ToShortDateString()}");
                    Console.WriteLine($"Thời gian kết thúc:   {deTai.ThoiGianKetThuc.ToShortDateString()}");
                    if (deTai is DeTaiKinhTe deTaiKinhTe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiKinhTe.TinhPhiNghienCuu()}");
                    }
                    else if (deTai is DeTaiCongNghe deTaiCongNghe)
                    {
                        Console.WriteLine($"Phí nghiên cứu:       {deTaiCongNghe.TinhPhiNghienCuu()}");
                    }
                    Console.WriteLine("--------------------------------------------------");

                }
            }
        }
        static void DocDanhSachDeTai()
        {
            List<DeTaiNCKH> danhSach = deTaiBLL.DocDanhSachDeTai();
        }
    }
}
