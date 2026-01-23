using System;

namespace VédőEszköz
{
    public class Adat_Behajtás_Alap
    {
        public int Id { get; set; }
        public string Adatbázisnév { get; set; }
        public string Sorszámbetűjele { get; set; }
        public int Sorszámkezdete { get; set; }
        public DateTime Engedélyérvényes { get; set; }
        public int Státus { get; set; }
        public string Adatbáziskönyvtár { get; set; }

        public Adat_Behajtás_Alap(int id, string adatbázisnév, string sorszámbetűjele, int sorszámkezdete, DateTime engedélyérvényes, int státus, string adatbáziskönyvtár)
        {
            Id = id;
            Adatbázisnév = adatbázisnév;
            Sorszámbetűjele = sorszámbetűjele;
            Sorszámkezdete = sorszámkezdete;
            Engedélyérvényes = engedélyérvényes;
            Státus = státus;
            Adatbáziskönyvtár = adatbáziskönyvtár;
        }
    }

    public class Adat_Behajtás_Dolgozótábla
    {
        public string Dolgozószám { get; set; }
        public string Dolgozónév { get; set; }
        public string Szervezetiegység { get; set; }
        public string Munkakör { get; set; }
        public bool Státus { get; set; }

        public Adat_Behajtás_Dolgozótábla(string dolgozószám, string dolgozónév, string szervezetiegység, string munkakör, bool státus)
        {
            Dolgozószám = dolgozószám;
            Dolgozónév = dolgozónév;
            Szervezetiegység = szervezetiegység;
            Munkakör = munkakör;
            Státus = státus;
        }
    }

    public class Adat_Behajtás_Engedélyezés
    {
        public int Id { get; set; }
        public string Telephely { get; set; }
        public string Emailcím { get; set; }
        public Boolean Gondnok { get; set; }
        public Boolean Szakszolgálat { get; set; }
        public string Telefonszám { get; set; }
        public string Szakszolgálatszöveg { get; set; }
        public string Beosztás { get; set; }
        public string Név { get; set; }

        public Adat_Behajtás_Engedélyezés(int id, string telephely, string emailcím, bool gondnok, bool szakszolgálat, string telefonszám, string szakszolgálatszöveg, string beosztás, string név)
        {
            Id = id;
            Telephely = telephely;
            Emailcím = emailcím;
            Gondnok = gondnok;
            Szakszolgálat = szakszolgálat;
            Telefonszám = telefonszám;
            Szakszolgálatszöveg = szakszolgálatszöveg;
            Beosztás = beosztás;
            Név = név;
        }
    }

    public class Adat_Behajtás_Jogosultság
    {
        public int ID { get; set; }
        public string Státustípus { get; set; }

        public Adat_Behajtás_Jogosultság(int iD, string státustípus)
        {
            ID = iD;
            Státustípus = státustípus;
        }
    }

    public class Adat_Behajtás_Kérelemoka
    {
        public int Id { get; set; }
        public string Ok { get; set; }

        public Adat_Behajtás_Kérelemoka(int id, string ok)
        {
            Id = id;
            Ok = ok;
        }
    }

    public class Adat_Behajtás_Kérelemsátus
    {
        public int ID { get; set; }
        public string Státus { get; set; }

        public Adat_Behajtás_Kérelemsátus(int iD, string státus)
        {
            ID = iD;
            Státus = státus;
        }
    }

    public class Adat_Behajtás_Szolgálati
    {
        public int ID { get; set; }
        public string Szolgálatihely { get; set; }

        public Adat_Behajtás_Szolgálati(int iD, string szolgálatihely)
        {
            ID = iD;
            Szolgálatihely = szolgálatihely;
        }
    }

    public class Adat_Behajtás_Telephelystátusz
    {
        public int ID { get; set; }
        public string Státus { get; set; }
        public int Gondnok { get; set; }
        public int Indoklás { get; set; }

        public Adat_Behajtás_Telephelystátusz(int iD, string státus, int gondnok, int indoklás)
        {
            ID = iD;
            Státus = státus;
            Gondnok = gondnok;
            Indoklás = indoklás;
        }
    }

    public class Adat_Behajtás_Behajtási
    {
        public string Sorszám { get; set; }
        public string Szolgálatihely { get; set; }
        public string HRazonosító { get; set; }
        public string Név { get; set; }
        public string Rendszám { get; set; }
        public int Angyalföld_engedély { get; set; }
        public string Angyalföld_megjegyzés { get; set; }
        public int Baross_engedély { get; set; }
        public string Baross_megjegyzés { get; set; }
        public int Budafok_engedély { get; set; }
        public string Budafok_megjegyzés { get; set; }
        public int Ferencváros_engedély { get; set; }
        public string Ferencváros_megjegyzés { get; set; }
        public int Fogaskerekű_engedély { get; set; }
        public string Fogaskerekű_megjegyzés { get; set; }
        public int Hungária_engedély { get; set; }
        public string Hungária_megjegyzés { get; set; }
        public int Kelenföld_engedély { get; set; }
        public string Kelenföld_megjegyzés { get; set; }
        public int Száva_engedély { get; set; }
        public string Száva_megjegyzés { get; set; }
        public int Szépilona_engedély { get; set; }
        public string Szépilona_megjegyzés { get; set; }
        public int Zugló_engedély { get; set; }
        public string Zugló_megjegyzés { get; set; }
        public string Korlátlan { get; set; }
        public int Autók_száma { get; set; }
        public int I_engedély { get; set; }
        public int II_engedély { get; set; }
        public int III_engedély { get; set; }
        public int Státus { get; set; }
        public DateTime Dátum { get; set; }
        public string Megjegyzés { get; set; }
        public string PDF { get; set; }
        public string OKA { get; set; }
        public DateTime Érvényes { get; set; }

        public Adat_Behajtás_Behajtási(string sorszám, string szolgálatihely, string hRazonosító, string név, string rendszám, int angyalföld_engedély, string angyalföld_megjegyzés, int baross_engedély, string baross_megjegyzés, int budafok_engedély, string budafok_megjegyzés, int ferencváros_engedély, string ferencváros_megjegyzés, int fogaskerekű_engedély, string fogaskerekű_megjegyzés, int hungária_engedély, string hungária_megjegyzés, int kelenföld_engedély, string kelenföld_megjegyzés, int száva_engedély, string száva_megjegyzés, int szépilona_engedély, string szépilona_megjegyzés, int zugló_engedély, string zugló_megjegyzés, string korlátlan, int autók_száma, int i_engedély, int iI_engedély, int iII_engedély, int státus, DateTime dátum, string megjegyzés, string pDF, string oKA, DateTime érvényes)
        {
            Sorszám = sorszám;
            Szolgálatihely = szolgálatihely;
            HRazonosító = hRazonosító;
            Név = név;
            Rendszám = rendszám;
            Angyalföld_engedély = angyalföld_engedély;
            Angyalföld_megjegyzés = angyalföld_megjegyzés;
            Baross_engedély = baross_engedély;
            Baross_megjegyzés = baross_megjegyzés;
            Budafok_engedély = budafok_engedély;
            Budafok_megjegyzés = budafok_megjegyzés;
            Ferencváros_engedély = ferencváros_engedély;
            Ferencváros_megjegyzés = ferencváros_megjegyzés;
            Fogaskerekű_engedély = fogaskerekű_engedély;
            Fogaskerekű_megjegyzés = fogaskerekű_megjegyzés;
            Hungária_engedély = hungária_engedély;
            Hungária_megjegyzés = hungária_megjegyzés;
            Kelenföld_engedély = kelenföld_engedély;
            Kelenföld_megjegyzés = kelenföld_megjegyzés;
            Száva_engedély = száva_engedély;
            Száva_megjegyzés = száva_megjegyzés;
            Szépilona_engedély = szépilona_engedély;
            Szépilona_megjegyzés = szépilona_megjegyzés;
            Zugló_engedély = zugló_engedély;
            Zugló_megjegyzés = zugló_megjegyzés;
            Korlátlan = korlátlan;
            Autók_száma = autók_száma;
            I_engedély = i_engedély;
            II_engedély = iI_engedély;
            III_engedély = iII_engedély;
            Státus = státus;
            Dátum = dátum;
            Megjegyzés = megjegyzés;
            PDF = pDF;
            OKA = oKA;
            Érvényes = érvényes;
        }

        public Adat_Behajtás_Behajtási(string sorszám, string szolgálatihely, string hRazonosító, string név, string rendszám, int angyalföld_engedély, string angyalföld_megjegyzés, int baross_engedély, string baross_megjegyzés, int budafok_engedély, string budafok_megjegyzés, int ferencváros_engedély, string ferencváros_megjegyzés, int fogaskerekű_engedély, string fogaskerekű_megjegyzés, int hungária_engedély, string hungária_megjegyzés, int kelenföld_engedély, string kelenföld_megjegyzés, int száva_engedély, string száva_megjegyzés, int szépilona_engedély, string szépilona_megjegyzés, int zugló_engedély, string zugló_megjegyzés, string korlátlan, int autók_száma, int státus, DateTime dátum, string megjegyzés, string pDF, string oKA, DateTime érvényes)
        {
            Sorszám = sorszám;
            Szolgálatihely = szolgálatihely;
            HRazonosító = hRazonosító;
            Név = név;
            Rendszám = rendszám;
            Angyalföld_engedély = angyalföld_engedély;
            Angyalföld_megjegyzés = angyalföld_megjegyzés;
            Baross_engedély = baross_engedély;
            Baross_megjegyzés = baross_megjegyzés;
            Budafok_engedély = budafok_engedély;
            Budafok_megjegyzés = budafok_megjegyzés;
            Ferencváros_engedély = ferencváros_engedély;
            Ferencváros_megjegyzés = ferencváros_megjegyzés;
            Fogaskerekű_engedély = fogaskerekű_engedély;
            Fogaskerekű_megjegyzés = fogaskerekű_megjegyzés;
            Hungária_engedély = hungária_engedély;
            Hungária_megjegyzés = hungária_megjegyzés;
            Kelenföld_engedély = kelenföld_engedély;
            Kelenföld_megjegyzés = kelenföld_megjegyzés;
            Száva_engedély = száva_engedély;
            Száva_megjegyzés = száva_megjegyzés;
            Szépilona_engedély = szépilona_engedély;
            Szépilona_megjegyzés = szépilona_megjegyzés;
            Zugló_engedély = zugló_engedély;
            Zugló_megjegyzés = zugló_megjegyzés;
            Korlátlan = korlátlan;
            Autók_száma = autók_száma;
            Státus = státus;
            Dátum = dátum;
            Megjegyzés = megjegyzés;
            PDF = pDF;
            OKA = oKA;
            Érvényes = érvényes;
        }

        public Adat_Behajtás_Behajtási(string sorszám, int státus)
        {
            Sorszám = sorszám;
            Státus = státus;
        }
    }

    public class Adat_Behajtás_Behajtási_Napló
    {
        public string Sorszám { get; set; }
        public string Szolgálatihely { get; set; }
        public string HRazonosító { get; set; }
        public string Név { get; set; }
        public string Rendszám { get; set; }
        public int Angyalföld_engedély { get; set; }
        public string Angyalföld_megjegyzés { get; set; }
        public int Baross_engedély { get; set; }
        public string Baross_megjegyzés { get; set; }
        public int Budafok_engedély { get; set; }
        public string Budafok_megjegyzés { get; set; }
        public int Ferencváros_engedély { get; set; }
        public string Ferencváros_megjegyzés { get; set; }
        public int Fogaskerekű_engedély { get; set; }
        public string Fogaskerekű_megjegyzés { get; set; }
        public int Hungária_engedély { get; set; }
        public string Hungária_megjegyzés { get; set; }
        public int Kelenföld_engedély { get; set; }
        public string Kelenföld_megjegyzés { get; set; }
        public int Száva_engedély { get; set; }
        public string Száva_megjegyzés { get; set; }
        public int Szépilona_engedély { get; set; }
        public string Szépilona_megjegyzés { get; set; }
        public int Zugló_engedély { get; set; }
        public string Zugló_megjegyzés { get; set; }
        public string Korlátlan { get; set; }
        public int Autók_száma { get; set; }
        public int I_engedély { get; set; }
        public int II_engedély { get; set; }
        public int III_engedély { get; set; }
        public int Státus { get; set; }
        public DateTime Dátum { get; set; }
        public string Megjegyzés { get; set; }
        public string PDF { get; set; }
        public string OKA { get; set; }
        public int ID { get; set; }
        public string Rögzítette { get; set; }
        public DateTime Rögzítésdátuma { get; set; }
        public DateTime Érvényes { get; set; }

        public Adat_Behajtás_Behajtási_Napló(string sorszám, string szolgálatihely, string hRazonosító, string név, string rendszám, int angyalföld_engedély, string angyalföld_megjegyzés, int baross_engedély, string baross_megjegyzés, int budafok_engedély, string budafok_megjegyzés, int ferencváros_engedély, string ferencváros_megjegyzés, int fogaskerekű_engedély, string fogaskerekű_megjegyzés, int hungária_engedély, string hungária_megjegyzés, int kelenföld_engedély, string kelenföld_megjegyzés, int száva_engedély, string száva_megjegyzés, int szépilona_engedély, string szépilona_megjegyzés, int zugló_engedély, string zugló_megjegyzés, string korlátlan, int autók_száma, int i_engedély, int iI_engedély, int iII_engedély, int státus, DateTime dátum, string megjegyzés, string pDF, string oKA, int iD, string rögzítette, DateTime rögzítésdátuma, DateTime érvényes)
        {
            Sorszám = sorszám;
            Szolgálatihely = szolgálatihely;
            HRazonosító = hRazonosító;
            Név = név;
            Rendszám = rendszám;
            Angyalföld_engedély = angyalföld_engedély;
            Angyalföld_megjegyzés = angyalföld_megjegyzés;
            Baross_engedély = baross_engedély;
            Baross_megjegyzés = baross_megjegyzés;
            Budafok_engedély = budafok_engedély;
            Budafok_megjegyzés = budafok_megjegyzés;
            Ferencváros_engedély = ferencváros_engedély;
            Ferencváros_megjegyzés = ferencváros_megjegyzés;
            Fogaskerekű_engedély = fogaskerekű_engedély;
            Fogaskerekű_megjegyzés = fogaskerekű_megjegyzés;
            Hungária_engedély = hungária_engedély;
            Hungária_megjegyzés = hungária_megjegyzés;
            Kelenföld_engedély = kelenföld_engedély;
            Kelenföld_megjegyzés = kelenföld_megjegyzés;
            Száva_engedély = száva_engedély;
            Száva_megjegyzés = száva_megjegyzés;
            Szépilona_engedély = szépilona_engedély;
            Szépilona_megjegyzés = szépilona_megjegyzés;
            Zugló_engedély = zugló_engedély;
            Zugló_megjegyzés = zugló_megjegyzés;
            Korlátlan = korlátlan;
            Autók_száma = autók_száma;
            I_engedély = i_engedély;
            II_engedély = iI_engedély;
            III_engedély = iII_engedély;
            Státus = státus;
            Dátum = dátum;
            Megjegyzés = megjegyzés;
            PDF = pDF;
            OKA = oKA;
            ID = iD;
            Rögzítette = rögzítette;
            Rögzítésdátuma = rögzítésdátuma;
            Érvényes = érvényes;
        }

        public Adat_Behajtás_Behajtási_Napló(string sorszám, int státus, int iD, string rögzítette, DateTime rögzítésdátuma)
        {
            Sorszám = sorszám;
            Státus = státus;
            ID = iD;
            Rögzítette = rögzítette;
            Rögzítésdátuma = rögzítésdátuma;
        }
    }

    public class Adat_Behajtási_Engedélyek
    {
        public string Telephely { get; set; }
        public int Engedély { get; set; }
        public string Megjegyzés { get; set; }

        public Adat_Behajtási_Engedélyek(string telephely, int engedély, string megjegyzés)
        {
            Telephely = telephely;
            Engedély = engedély;
            Megjegyzés = megjegyzés;
        }
    }
}
