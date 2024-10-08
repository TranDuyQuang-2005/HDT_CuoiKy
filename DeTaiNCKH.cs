using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDeTaiNghienCuu_DTO
{
    public class DeTaiNCKH
    {
        public string MaSo { get; set; }
        public string TenDeTai { get; set; }
        public double KinhPhi { get; set; }
        public string NguoiChuTri { get; set; }
        public string GiangVienHuongDan { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        public virtual double TinhKinhPhi()
        {
            return KinhPhi;
        }
    }

    public class DeTaiLyThuyet : DeTaiNCKH
    {
        public bool ApDungThucTe { get; set; }

        public override double TinhKinhPhi()
        {
            return ApDungThucTe ? 15000000 : 8000000;
        }
    }

    public class DeTaiKinhTe : DeTaiNCKH
    {
        public int SoCauHoiKhaoSat { get; set; }

        public override double TinhKinhPhi()
        {
            return SoCauHoiKhaoSat > 100 ? 12000000 : 7000000;
        }

        public double TinhPhiNghienCuu()
        {
            return SoCauHoiKhaoSat > 100 ? SoCauHoiKhaoSat * 550 : SoCauHoiKhaoSat * 450;
        }
    }

    public class DeTaiCongNghe : DeTaiNCKH
    {
        public string MoiTruong { get; set; }

        public override double TinhKinhPhi()
        {
            if (MoiTruong == "web" || MoiTruong == "mobile")
                return 15000000;
            else
                return 10000000;
        }

        public double TinhPhiNghienCuu()
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
