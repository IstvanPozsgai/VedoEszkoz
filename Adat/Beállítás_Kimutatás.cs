using System.Collections.Generic;

namespace VédőEszköz
{
    public class Beállítás_Kimutatás
    {
        /// <summary>
        /// Az adatok munkalapjának a neve
        /// </summary>
        public string Munkalapnév { get; set; }
        public string Balfelső { get; set; }
        public string Jobbalsó { get; set; }

        /// <summary>
        /// Hova lesz rakva a kimutatás a munkalapot nem hozza létre
        /// </summary>
        public string Kimutatás_Munkalapnév { get; set; }
        public string Kimutatás_cella { get; set; }
        public string Kimutatás_név { get; set; }
        public List<string> ÖsszesítNév { get; set; }
        public List<string> Összesítés_módja { get; set; }
        public List<string> SorNév { get; set; }
        public List<string> OszlopNév { get; set; }
        public List<string> SzűrőNév { get; set; }
    }
}
