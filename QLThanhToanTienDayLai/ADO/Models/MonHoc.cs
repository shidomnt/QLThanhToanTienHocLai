using System;
using System.Collections.Generic;
using DBLib;
using DBLib.Attributes;
namespace QLThanhToanTienDayLai.ADO.Models
{ 

    public partial class MonHoc : Model
    {
        
        public string Ma { get; set; }

        public string Ten { get; set; }

        public int SoGioLt { get; set; }

        public int SoGioTh { get; set; }

        public string MaKyHoc { get; set; }

    }
}
