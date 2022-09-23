using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4MCC.Models
{
    public class Karyawan
    {
        public int KaryawanID { get; set; }
        [DisplayName("Karyawan ID")]
        public string Nik { get; set; }
        public string Nama { get; set; }

    }
}
