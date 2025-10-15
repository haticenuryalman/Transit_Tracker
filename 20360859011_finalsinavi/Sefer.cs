using System.Collections.Generic;
using _20360859011_finalsinavi;

internal class Sefer
{
    public int SeferID { get; set; }
    public string SeferNumarasi { get; set; }
    public string KalkisSehri { get; set; }
    public string VarisSehri { get; set; }
    public string KalkisSaati { get; set; }
    public List<Yolcu> Yolcular { get; set; } = new List<Yolcu>();
}