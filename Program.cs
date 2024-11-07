using System;
using System.Collections.Generic;
using QuanLyDeTaiNghienCuu_DTO;
using QuanLyDeTaiNghienCuu_BLL;
using System.Xml.Linq;
using QuanLyDeTaiNghienCuu_GUI;
using System.Text;

namespace QuanLyDeTaiNghienCuu_GUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            GUI quanLyDeTaiGUI = new GUI();
            quanLyDeTaiGUI.HienThiMenu();
        }
    }
}
