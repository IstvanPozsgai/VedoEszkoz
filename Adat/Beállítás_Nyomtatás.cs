namespace VédőEszköz
{
    /// <summary>
    /// 
    /// </summary>
    public class Beállítás_Nyomtatás
    {
        /// <summary>
        /// Munkalap neve
        /// </summary>
        public string Munkalap { get; set; } = "";
        /// <summary>
        ///  Nyomtatási terület beállítása pl. $"a1:i{sor}"
        /// </summary>
        public string NyomtatásiTerület { get; set; } = "";
        /// <summary>
        /// Beállítja, hogy minden lapon szerepeljen formátum "$1:$1"
        /// </summary>
        public string IsmétlődőSorok { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string IsmétlődőOszlopok { get; set; } = "";
        /// <summary>
        ///  Nyomtatási beállítás Álló elhelyezkedés ha true
        /// </summary>
        public bool Álló { get; set; } = true;
        /// <summary>
        /// Automatikus 0, ... lap széles
        /// </summary>
        public int LapSzéles { get; set; } = 0;// Automatikus érték
        /// <summary>
        /// Automatikus 0, ... lap magas
        /// </summary>
        public int LapMagas { get; set; } = 0;
        /// <summary>
        /// Bal margó beállítása alap 15 mm
        /// </summary>
        public int? BalMargó { get; set; } = 15;
        /// <summary>
        /// Jobb margó beállítása alap 15 mm
        /// </summary>
        public int? JobbMargó { get; set; } = 15;
        /// <summary>
        /// Alsó margó beállítása alap 20 mm
        /// </summary>
        public int? AlsóMargó { get; set; } = 20;
        /// <summary>
        /// Felső margó beállítása alap 20 mm 
        /// </summary>
        public int? FelsőMargó { get; set; } = 20;
        /// <summary>
        /// Fejléc mérete lap 13 mm
        /// </summary>
        /// 
        public int? FejlécMéret { get; set; } = 13;
        /// <summary>
        /// Lábléc mérete lap 13 mm
        /// </summary>
        public int? LáblécMéret { get; set; } = 13;
        /// <summary>
        /// Papírméretet lehet beállítani
        /// </summary>
        /// <remarks>
        /// A4 az alapértelmezés, A3 vállasztható.
        /// Ha helytelen értéket kap akkor marad A4
        /// </remarks>
        public string Papírméret { get; set; } = "A4";
        /// <summary>
        /// 
        /// </summary>
        public string FejlécJobb { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string FejlécKözép { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string FejlécBal { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string LáblécJobb { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string LáblécKözép { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string LáblécBal { get; set; } = "";
        /// <summary>
        /// a sorszám után lévő részen megtöri az oldalt,
        /// 0 érték az alapértelmezett, ekkor az oldaltörés nem szabályozott    
        /// </summary>
        public int Oldaltörés { get; set; } = 0;
        /// <summary>
        /// Papríron elhelyezkedés alapérték false
        /// </summary>
        public bool VízKözép { get; set; } = false;
        /// <summary>
        /// Papríron elhelyezkedés alapérték false 
        /// </summary>
        public bool FüggKözép { get; set; } = false;
        /// <summary>
        /// Logó elhelyezése a fejlécben bal oldalra
        /// </summary>
        public string Képútvonal { get; set; } = "";
    }
}
