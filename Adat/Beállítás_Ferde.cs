
using static VédőEszköz.Enumok;

namespace VédőEszköz
{
    public class Beállítás_Ferde
    {
        /// <summary>
        /// Munkalap neve
        /// </summary>
        public string Munkalap { get; set; } = "";
        /// <summary>
        ///  Nyomtatási terület beállítása pl. $"a1:i{sor}"
        /// </summary>
        public string Terület { get; set; } = "";

        /// <summary>
        /// Ferde vonal iránya
        /// </summary>
        public bool Jobb { get; set; } = true;


        public KeretVastagsag Felső { get; set; } = KeretVastagsag.Alap;
        public KeretVastagsag Alsó { get; set; } = KeretVastagsag.Alap;
        public KeretVastagsag BalOldal { get; set; } = KeretVastagsag.Alap;
        public KeretVastagsag JobbOldal { get; set; } = KeretVastagsag.Alap;
    }
}
