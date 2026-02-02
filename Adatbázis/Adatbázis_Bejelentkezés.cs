namespace VédőEszköz
{
    public static partial class Adatbázis_Létrehozás
    {
        private static bool IsSQLite(string hely) => hely.ToLower().EndsWith(".db") || hely.ToLower().EndsWith(".sqlite");
        private const string Jelszó = "ForgalmiUtasítás";

        public static void Adatbázis_Oldalak(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string táblanév = "Tábla_Oldalak";
            string szöveg;

            if (IsSQLite(hely))
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[OldalId] INTEGER PRIMARY KEY AUTOINCREMENT,";
                szöveg += "[FromName] TEXT(255),";
                szöveg += "[MenuName] TEXT(255),";
                szöveg += "[MenuFelirat] TEXT(255),";
                szöveg += "[Látható] INTEGER,";
                szöveg += "[Törölt] INTEGER)";
            }
            else
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[OldalId] AUTOINCREMENT PRIMARY KEY,";
                szöveg += "[FromName] CHAR(255),";
                szöveg += "[MenuName] CHAR(255),";
                szöveg += "[MenuFelirat] CHAR(255),";
                szöveg += "[Látható] yesno,";
                szöveg += "[Törölt] yesno)";
            }
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Gombok(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string táblanév = "Tábla_Gombok";
            string szöveg;

            if (IsSQLite(hely))
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[GombokId] INTEGER PRIMARY KEY AUTOINCREMENT,";
                szöveg += "[FromName] TEXT(255),";
                szöveg += "[GombName] TEXT(255),";
                szöveg += "[GombFelirat] TEXT(255),";
                szöveg += "[Szervezet] TEXT(255),";
                szöveg += "[Látható] INTEGER,";
                szöveg += "[Törölt] INTEGER)";
            }
            else
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[GombokId] AUTOINCREMENT PRIMARY KEY,";
                szöveg += "[FromName] CHAR(255),";
                szöveg += "[GombName] CHAR(255),";
                szöveg += "[GombFelirat] CHAR(255),";
                szöveg += "[Szervezet] CHAR(255),";
                szöveg += "[Látható] yesno,";
                szöveg += "[Törölt] yesno)";
            }
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Users(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string táblanév = "Tábla_Users";
            string szöveg;

            if (IsSQLite(hely))
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[UserId] INTEGER PRIMARY KEY AUTOINCREMENT,";
                szöveg += "[UserName] TEXT(25),";
                szöveg += "[WinUserName] TEXT(25),";
                szöveg += "[Dolgozószám] TEXT(8),";
                szöveg += "[Password] TEXT(255),";
                szöveg += "[Dátum] TEXT,"; // SQLite-ban a dátumot TEXT-ként vagy NUMERIC-ként kezeljük
                szöveg += "[Frissít] INTEGER,";
                szöveg += "[Törölt] INTEGER,";
                szöveg += "[Szervezetek] TEXT(255),";
                szöveg += "[Szervezet] TEXT(25),";
                szöveg += "[GlobalAdmin] INTEGER,";
                szöveg += "[TelepAdmin] INTEGER)";
            }
            else
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[UserId] AUTOINCREMENT PRIMARY KEY,";
                szöveg += "[UserName] CHAR(25),";
                szöveg += "[WinUserName] CHAR(25),";
                szöveg += "[Dolgozószám] CHAR(8),";
                szöveg += "[Password] CHAR(255),";
                szöveg += "[Dátum] Date,";
                szöveg += "[Frissít] yesno,";
                szöveg += "[Törölt] yesno,";
                szöveg += "[Szervezetek] CHAR(255),";
                szöveg += "[Szervezet] CHAR(25),";
                szöveg += "[GlobalAdmin] yesno,";
                szöveg += "[TelepAdmin] yesno)";
            }
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Verzió(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string táblanév = "Tábla_Verzió";
            string szöveg;

            if (IsSQLite(hely))
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[Id] INTEGER PRIMARY KEY AUTOINCREMENT,";
                szöveg += "[Verzió] DOUBLE)";
            }
            else
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[Id] AUTOINCREMENT PRIMARY KEY,";
                szöveg += "[Verzió] double)";
            }
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Jogosultság(string hely)
        {
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, Jelszó);
            string táblanév = "Tábla_Jogosultság";
            string szöveg;

            if (IsSQLite(hely))
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[UserId] INTEGER,";
                szöveg += "[OldalId] INTEGER,";
                szöveg += "[GombokId] INTEGER,";
                szöveg += "[SzervezetId] INTEGER,";
                szöveg += "[Törölt] INTEGER)";
            }
            else
            {
                szöveg = $"CREATE TABLE {táblanév} (";
                szöveg += "[UserId] short,";
                szöveg += "[OldalId] short,";
                szöveg += "[GombokId] short,";
                szöveg += "[SzervezetId] short,";
                szöveg += "[Törölt] yesno)";
            }
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, Jelszó, szöveg, "Alapadatok");
        }
    }
}