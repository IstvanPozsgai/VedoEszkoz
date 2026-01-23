namespace VédőEszköz
{
    public class Adat_Excel_Beolvasás
    {

        public string Csoport { get; private set; }
        public int Oszlop { get; private set; }
        public string Fejléc { get; private set; }
        public bool Státusz { get; private set; }
        public string Változónév { get; private set; }

        public Adat_Excel_Beolvasás(string csoport, int oszlop, string fejléc, bool státusz, string változónév)
        {
            Csoport = csoport;
            Oszlop = oszlop;
            Fejléc = fejléc;
            Státusz = státusz;
            Változónév = változónév;
        }
    }
}
