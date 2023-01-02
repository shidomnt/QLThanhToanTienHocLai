using System;
using System.Collections.Generic;
using DBLib;
using DBLib.Attributes;
namespace QLThanhToanTienDayLai.ADO.Models
{

    public partial class KhoaHoc : Model
    {
        public string Ma { get; set; }

        public string Ten { get; set; }

        public string MaKhoa { get; set; }

    }


}