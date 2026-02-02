namespace VédőEszköz
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Adatbázis_Excel_Beolvasás(string hely)
        {
            string jelszó = "sajátmagam";
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE Tábla_Excel_Beolvasás (" +
                " [csoport] TEXT(20), " +
                " [oszlop] INTEGER," +
                " [fejléc] TEXT(255)," +
                " [Státusz] INTEGER," +
                " [Változónév] TEXT(50))";

            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }
    }
}