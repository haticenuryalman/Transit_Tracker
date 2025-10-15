using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20360859011_finalsinavi
{
    internal class Sefer
    {
        public static IEnumerable<Sefer> seferler { get; internal set; }
        public string sefernumarasi { get; set; }
        public string kalkis_sehri { get; set; }
        public string varis_sehri { get; set; }
        public string kalkis_saati { get; set; }
        public List<Yolcu> Yolcular { get; set; } = new List<Yolcu>();
        public List<Yolcu> Seferler { get; set; } = new List<Yolcu>();










    }
}

}

