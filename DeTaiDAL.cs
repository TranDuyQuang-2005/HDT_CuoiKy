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

                XmlElement maSo = xmlDoc.CreateElement("MaSo");
                maSo.InnerText = deTai.MaSo;
                deTaiNode.AppendChild(maSo);

                XmlElement tenDeTai = xmlDoc.CreateElement("TenDeTai");
                tenDeTai.InnerText = deTai.TenDeTai;
                deTaiNode.AppendChild(tenDeTai);

                XmlElement nguoiChuTri = xmlDoc.CreateElement("NguoiChuTri");
                nguoiChuTri.InnerText = deTai.NguoiChuTri;
                deTaiNode.AppendChild(nguoiChuTri);

                XmlElement giangVienHuongDan = xmlDoc.CreateElement("GiangVienHuongDan");
                giangVienHuongDan.InnerText = deTai.GiangVienHuongDan;
                deTaiNode.AppendChild(giangVienHuongDan);

                XmlElement thoiGianBatDau = xmlDoc.CreateElement("ThoiGianBatDau");
                thoiGianBatDau.InnerText = deTai.ThoiGianBatDau.ToString("yyyy-MM-dd");
                deTaiNode.AppendChild(thoiGianBatDau);

                XmlElement thoiGianKetThuc = xmlDoc.CreateElement("ThoiGianKetThuc");
                thoiGianKetThuc.InnerText = deTai.ThoiGianKetThuc.ToString("yyyy-MM-dd");
                deTaiNode.AppendChild(thoiGianKetThuc);

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

                XmlElement kinhPhi = xmlDoc.CreateElement("KinhPhi");
                kinhPhi.InnerText = deTai.TinhKinhPhi().ToString(); // Lưu giá trị TinhKinhPhi
                deTaiNode.AppendChild(kinhPhi);

                root.AppendChild(deTaiNode);
            }

            xmlDoc.Save(FilePath);
        }
        public DeTaiNCKH ChuyenDoiXMLSangDeTai(XmlNode node)
        {

            string maSo = node["MaSo"]?.InnerText;
            string tenDeTai = node["TenDeTai"]?.InnerText;
            string giangVienHuongDan = node["GiangVienHuongDan"]?.InnerText;
            string nguoiChuTri = node["NguoiChuTri"]?.InnerText;
            DateTime thoiGianBatDau = DateTime.Parse(node["ThoiGianBatDau"]?.InnerText ?? DateTime.Now.ToString());
            DateTime thoiGianKetThuc = DateTime.Parse(node["ThoiGianKetThuc"]?.InnerText ?? DateTime.Now.ToString());
            double kinhPhi = double.Parse(node["KinhPhi"]?.InnerText);
            string loaiDeTai = node["LoaiDeTai"]?.InnerText;
            DeTaiNCKH deTai = null;

            switch (loaiDeTai)
            {
                case "DeTaiLyThuyet":
                    deTai = new DeTaiLyThuyet
                    {
                        MaSo = maSo,
                        TenDeTai = tenDeTai,
                        ApDungThucTe = bool.Parse(node["ApDungThucTe"]?.InnerText ?? "false"),
                        ThoiGianBatDau = thoiGianBatDau,
                        ThoiGianKetThuc = thoiGianKetThuc,
                        GiangVienHuongDan = giangVienHuongDan,
                        NguoiChuTri = nguoiChuTri,
                        KinhPhi = kinhPhi,
                    };
                    break;

                case "DeTaiKinhTe":
                    deTai = new DeTaiKinhTe
                    {
                        MaSo = maSo,
                        TenDeTai = tenDeTai,
                        SoCauHoiKhaoSat = int.Parse(node["SoCauHoiKhaoSat"]?.InnerText ?? "0"),
                        ThoiGianBatDau = thoiGianBatDau,
                        ThoiGianKetThuc = thoiGianKetThuc,
                        GiangVienHuongDan = giangVienHuongDan,
                        NguoiChuTri = nguoiChuTri,
                        KinhPhi = kinhPhi,
                    };
                    break;

                case "DeTaiCongNghe":
                    deTai = new DeTaiCongNghe
                    {
                        MaSo = maSo,
                        TenDeTai = tenDeTai,
                        MoiTruong = node["MoiTruong"]?.InnerText,
                        ThoiGianBatDau = thoiGianBatDau,
                        ThoiGianKetThuc = thoiGianKetThuc,
                        GiangVienHuongDan = giangVienHuongDan,
                        NguoiChuTri = nguoiChuTri,
                        KinhPhi = kinhPhi,
                    };
                    break;

                default:
                    throw new Exception("Loại đề tài không hợp lệ");
            }

            return deTai;
        }
        public XmlElement ChuyenDoiDeTaiSangXML(DeTaiNCKH deTai)//Them the
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

            XmlElement nguoiChuTriNode = xmlDoc.CreateElement("NguoiChuTri");
            nguoiChuTriNode.InnerText = deTai.NguoiChuTri;
            deTaiNode.AppendChild(nguoiChuTriNode);

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
            XmlElement kinhPhiNode = xmlDoc.CreateElement("KinhPhi");
            kinhPhiNode.InnerText = deTai.KinhPhi.ToString(); 
            deTaiNode.AppendChild(kinhPhiNode);
            return deTaiNode;
        }
        public void CapNhatKinhPhi(double tiLeTang)
        {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("D:\\HDT_BaiNhomCuoiKy\\QuanLyDeTaiNghienCuu_GUI\\DanhSachDeTai.xml");
                XmlNodeList deTaiNodes = xmlDoc.SelectNodes("/DeTaiNCKHList/DeTaiNCKH");
                double kinhPhiMoi;
                foreach (XmlNode deTaiNode in deTaiNodes)
                {
                    double kinhPhiHienTai = double.Parse(deTaiNode.SelectSingleNode("KinhPhi").InnerText);
                    kinhPhiMoi = kinhPhiHienTai * tiLeTang;
                    XmlNode kinhPhiNode = deTaiNode.SelectSingleNode("KinhPhi");
                    if (kinhPhiNode != null)
                    {
                        kinhPhiNode.InnerText = kinhPhiMoi.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Node KinhPhi không tồn tại!");
                    }
                }
                xmlDoc.Save("D:\\HDT_BaiNhomCuoiKy\\QuanLyDeTaiNghienCuu_GUI\\DanhSachDeTai.xml");
        }
    }
}
