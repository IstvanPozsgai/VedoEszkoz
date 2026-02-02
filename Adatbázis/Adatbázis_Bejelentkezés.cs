namespace VédőEszköz
{
    public static partial class Adatbázis_Létrehozás
    {
        private const string Jelszó = "ForgalmiUtasítás";

        public static void Adatbázis_Users(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);

            string szöveg = "CREATE TABLE Tábla_Users (" +
                "[UserId] INTEGER PRIMARY KEY AUTOINCREMENT," +
                "[UserName] TEXT(25)," +
                "[WinUserName] TEXT(25)," +
                "[Dolgozószám] TEXT(8)," +
                "[Password] TEXT(255)," +
                "[Dátum] TEXT," +
                "[Frissít] INTEGER," +
                "[Törölt] INTEGER," +
                "[Szervezetek] TEXT(255)," +
                "[Szervezet] TEXT(25)," +
                "[GlobalAdmin] INTEGER," +
                "[TelepAdmin] INTEGER)";

            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Tábla_Users");
        }

        public static void Adatbázis_Oldalak(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string szöveg = "CREATE TABLE Tábla_Oldalak (" +
                "[OldalId] INTEGER PRIMARY KEY AUTOINCREMENT," +
                "[FromName] TEXT(255)," +
                "[MenuName] TEXT(255)," +
                "[MenuFelirat] TEXT(255)," +
                "[Látható] INTEGER," +
                "[Törölt] INTEGER)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Tábla_Oldalak");
        }

        public static void Adatbázis_Gombok(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string szöveg = "CREATE TABLE Tábla_Gombok (" +
                "[GombokId] INTEGER PRIMARY KEY AUTOINCREMENT," +
                "[FromName] TEXT(255)," +
                "[GombName] TEXT(255)," +
                "[GombFelirat] TEXT(255)," +
                "[Szervezet] TEXT(255)," +
                "[Látható] INTEGER," +
                "[Törölt] INTEGER)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Tábla_Gombok");
        }

        public static void Adatbázis_Verzió(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string szöveg = "CREATE TABLE Tábla_Verzió (" +
                "[Id] INTEGER PRIMARY KEY AUTOINCREMENT," +
                "[Verzió] REAL)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Tábla_Verzió");
        }

        public static void Adatbázis_Jogosultság(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string szöveg = "CREATE TABLE Tábla_Jogosultság (" +
                "[UserId] INTEGER," +
                "[OldalId] INTEGER," +
                "[GombokId] INTEGER," +
                "[SzervezetId] INTEGER," +
                "[Törölt] INTEGER)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Tábla_Jogosultság");
        }
    }
}