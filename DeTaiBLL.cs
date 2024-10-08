using QuanLyDeTaiNghienCuu_DAL;
using QuanLyDeTaiNghienCuu_DTO;
using System;
using System.Collections.Generic;

namespace QuanLyDeTaiNghienCuu_BLL
{
    public class DeTaiBLL
    {
        private DeTaiDAL deTaiDAL = new DeTaiDAL();
        private List<DeTaiNCKHDTO> danhSachDeTai;

        public DeTaiBLL()
        {
            danhSachDeTai = deTaiDAL.DocDanhSachDeTai();
        }

        public void ThemDeTai(DeTaiNCKHDTO deTai)
        {
            danhSachDeTai.Add(deTai);
            LuuDanhSachDeTai();
        }

        private void LuuDanhSachDeTai()
        {
            deTaiDAL.LuuDanhSachDeTai(danhSachDeTai);
        }

        public List<DeTaiNCKHDTO> DocDanhSachDeTai()
        {
            return danhSachDeTai;
        }

        public List<DeTaiNCKHDTO> TimKiemDeTaiTheoTen(string keyword)
        {
            return danhSachDeTai.FindAll(dt =>
                (dt.TenDeTai != null && dt.TenDeTai.Contains(keyword)) ||  // Kiểm tra null trước khi Contains
                dt.MaSo == keyword ||
                (dt.GiangVienHuongDan != null && dt.GiangVienHuongDan.Contains(keyword)) ||  // Kiểm tra null
                (dt.NguoiChuTri != null && dt.NguoiChuTri.Contains(keyword))  // Kiểm tra null
            );
        }

        public List<DeTaiNCKHDTO> TimKiemDeTaiTheoGiangVien(string tenGV)
        {
            return danhSachDeTai.FindAll(dt => dt.GiangVienHuongDan.Contains(tenGV));
        }

        public void CapNhatKinhPhiTang10PhanTram()
        {
            foreach (var deTai in danhSachDeTai)
            {
                // Gọi TinhKinhPhi để đảm bảo lấy giá trị kinh phí đúng theo logic
                double kinhPhiHienTai = deTai.TinhKinhPhi();

                // Tăng kinh phí lên 10%
                deTai.KinhPhi = kinhPhiHienTai * 1.1; // Cập nhật KinhPhi với 10% tăng thêm
            }

            //LuuDanhSachDeTai();
        }


        public List<DeTaiNCKHDTO> LocDeTaiKinhPhiTren10Trieu()
        {
            return danhSachDeTai.FindAll(dt => dt.TinhKinhPhi() > 10000000);
        }

        public List<DeTaiNCKHDTO> LocDeTaiLyThuyetApDungThucTe()
        {
            return danhSachDeTai.FindAll(dt => dt is DeTaiLyThuyet lyThuyet && lyThuyet.ApDungThucTe);
        }

        public List<DeTaiNCKHDTO> LocDeTaiKinhTeSoCauHoiTren100()
        {
            return danhSachDeTai.FindAll(dt => dt is DeTaiKinhTe kinhTe && kinhTe.SoCauHoiKhaoSat > 100);
        }

        public List<DeTaiNCKHDTO> LocDeTaiThoiGianTren4Thang()
        {
            return danhSachDeTai.FindAll(dt => (dt.ThoiGianKetThuc - dt.ThoiGianBatDau).TotalDays > 120);
        }
    }
}
