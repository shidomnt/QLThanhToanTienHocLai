﻿using System;
using System.Collections.Generic;
using DBLib;
using DBLib.Attributes;
namespace QLThanhToanTienDayLai.ADO.Models
{

    public partial class Khoa : Model
    {
        public string Ma { get; set; }

        public string Ten { get; set; }

        public string LienHe { get; set; }

        public string GhiChu { get; set; }

        public string MaCoSo { get; set; }

    }
}