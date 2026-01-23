

namespace VédőEszköz
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Behajtási_Alap(string hely)
        {
            string szöveg;
            string jelszó = "egérpad";

            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Alapadatok (";
            szöveg += "[Id] long,";
            szöveg += "[Adatbázisnév]  char (250),";
            szöveg += "[Sorszámbetűjele]  char (250),";
            szöveg += "[Sorszámkezdete] long,";
            szöveg += "[Engedélyérvényes] DATE,";
            szöveg += "[Státus] long,";
            szöveg += "[Adatbáziskönyvtár]  char (250))";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");


            szöveg = "CREATE TABLE Dolgozóktábla (";
            szöveg += "[Dolgozószám]  char (250),";
            szöveg += "[Dolgozónév]  char (250),";
            szöveg += "[Szervezetiegység]  char (250),";
            szöveg += "[Munkakör]  char (250),";
            szöveg += "[Státus yesno)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");


            szöveg = "CREATE TABLE Engedélyezés (";
            szöveg += "[id]  long,";
            szöveg += "[Telephely]  char (250),";
            szöveg += "[emailcím]  char (250),";
            szöveg += "[Gondnok]  YESNO,";
            szöveg += "[Szakszolgálat]  YESNO,";
            szöveg += "[Telefonszám]  char (30),";
            szöveg += "[Szakszolgálatszöveg]  char (30),";
            szöveg += "[Beosztás]  char (50),";
            szöveg += "[Név]  char (200))";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");


            szöveg = "CREATE TABLE Jogosultságtípus (";
            szöveg += "[ID]  DOUBLE,";
            szöveg += "[Státustípus]  char (250))";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");


            szöveg = "CREATE TABLE Kérelemoka (";
            szöveg += "[Id]  long,";
            szöveg += "[Ok]  char (250))";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");


            szöveg = "CREATE TABLE KérelemStátus (";
            szöveg += "[ID]  DOUBLE,";
            szöveg += "[Státus]  char (250))";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");


            szöveg = "CREATE TABLE Szolgálatihely (";
            szöveg += "[ID]  DOUBLE,";
            szöveg += "[Szolgálatihely]  char (250))";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");


            szöveg = "CREATE TABLE Telephelystátus (";
            szöveg += "[ID]  DOUBLE,";
            szöveg += "[Státus]  char (250),";
            szöveg += "[Gondnok]  DOUBLE,";
            szöveg += "[Indoklás]  DOUBLE)";
            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }

        public static void Behajtási_Adatok(string hely)
        {
            string szöveg;

            string jelszó = "forgalmirendszám";

            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Alapadatok(";
            szöveg += "Sorszám char(250),";
            szöveg += "Szolgálatihely LONGTEXT,";
            szöveg += "Hrazonosító LONGTEXT,";
            szöveg += "Név LONGTEXT,";
            szöveg += "Rendszám LONGTEXT,";
            szöveg += "Angyalföld_engedély short,";
            szöveg += "Angyalföld_megjegyzés LONGTEXT,";
            szöveg += "Baross_engedély short,";
            szöveg += "Baross_megjegyzés LONGTEXT,";
            szöveg += "Budafok_engedély short,";
            szöveg += "Budafok_megjegyzés LONGTEXT,";
            szöveg += "Ferencváros_engedély short,";
            szöveg += "Ferencváros_megjegyzés LONGTEXT,";
            szöveg += "Fogaskerekű_engedély short,";
            szöveg += "Fogaskerekű_megjegyzés LONGTEXT,";
            szöveg += "Hungária_engedély short,";
            szöveg += "Hungária_megjegyzés LONGTEXT,";
            szöveg += "Kelenföld_engedély short,";
            szöveg += "Kelenföld_megjegyzés LONGTEXT,";
            szöveg += "Száva_engedély short,";
            szöveg += "Száva_megjegyzés LONGTEXT,";
            szöveg += "Szépilona_engedély short,";
            szöveg += "Szépilona_megjegyzés LONGTEXT,";
            szöveg += "Zugló_engedély short,";
            szöveg += "Zugló_megjegyzés LONGTEXT,";
            szöveg += "Korlátlan LONGTEXT,";
            szöveg += "Autók_száma short,";
            szöveg += "I_engedély long,";
            szöveg += "II_engedély long,";
            szöveg += "III_engedély long,";
            szöveg += "Státus short,";
            szöveg += "Dátum DATE,";
            szöveg += "Megjegyzés LONGTEXT,";
            szöveg += "PDF LONGTEXT,";
            szöveg += "OKA LONGTEXT,";
            szöveg += "érvényes DATE)";

            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }

        public static void Behajtási_Adatok_Napló(string hely)
        {
            string szöveg;
            string jelszó = "forgalmirendszám";

            AdatBázis_kezelés.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Alapadatok (";
            szöveg += "[Sorszám] LONGTEXT,";
            szöveg += "[Szolgálatihely] LONGTEXT,";
            szöveg += "[Hrazonosító] LONGTEXT,";
            szöveg += "[Név] char (250),";
            szöveg += "[Rendszám] LONGTEXT,";
            szöveg += "[Angyalföld_engedély] short,";
            szöveg += "[Angyalföld_megjegyzés] LONGTEXT,";
            szöveg += "[Baross_engedély] short,";
            szöveg += "[Baross_megjegyzés] LONGTEXT,";
            szöveg += "[Budafok_engedély] short,";
            szöveg += "[Budafok_megjegyzés] LONGTEXT,";
            szöveg += "[Ferencváros_engedély] short,";
            szöveg += "[Ferencváros_megjegyzés] LONGTEXT,";
            szöveg += "[Fogaskerekű_engedély] short,";
            szöveg += "[Fogaskerekű_megjegyzés] LONGTEXT,";
            szöveg += "[Hungária_engedély] short,";
            szöveg += "[Hungária_megjegyzés] LONGTEXT,";
            szöveg += "[Kelenföld_engedély] short,";
            szöveg += "[Kelenföld_megjegyzés] LONGTEXT,";
            szöveg += "[Száva_engedély] short,";
            szöveg += "[Száva_megjegyzés] LONGTEXT,";
            szöveg += "[Szépilona_engedély] short,";
            szöveg += "[Szépilona_megjegyzés] LONGTEXT,";
            szöveg += "[Zugló_engedély] short,";
            szöveg += "[Zugló_megjegyzés] LONGTEXT,";
            szöveg += "[Korlátlan] LONGTEXT,";
            szöveg += "[Autók_száma] short,";
            szöveg += "[I_engedély] long,";
            szöveg += "[II_engedély] long,";
            szöveg += "[III_engedély] long,";
            szöveg += "[Státus] short,";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Megjegyzés] LONGTEXT,";
            szöveg += "[PDF] LONGTEXT,";
            szöveg += "[OKA] LONGTEXT,";
            szöveg += "[ID] long,";
            szöveg += "[rögzítette] LONGTEXT,";
            szöveg += "[rögzítésdátuma] DATE,";
            szöveg += "[érvényes] DATE)";

            AdatBázis_kezelés.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, "Alapadatok");
        }
    }
}
