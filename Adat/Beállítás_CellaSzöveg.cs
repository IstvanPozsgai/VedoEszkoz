using System.Collections.Generic;

namespace VédőEszköz
{
    public class Beállítás_CellaSzöveg
    {
        public string MunkalapNév { get; set; }
        public string Cella { get; set; } // pl. "B5"
        public string FullText { get; set; }
        public List<RichTextRun> Beállítások { get; set; } = new List<RichTextRun> { };
        public Beállítás_Betű Betű { get; set; }
    }

    public class RichTextRun
    {
        public int Start { get; set; }        // 0-alapú pozíció a szövegben
        public int Hossz { get; set; }       // hossz
        public bool Vastag { get; set; } = false;
        public bool Dőlt { get; set; } = false;
        public bool Aláhúzott { get; set; } = false;
    }

}
