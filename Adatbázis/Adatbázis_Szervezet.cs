

namespace VédőEszköz

{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Szervezet(string hely)
        {
            string jelszó = "csavarhúzó";
            string táblanév = "Tábla_Szervezet";

            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            string szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[ID]  Short,";
            szöveg += "[Szervezet]  char (200),";
            szöveg += "[státus]  yesno)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, táblanév);
        }
    }
}
