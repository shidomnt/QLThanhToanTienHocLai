using System;
using System.Collections.Generic;
using DBLib;
using DBLib.Attributes;
namespace QLThanhToanTienDayLai.ADO.Models
{ 

public partial class Nganh : Model
{
    public string Ma { get; set; }

    public string Ten { get; set; }

    public string MaKhoaHoc { get; set; }

}
}
