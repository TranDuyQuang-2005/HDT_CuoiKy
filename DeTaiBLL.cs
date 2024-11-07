using QuanLyDeTaiNghienCuu_DAL;
using QuanLyDeTaiNghienCuu_DTO;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;  
using System.Text;
using System.Linq;

namespace QuanLyDeTaiNghienCuu_BLL
{
    public class DeTaiBLL
    {
        private DeTaiDAL deTaiDAL = new DeTaiDAL();
        private List<DeTaiNCKH> danhSachDeTai;
        public DeTaiBLL()
        {
            danhSachDeTai = deTaiDAL.DocDanhSachDeTai();
        }
        public void ThemDeTai(DeTaiNCKH deTai)
        {
            danhSachDeTai.Add(deTai);
            LuuDanhSachDeTai();
        }
        public void LuuDanhSachDeTai()
        {
            deTaiDAL.LuuDanhSachDeTai(danhSachDeTai);
        }
        public List<DeTaiNCKH> DocDanhSachDeTai()
        {
            return danhSachDeTai;
        }
        //FindAll: tìm kiếm trả về ds phần tử
        public List<DeTaiNCKH> TimKiemDeTaiTheoTen(string keyword)
        {
            return danhSachDeTai.FindAll(dt => dt.TenDeTai != null && dt.TenDeTai.Contains(keyword)); 
        }
        public List<DeTaiNCKH> TimKiemDeTaiTheoMaSo(string keyword)
        {
            return danhSachDeTai.FindAll(dt => dt.MaSo == keyword);
        }
        public List<DeTaiNCKH> TimKiemDeTaiTheoChuTri(string keyword)
        {
            return danhSachDeTai.FindAll(dt => dt.NguoiChuTri != null && dt.NguoiChuTri.Contains(keyword));
        }
        public List<DeTaiNCKH> TimKiemDeTaiTheoGiangVien(string keyword)
        {
            return danhSachDeTai.FindAll(dt => dt.GiangVienHuongDan != null && dt.GiangVienHuongDan.Contains(keyword));
        }
        public void CapNhatToanBoKinhPhi(double tiLeTang)
        {
            DeTaiDAL deTaiDAL = new DeTaiDAL();
            deTaiDAL.CapNhatKinhPhi(tiLeTang);
        }
        public List<DeTaiNCKH> LocDeTaiKinhPhiTren10Trieu()
        {
            return danhSachDeTai.FindAll(dt => dt.TinhKinhPhi() > 10000000);
        }
        public List<DeTaiNCKH> LocDeTaiLyThuyetApDungThucTe()
        {
            return danhSachDeTai.FindAll(dt => dt is DeTaiLyThuyet lyThuyet && lyThuyet.ApDungThucTe);
        }
        public List<DeTaiNCKH> LocDeTaiKinhTeSoCauHoiTren100()
        {
            return danhSachDeTai.FindAll(dt => dt is DeTaiKinhTe kinhTe && kinhTe.SoCauHoiKhaoSat > 100);
        }
        public List<DeTaiNCKH> LocDeTaiThoiGianTren4Thang()
        {
            return danhSachDeTai.FindAll(dt => (dt.ThoiGianKetThuc - dt.ThoiGianBatDau).TotalDays > 120);
        }
    }
}
