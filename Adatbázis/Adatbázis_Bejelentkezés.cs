

namespace VédőEszköz
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Adatbázis_Oldalak(string hely)
        {
            string szöveg;
            string jelszó = "ForgalmiUtasítás";

            //Létrehozzuk az adatbázist és beállítunk jelszót
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);


            string táblanév = "Tábla_Oldalak";
            szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[OldalId] AUTOINCREMENT PRIMARY KEY,";
            szöveg += "[FromName] CHAR(255),";
            szöveg += "[MenuName] CHAR(255),";
            szöveg += "[MenuFelirat] CHAR(255),";
            szöveg += "[Látható] yesno,";
            szöveg += "[Törölt] yesno)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Gombok(string hely)
        {
            string szöveg;
            string jelszó = "ForgalmiUtasítás";

            //Létrehozzuk az adatbázist és beállítunk jelszót
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);


            string táblanév = "Tábla_Gombok";
            szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[GombokId] AUTOINCREMENT PRIMARY KEY,";
            szöveg += "[FromName] CHAR(255),";
            szöveg += "[GombName] CHAR(255),";
            szöveg += "[GombFelirat] CHAR(255),";
            szöveg += "[Szervezet] CHAR(255),";
            szöveg += "[Látható] yesno,";
            szöveg += "[Törölt] yesno)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Users(string hely)
        {
            string jelszó = "ForgalmiUtasítás";

            //Létrehozzuk az adatbázist és beállítunk jelszót
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);


            string táblanév = "Tábla_Users";
            string szöveg = $"CREATE TABLE {táblanév} (";
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
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Verzió(string hely)
        {
            string jelszó = "ForgalmiUtasítás";

            //Létrehozzuk az adatbázist és beállítunk jelszót
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);


            string táblanév = "Tábla_Verzió";
            string szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[Id] AUTOINCREMENT PRIMARY KEY,";
            szöveg += "[Verzió] double)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }

        public static void Adatbázis_Jogosultság(string hely)
        {
            string jelszó = "ForgalmiUtasítás";

            //Létrehozzuk az adatbázist és beállítunk jelszót
            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);


            string táblanév = "Tábla_Jogosultság";
            string szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[UserId] short,";
            szöveg += "[OldalId] short,";
            szöveg += "[GombokId] short,";
            szöveg += "[SzervezetId] short,";
            szöveg += "[Törölt] yesno)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }
    }
}

