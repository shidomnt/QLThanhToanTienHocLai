using System;
using System.Collections.Generic;
using DBLib;
using DBLib.Attributes;
namespace QLThanhToanTienDayLai.ADO.Models
{

    public partial class DonGia : Model
    {
        public string Ma { get; set; }

        public DateTime Ngay { get; set; }

        public int SoTienGioDay { get; set; }

        public double HeSoTh { get; set; }

        public double HeSoNgoaiGio { get; set; }

        public int CoiThi { get; set; }

        public int ChamThi { get; set; }

        public string GhiChu { get; set; }

    }
}