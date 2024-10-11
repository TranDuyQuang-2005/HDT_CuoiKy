using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDeTaiNghienCuu_DTO
{
    public abstract class DeTaiNCKH
    {
        private string maSo;
        private string tenDeTai;
        private double kinhPhi;
        private string nguoiChuTri;
        private string giangVienHuongDan;
        private DateTime thoiGianBatDau;
        private DateTime thoiGianKetThuc;

        public string MaSo
        {
            get { return maSo; }
            set { maSo = value; }
        }

        public string TenDeTai
        {
            get { return tenDeTai; }
            set { tenDeTai = value; }
        }

        public double KinhPhi
        {
            get { return kinhPhi; }
            set { kinhPhi = value; }
        }

        public string NguoiChuTri
        {
            get { return nguoiChuTri; }
            set { nguoiChuTri = value; }
        }

        public string GiangVienHuongDan
        {
            get { return giangVienHuongDan; }
            set { giangVienHuongDan = value; }
        }

        public DateTime ThoiGianBatDau
        {
            get { return thoiGianBatDau; }
            set { thoiGianBatDau = value; }
        }

        public DateTime ThoiGianKetThuc
        {
            get { return thoiGianKetThuc; }
            set { thoiGianKetThuc = value; }
        }
        public abstract double TinhKinhPhi();
    }

    public class DeTaiLyThuyet : DeTaiNCKH
    {
        private bool _apDungThucTe;

        public bool ApDungThucTe
        {
            get { return _apDungThucTe; }
            set { _apDungThucTe = value; }
        }


        public override double TinhKinhPhi()
        {
            return ApDungThucTe ? 15000000 : 8000000;
        }
    }

    public interface I
    {
        double TinhPhiNghienCuu();
    }

    public class DeTaiKinhTe : DeTaiNCKH, I
    {
        private int soCauHoiKhaoSat;
        public int SoCauHoiKhaoSat
        {
            get { return soCauHoiKhaoSat; }
            set { soCauHoiKhaoSat = value; }
        }
        public override double TinhKinhPhi()
        {
            return SoCauHoiKhaoSat > 100 ? 12000000 : 7000000;
        }

        public  double TinhPhiNghienCuu()
        {
            return SoCauHoiKhaoSat > 100 ? SoCauHoiKhaoSat * 550 : SoCauHoiKhaoSat * 450;
        }
    }

    public class DeTaiCongNghe : DeTaiNCKH, I
    {
        private string moiTruong;

        public string MoiTruong
        {
            get { return moiTruong; }
            set { moiTruong = value; }
        }
        public override double TinhKinhPhi()
        {
            if (MoiTruong == "web" || MoiTruong == "mobile")
                return 15000000;
            else
                return 10000000;
        }

        public  double TinhPhiNghienCuu()
        {
            if (MoiTruong == "mobile")
                return 1000000;
            else if (MoiTruong == "web")
                return 800000;
            else
                return 500000;
        }
    }
}
