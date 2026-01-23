namespace VédőEszköz
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Adatbázis_Excel_Beolvasás(string hely)
        {
            string jelszó = "sajátmagam";
            string táblanév = "Tábla_Excel_Beolvasás";

            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            //Létrehozzuk az adatbázist és beállítunk jelszót


            string szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += " [csoport] CHAR(20), ";
            szöveg += " [oszlop] short,";
            szöveg += " [fejléc] CHAR(255),";
            szöveg += " [Státusz] yesno,";
            szöveg += " [Változónév] char(50))";

            //Létrehozzuk az adattáblát
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }

    }
}

