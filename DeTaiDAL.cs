using QuanLyDeTaiNghienCuu_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace QuanLyDeTaiNghienCuu_DAL
{
    public class DeTaiDAL
    {
        string FilePath = "D:\\HDT_BaiNhomCuoiKy\\QuanLyDeTaiNghienCuu_GUI\\DanhSachDeTai.xml";
        public List<DeTaiNCKH> DocDanhSachDeTai()
        {
            List<DeTaiNCKH> danhSachDeTai = new List<DeTaiNCKH>();
            if (!File.Exists(FilePath))
                return danhSachDeTai;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            XmlNodeList nodeList = xmlDoc.SelectNodes("/DeTaiNCKHList/DeTaiNCKH");
            foreach (XmlNode node in nodeList)
            {
                DeTaiNCKH deTai = ChuyenDoiXMLSangDeTai(node);
                danhSachDeTai.Add(deTai);
            }
            return danhSachDeTai;
        }
        public void LuuDanhSachDeTai(List<DeTaiNCKH> danhSachDeTai)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("DeTaiNCKHList");
            xmlDoc.AppendChild(root);

            foreach (DeTaiNCKH deTai in danhSachDeTai)
            {
                XmlElement deTaiNode = xmlDoc.CreateElement("DeTaiNCKH");

                // Thêm thẻ LoaiDeTai
                XmlElement loaiDeTai = xmlDoc.CreateElement("LoaiDeTai");
                if (deTai is DeTaiLyThuyet)
                {
                    loaiDeTai.InnerText = "DeTaiLyThuyet";
                }
                else if (deTai is DeTaiKinhTe)
                {
                    loaiDeTai.InnerText = "DeTaiKinhTe";
                }
                else if (deTai is DeTaiCongNghe)
                {
                    loaiDeTai.InnerText = "DeTaiCongNghe";
                }
                deTaiNode.AppendChild(loaiDeTai);

                // Thêm thẻ MaSo
                XmlElement maSo = xmlDoc.CreateElement("MaSo");
                maSo.InnerText = deTai.MaSo;
                deTaiNode.AppendChild(maSo);

                // Thêm thẻ TenDeTai
                XmlElement tenDeTai = xmlDoc.CreateElement("TenDeTai");
                tenDeTai.InnerText = deTai.TenDeTai;
                deTaiNode.AppendChild(tenDeTai);

                // Thêm thẻ NguoiChuTri
                XmlElement nguoiChuTri = xmlDoc.CreateElement("NguoiChuTri");
                nguoiChuTri.InnerText = deTai.NguoiChuTri;
                deTaiNode.AppendChild(nguoiChuTri);

                // Thêm thẻ GiangVienHuongDan
                XmlElement giangVienHuongDan = xmlDoc.CreateElement("GiangVienHuongDan");
                giangVienHuongDan.InnerText = deTai.GiangVienHuongDan;
                deTaiNode.AppendChild(giangVienHuongDan);

                // Thêm thẻ ThoiGianBatDau
                XmlElement thoiGianBatDau = xmlDoc.CreateElement("ThoiGianBatDau");
                thoiGianBatDau.InnerText = deTai.ThoiGianBatDau.ToString("yyyy-MM-dd");
                deTaiNode.AppendChild(thoiGianBatDau);

                // Thêm thẻ ThoiGianKetThuc
                XmlElement thoiGianKetThuc = xmlDoc.CreateElement("ThoiGianKetThuc");
                thoiGianKetThuc.InnerText = deTai.ThoiGianKetThuc.ToString("yyyy-MM-dd");
                deTaiNode.AppendChild(thoiGianKetThuc);

                // Kiểm tra loại đề tài để thêm thông tin đặc biệt
                if (deTai is DeTaiLyThuyet lyThuyet)
                {
                    XmlElement apDungThucTe = xmlDoc.CreateElement("ApDungThucTe");
                    apDungThucTe.InnerText = lyThuyet.ApDungThucTe.ToString();
                    deTaiNode.AppendChild(apDungThucTe);
                }
                else if (deTai is DeTaiKinhTe kinhTe)
                {
                    XmlElement soCauHoiKhaoSat = xmlDoc.CreateElement("SoCauHoiKhaoSat");
                    soCauHoiKhaoSat.InnerText = kinhTe.SoCauHoiKhaoSat.ToString();
                    deTaiNode.AppendChild(soCauHoiKhaoSat);
                }
                else if (deTai is DeTaiCongNghe congNghe)
                {
                    XmlElement moiTruong = xmlDoc.CreateElement("MoiTruong");
                    moiTruong.InnerText = congNghe.MoiTruong;
                    deTaiNode.AppendChild(moiTruong);
                }

                root.AppendChild(deTaiNode);
            }

            xmlDoc.Save(FilePath);
        }





        private DeTaiNCKH ChuyenDoiXMLSangDeTai(XmlNode node)
        {
            // Kiểm tra nút loại đề tài có tồn tại hay không
            string loaiDeTai = node["LoaiDeTai"]?.InnerText;

            //if (string.IsNullOrEmpty(loaiDeTai))
            //{
            //    throw new Exception("Loại đề tài không hợp lệ hoặc không tồn tại.");
            //}

            DeTaiNCKH deTai = null;
            switch (loaiDeTai)
            {
                case "DeTaiLyThuyet":
                    deTai = new DeTaiLyThuyet
                    {
                        MaSo = node["MaSo"]?.InnerText,
                        TenDeTai = node["TenDeTai"]?.InnerText,
                        ApDungThucTe = bool.Parse(node["ApDungThucTe"]?.InnerText ?? "false"),
                        ThoiGianBatDau = DateTime.Parse(node["ThoiGianBatDau"]?.InnerText ?? DateTime.Now.ToString()),
                        ThoiGianKetThuc = DateTime.Parse(node["ThoiGianKetThuc"]?.InnerText ?? DateTime.Now.ToString()),
                        GiangVienHuongDan = node["GiangVienHuongDan"]?.InnerText,
                        NguoiChuTri = node["NguoiChuTri"]?.InnerText // Thêm dòng này
                    };
                    break;

                case "DeTaiKinhTe":
                    deTai = new DeTaiKinhTe
                    {
                        MaSo = node["MaSo"]?.InnerText,
                        TenDeTai = node["TenDeTai"]?.InnerText,
                        SoCauHoiKhaoSat = int.Parse(node["SoCauHoiKhaoSat"]?.InnerText ?? "0"),
                        ThoiGianBatDau = DateTime.Parse(node["ThoiGianBatDau"]?.InnerText ?? DateTime.Now.ToString()),
                        ThoiGianKetThuc = DateTime.Parse(node["ThoiGianKetThuc"]?.InnerText ?? DateTime.Now.ToString()),
                        GiangVienHuongDan = node["GiangVienHuongDan"]?.InnerText,
                        NguoiChuTri = node["NguoiChuTri"]?.InnerText // Thêm dòng này
                    };
                    break;

                case "DeTaiCongNghe":
                    deTai = new DeTaiCongNghe
                    {
                        MaSo = node["MaSo"]?.InnerText,
                        TenDeTai = node["TenDeTai"]?.InnerText,
                        MoiTruong = node["MoiTruong"]?.InnerText,
                        ThoiGianBatDau = DateTime.Parse(node["ThoiGianBatDau"]?.InnerText ?? DateTime.Now.ToString()),
                        ThoiGianKetThuc = DateTime.Parse(node["ThoiGianKetThuc"]?.InnerText ?? DateTime.Now.ToString()),
                        GiangVienHuongDan = node["GiangVienHuongDan"]?.InnerText,
                        NguoiChuTri = node["NguoiChuTri"]?.InnerText // Thêm dòng này
                    };
                    break;

                default:
                    throw new Exception("Loại đề tài không hợp lệ");
            }
            return deTai;
        }


        private XmlElement ChuyenDoiDeTaiSangXML(DeTaiNCKH deTai)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement deTaiNode = xmlDoc.CreateElement("DeTaiNCKH");

            // Thêm thẻ MaSo
            XmlElement maSoNode = xmlDoc.CreateElement("MaSo");
            maSoNode.InnerText = deTai.MaSo;
            deTaiNode.AppendChild(maSoNode);

            // Thêm thẻ TenDeTai
            XmlElement tenDeTaiNode = xmlDoc.CreateElement("TenDeTai");
            tenDeTaiNode.InnerText = deTai.TenDeTai;
            deTaiNode.AppendChild(tenDeTaiNode);

            // Thêm thẻ LoaiDeTai
            XmlElement loaiDeTaiNode = xmlDoc.CreateElement("LoaiDeTai");
            loaiDeTaiNode.InnerText = deTai.GetType().Name;
            deTaiNode.AppendChild(loaiDeTaiNode);

            // Thêm thẻ ThoiGianBatDau
            XmlElement thoiGianBatDauNode = xmlDoc.CreateElement("ThoiGianBatDau");
            thoiGianBatDauNode.InnerText = deTai.ThoiGianBatDau.ToString("yyyy-MM-dd");
            deTaiNode.AppendChild(thoiGianBatDauNode);

            // Thêm thẻ ThoiGianKetThuc
            XmlElement thoiGianKetThucNode = xmlDoc.CreateElement("ThoiGianKetThuc");
            thoiGianKetThucNode.InnerText = deTai.ThoiGianKetThuc.ToString("yyyy-MM-dd");
            deTaiNode.AppendChild(thoiGianKetThucNode);

            // Thêm thẻ GiangVienHuongDan
            XmlElement giangVienHuongDanNode = xmlDoc.CreateElement("GiangVienHuongDan");
            giangVienHuongDanNode.InnerText = deTai.GiangVienHuongDan;
            deTaiNode.AppendChild(giangVienHuongDanNode);

            // Thêm thẻ NguoiChuTri (Đây là phần bổ sung)
            XmlElement nguoiChuTriNode = xmlDoc.CreateElement("NguoiChuTri");
            nguoiChuTriNode.InnerText = deTai.NguoiChuTri;
            deTaiNode.AppendChild(nguoiChuTriNode);

            // Nếu là DeTaiLyThuyet, thêm thẻ ApDungThucTe
            if (deTai is DeTaiLyThuyet lyThuyet)
            {
                XmlElement apDungThucTeNode = xmlDoc.CreateElement("ApDungThucTe");
                apDungThucTeNode.InnerText = lyThuyet.ApDungThucTe.ToString();
                deTaiNode.AppendChild(apDungThucTeNode);
            }
            // Nếu là DeTaiKinhTe, thêm thẻ SoCauHoiKhaoSat
            else if (deTai is DeTaiKinhTe kinhTe)
            {
                XmlElement soCauHoiNode = xmlDoc.CreateElement("SoCauHoiKhaoSat");
                soCauHoiNode.InnerText = kinhTe.SoCauHoiKhaoSat.ToString();
                deTaiNode.AppendChild(soCauHoiNode);
            }
            // Nếu là DeTaiCongNghe, thêm thẻ MoiTruong
            else if (deTai is DeTaiCongNghe congNghe)
            {
                XmlElement moiTruongNode = xmlDoc.CreateElement("MoiTruong");
                moiTruongNode.InnerText = congNghe.MoiTruong;
                deTaiNode.AppendChild(moiTruongNode);
            }

            return deTaiNode;
        }
    }
}
