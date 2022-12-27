using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLThanhToanTienDayLai.Attributes;

namespace QLThanhToanTienDayLai.ADO.Models
{
    public class CoSo : Model
    {
        public string Ma { get; set; }

        public string Ten { get; set; }

        public string DiaChi { get; set; }

        public string LienHe { get; set; }

        public string GhiChu { get; set; }
    }
}
