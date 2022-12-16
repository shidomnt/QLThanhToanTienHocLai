using System;

namespace QLThanhToanTienDayLai.ADO.Models
{

    public partial class ThanhToan
    {
        public string Ma { get; set; }

        public DateTime Ngay { get; set; }

        public string MaKhoa { get; set; }

        public string MaMon { get; set; }

        public string MaGiaoVien { get; set; }

        public string MaDonGia { get; set; }

        public int SoCaCoiThi { get; set; }

        public int TienChamBai { get; set; }

        public int SoLop { get; set; }

        public int TienPhuDao { get; set; }

        public int TongTien { get; set; }

        public string TongTienBangChu { get; set; }

        public string MaTruongPhong { get; set; }

        public string MaNguoiLapPhieu { get; set; }

    }

}