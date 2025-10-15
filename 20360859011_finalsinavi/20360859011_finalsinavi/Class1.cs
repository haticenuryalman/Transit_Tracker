using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20360859011_finalsinavi
{
    internal class Yolcu
    {
        internal static IEnumerable<Yolcu> yolcular;

        public string telefonnumarasi { get; set; }
        public string sefernumarasi { get; set; }
        public string koltuk_numarasi { get; set; }
        public string cinsiyet { get; set; }
        public object Cinsiyet { get; internal set; }

        public List<Yolcu> Seferler { get; set; } = new List<Yolcu>();
        public object KoltukNo { get; internal set; }
        public object Telefon { get; internal set; }
        public object Soyad { get; internal set; }
        public object Ad { get; internal set; }
    }


}
