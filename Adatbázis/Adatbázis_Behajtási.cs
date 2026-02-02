namespace VédőEszköz
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Behajtási_Alap(string hely)
        {
            string jelszó = "egérpad";
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE Alapadatok (" +
                "[Id] INTEGER," +
                "[Adatbázisnév] TEXT(250)," +
                "[Sorszámbetűjele] TEXT(250)," +
                "[Sorszámkezdete] INTEGER," +
                "[Engedélyérvényes] TEXT," +
                "[Státus] INTEGER," +
                "[Adatbáziskönyvtár] TEXT(250))";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");

            szöveg = "CREATE TABLE Dolgozóktábla (" +
                "[Dolgozószám] TEXT(250)," +
                "[Dolgozónév] TEXT(250)," +
                "[Szervezetiegység] TEXT(250)," +
                "[Munkakör] TEXT(250)," +
                "[Státus] INTEGER)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Dolgozóktábla");
        }

        public static void Behajtási_Adatok(string hely)
        {
            string jelszó = "forgalmirendszám";
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE Alapadatok (" +
                "Sorszám TEXT(250)," +
                "Szolgálatihely TEXT," +
                "Hrazonosító TEXT," +
                "Név TEXT," +
                "Rendszám TEXT," +
                "Angyalföld_engedély INTEGER," +
                "Angyalföld_megjegyzés TEXT," +
                "Baross_engedély INTEGER," +
                "Baross_megjegyzés TEXT," +
                "Budafok_engedély INTEGER," +
                "Budafok_megjegyzés TEXT," +
                "Ferencváros_engedély INTEGER," +
                "Ferencváros_megjegyzés TEXT," +
                "Fogaskerekű_engedély INTEGER," +
                "Fogaskerekű_megjegyzés TEXT," +
                "Hungária_engedély INTEGER," +
                "Hungária_megjegyzés TEXT," +
                "Kelenföld_engedély INTEGER," +
                "Kelenföld_megjegyzés TEXT," +
                "Száva_engedély INTEGER," +
                "Száva_megjegyzés TEXT," +
                "Szépilona_engedély INTEGER," +
                "Szépilona_megjegyzés TEXT," +
                "Zugló_engedély INTEGER," +
                "Zugló_megjegyzés TEXT," +
                "Korlátlan TEXT," +
                "Autók_száma INTEGER," +
                "I_engedély INTEGER," +
                "II_engedély INTEGER," +
                "III_engedély INTEGER," +
                "Státus INTEGER," +
                "Dátum TEXT," +
                "Megjegyzés TEXT," +
                "PDF TEXT," +
                "OKA TEXT," +
                "érvényes TEXT)";

            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }
    }
}