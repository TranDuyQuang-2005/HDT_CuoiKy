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
                XmlElement deTaiNode = xmlDoc.CreateElement("DeTaiNode");

                // Thêm thuộc tính vào node DeTaiNode
                deTaiNode.SetAttribute("MaSo", deTai.MaSo);
                deTaiNode.SetAttribute("TenDeTai", deTai.TenDeTai);
                deTaiNode.SetAttribute("KinhPhi", deTai.KinhPhi.ToString());

                // Thêm thuộc tính người chủ trì nếu có
                if (!string.IsNullOrWhiteSpace(deTai.NguoiChuTri))
                {
                    deTaiNode.SetAttribute("NguoiChuTri", deTai.NguoiChuTri);
                }

                deTaiNode.SetAttribute("GiangVienHuongDan", deTai.GiangVienHuongDan);
                deTaiNode.SetAttribute("ThoiGianBatDau", deTai.ThoiGianBatDau.ToString("yyyy-MM-ddTHH:mm:ss"));
                deTaiNode.SetAttribute("ThoiGianKetThuc", deTai.ThoiGianKetThuc.ToString("yyyy-MM-ddTHH:mm:ss"));

                // Thêm node vào root
                root.AppendChild(deTaiNode);
            }

            // Lưu file XML
            xmlDoc.Save(FilePath);
        }


        private DeTaiNCKH ChuyenDoiXMLSangDeTai(XmlNode node)
        {
            string loaiDeTai = node["LoaiDeTai"].InnerText;

            DeTaiNCKH deTai = null;
            switch (loaiDeTai)
            {
                case "DeTaiLyThuyet":
                    deTai = new DeTaiLyThuyet
                    {
                        MaSo = node["MaSo"].InnerText,
                        TenDeTai = node["TenDeTai"].InnerText,
                        ApDungThucTe = bool.Parse(node["ApDungThucTe"].InnerText),
                        ThoiGianBatDau = DateTime.Parse(node["ThoiGianBatDau"].InnerText),
                        ThoiGianKetThuc = DateTime.Parse(node["ThoiGianKetThuc"].InnerText),
                        GiangVienHuongDan = node["GiangVienHuongDan"].InnerText,
                    };
                    break;

                case "DeTaiKinhTe":
                    deTai = new DeTaiKinhTe
                    {
                        MaSo = node["MaSo"].InnerText,
                        TenDeTai = node["TenDeTai"].InnerText,
                        SoCauHoiKhaoSat = int.Parse(node["SoCauHoiKhaoSat"].InnerText),
                        ThoiGianBatDau = DateTime.Parse(node["ThoiGianBatDau"].InnerText),
                        ThoiGianKetThuc = DateTime.Parse(node["ThoiGianKetThuc"].InnerText),
                        GiangVienHuongDan = node["GiangVienHuongDan"].InnerText,
                    };
                    break;

                case "DeTaiCongNghe":
                    deTai = new DeTaiCongNghe
                    {
                        MaSo = node["MaSo"].InnerText,
                        TenDeTai = node["TenDeTai"].InnerText,
                        MoiTruong = node["MoiTruong"].InnerText,
                        ThoiGianBatDau = DateTime.Parse(node["ThoiGianBatDau"].InnerText),
                        ThoiGianKetThuc = DateTime.Parse(node["ThoiGianKetThuc"].InnerText),
                        GiangVienHuongDan = node["GiangVienHuongDan"].InnerText,
                    };
                    break;
            }
            return deTai;
        }
        private XmlElement ChuyenDoiDeTaiSangXML(DeTaiNCKH deTai)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement deTaiNode = xmlDoc.CreateElement("DeTaiNCKH");

            XmlElement maSoNode = xmlDoc.CreateElement("MaSo");
            maSoNode.InnerText = deTai.MaSo;
            deTaiNode.AppendChild(maSoNode);

            XmlElement tenDeTaiNode = xmlDoc.CreateElement("TenDeTai");
            tenDeTaiNode.InnerText = deTai.TenDeTai;
            deTaiNode.AppendChild(tenDeTaiNode);

            XmlElement loaiDeTaiNode = xmlDoc.CreateElement("LoaiDeTai");
            loaiDeTaiNode.InnerText = deTai.GetType().Name;
            deTaiNode.AppendChild(loaiDeTaiNode);

            XmlElement thoiGianBatDauNode = xmlDoc.CreateElement("ThoiGianBatDau");
            thoiGianBatDauNode.InnerText = deTai.ThoiGianBatDau.ToString("yyyy-MM-dd");
            deTaiNode.AppendChild(thoiGianBatDauNode);

            XmlElement thoiGianKetThucNode = xmlDoc.CreateElement("ThoiGianKetThuc");
            thoiGianKetThucNode.InnerText = deTai.ThoiGianKetThuc.ToString("yyyy-MM-dd");
            deTaiNode.AppendChild(thoiGianKetThucNode);

            XmlElement giangVienHuongDanNode = xmlDoc.CreateElement("GiangVienHuongDan");
            giangVienHuongDanNode.InnerText = deTai.GiangVienHuongDan;
            deTaiNode.AppendChild(giangVienHuongDanNode);

            // Thêm các thuộc tính khác tùy thuộc vào loại đề tài
            if (deTai is DeTaiLyThuyet lyThuyet)
            {
                XmlElement apDungThucTeNode = xmlDoc.CreateElement("ApDungThucTe");
                apDungThucTeNode.InnerText = lyThuyet.ApDungThucTe.ToString();
                deTaiNode.AppendChild(apDungThucTeNode);
            }
            else if (deTai is DeTaiKinhTe kinhTe)
            {
                XmlElement soCauHoiNode = xmlDoc.CreateElement("SoCauHoiKhaoSat");
                soCauHoiNode.InnerText = kinhTe.SoCauHoiKhaoSat.ToString();
                deTaiNode.AppendChild(soCauHoiNode);
            }
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
