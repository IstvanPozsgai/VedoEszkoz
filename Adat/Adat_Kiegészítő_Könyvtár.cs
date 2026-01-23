using System;
using System.Collections.Generic;

namespace VédőEszköz
{
    public class Adat_Kiegészítő_Szolgálattelepei
    {
        public int Sorszám { get; private set; }
        public string Telephelynév { get; private set; }
        public string Szolgálatnév { get; private set; }
        public string Felelősmunkahely { get; private set; }
        public string Raktár { get; private set; }
        public Adat_Kiegészítő_Szolgálattelepei(int sorszám, string telephelynév, string szolgálatnév, string felelősmunkahely)
        {
            Sorszám = sorszám;
            Telephelynév = telephelynév;
            Szolgálatnév = szolgálatnév;
            Felelősmunkahely = felelősmunkahely;
        }

        public Adat_Kiegészítő_Szolgálattelepei(int sorszám, string telephelynév, string szolgálatnév, string felelősmunkahely, string raktár)
        {
            Sorszám = sorszám;
            Telephelynév = telephelynév;
            Szolgálatnév = szolgálatnév;
            Felelősmunkahely = felelősmunkahely;
            Raktár = raktár;
        }
    }

    public class Adat_Kiegészítő_Szolgálat
    {
        public int Sorszám { get; private set; }
        public string Szolgálatnév { get; private set; }

        public Adat_Kiegészítő_Szolgálat(int sorszám, string szolgálatnév)
        {
            Sorszám = sorszám;
            Szolgálatnév = szolgálatnév;
        }
    }

    public class Adat_Kiegészítő_Könyvtár
    {
        public int ID { get; private set; }
        public string Név { get; private set; }
        public bool Vezér1 { get; private set; }
        public int Csoport1 { get; private set; }
        public int Csoport2 { get; private set; }
        public bool Vezér2 { get; private set; }
        public int Sorrend1 { get; private set; }
        public int Sorrend2 { get; private set; }

        public Adat_Kiegészítő_Könyvtár(int iD, string név, bool vezér1, int csoport1, int csoport2, bool vezér2, int sorrend1, int sorrend2)
        {
            ID = iD;
            Név = név;
            Vezér1 = vezér1;
            Csoport1 = csoport1;
            Csoport2 = csoport2;
            Vezér2 = vezér2;
            Sorrend1 = sorrend1;
            Sorrend2 = sorrend2;
        }
    }

    public class Adat_Kiegészítő_Sérülés
    {
        public int ID { get; private set; }
        public string Név { get; private set; }
        public bool Vezér1 { get; private set; }
        public int Csoport1 { get; private set; }
        public int Csoport2 { get; private set; }
        public bool Vezér2 { get; private set; }
        public int Sorrend1 { get; private set; }
        public int Sorrend2 { get; private set; }
        public string Költséghely { get; private set; }

        public Adat_Kiegészítő_Sérülés(int iD, string név, bool vezér1, int csoport1, int csoport2, bool vezér2, int sorrend1, int sorrend2, string költséghely)
        {
            ID = iD;
            Név = név;
            Vezér1 = vezér1;
            Csoport1 = csoport1;
            Csoport2 = csoport2;
            Vezér2 = vezér2;
            Sorrend1 = sorrend1;
            Sorrend2 = sorrend2;
            Költséghely = költséghely;
        }
    }
    public class Adat_Kiegészítő_SérülésSzöveg
    {
        public int Id { get; set; }
        public string Szöveg1 { get; set; }
        public string Szöveg2 { get; set; }
        public string Szöveg3 { get; set; }
        public string Szöveg4 { get; set; }
        public string Szöveg5 { get; set; }
        public string Szöveg6 { get; set; }
        public string Szöveg7 { get; set; }
        public string Szöveg8 { get; set; }
        public string Szöveg9 { get; set; }
        public string Szöveg10 { get; set; }
        public string Szöveg11 { get; set; }

        public Adat_Kiegészítő_SérülésSzöveg(int id, string szöveg1, string szöveg2, string szöveg3, string szöveg4, string szöveg5, string szöveg6, string szöveg7, string szöveg8, string szöveg9, string szöveg10, string szöveg11)
        {
            Id = id;
            Szöveg1 = szöveg1;
            Szöveg2 = szöveg2;
            Szöveg3 = szöveg3;
            Szöveg4 = szöveg4;
            Szöveg5 = szöveg5;
            Szöveg6 = szöveg6;
            Szöveg7 = szöveg7;
            Szöveg8 = szöveg8;
            Szöveg9 = szöveg9;
            Szöveg10 = szöveg10;
            Szöveg11 = szöveg11;
        }
    }

    public class Adat_Kiegészítő_Beosegéd
    {
        public string Beosztáskód { get; private set; }
        public int Túlóra { get; private set; }
        public DateTime Kezdőidő { get; private set; }
        public DateTime Végeidő { get; private set; }
        public string Túlóraoka { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Kiegészítő_Beosegéd(string beosztáskód, int túlóra, DateTime kezdőidő, DateTime végeidő, string túlóraoka, string telephely)
        {
            Beosztáskód = beosztáskód;
            Túlóra = túlóra;
            Kezdőidő = kezdőidő;
            Végeidő = végeidő;
            Túlóraoka = túlóraoka;
            Telephely = telephely;
        }
    }



    public class Adat_Kiegészítő_Túlórakeret
    {
        public int Határ { get; private set; }
        public int Parancs { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Kiegészítő_Túlórakeret(int határ, int parancs, string telephely)
        {
            Határ = határ;
            Parancs = parancs;
            Telephely = telephely;
        }

        internal List<Adat_Kiegészítő_Túlórakeret> ToList()
        {
            throw new NotImplementedException();
        }
    }

    public class Adat_Kiegészítő_Turnusok
    {
        public string Csoport { get; private set; }

        public Adat_Kiegészítő_Turnusok(string csoport)
        {
            Csoport = csoport;
        }
    }


    public class Adat_Kiegészítő_Váltóstábla
    {
        public int Id { get; private set; }
        public DateTime Kezdődátum { get; private set; }
        public int Ciklusnap { get; private set; }
        public string Megnevezés { get; private set; }
        public string Csoport { get; private set; }

        public Adat_Kiegészítő_Váltóstábla(int id, DateTime kezdődátum, int ciklusnap, string megnevezés, string csoport)
        {
            Id = id;
            Kezdődátum = kezdődátum;
            Ciklusnap = ciklusnap;
            Megnevezés = megnevezés;
            Csoport = csoport;
        }
    }

    public class Adat_Kiegészítő_Beosztásciklus
    {
        public int Id { get; private set; }
        public string Beosztáskód { get; private set; }
        public string Hétnapja { get; private set; }
        public string Beosztásszöveg { get; private set; }

        public Adat_Kiegészítő_Beosztásciklus(int id, string beosztáskód, string hétnapja, string beosztásszöveg)
        {
            Id = id;
            Beosztáskód = beosztáskód;
            Hétnapja = hétnapja;
            Beosztásszöveg = beosztásszöveg;
        }
    }

    public class Adat_Kiegészítő_Doksi
    {
        public string Kategória { get; private set; }
        public string Kód { get; private set; }
        public string Éves { get; private set; }

        public Adat_Kiegészítő_Doksi(string kategória, string kód, string éves)
        {
            Kategória = kategória;
            Kód = kód;
            Éves = éves;
        }
    }

    public class Adat_Kiegészítő_éjszakásciklus { }

    public class Adat_Kiegészítő_Feorszámok
    {
        public long Sorszám { get; private set; }
        public string Feorszám { get; private set; }

        public string Feormegnevezés { get; private set; }
        public long Státus { get; private set; }

        public Adat_Kiegészítő_Feorszámok(long sorszám, string feorszám, string feormegnevezés, long státus)
        {
            Sorszám = sorszám;
            Feorszám = feorszám;
            Feormegnevezés = feormegnevezés;
            Státus = státus;
        }
    }

    public class Adat_Kiegészítő_Jogtípus
    {
        public long Sorszám { get; private set; }
        public string Típus { get; private set; }

        public Adat_Kiegészítő_Jogtípus(long sorszám, string típus)
        {
            Sorszám = sorszám;
            Típus = típus;
        }
    }

    public class Adat_Kiegészítő_Jogvonal
    {
        public long Sorszám { get; private set; }
        public string Szám { get; private set; }
        public string Megnevezés { get; private set; }

        public Adat_Kiegészítő_Jogvonal(long sorszám, string szám, string megnevezés)
        {
            Sorszám = sorszám;
            Szám = szám;
            Megnevezés = megnevezés;
        }
    }

    public class Adat_Kiegészítő_Kiegmunkakör
    {
        public long Id { get; private set; }
        public string Megnevezés { get; private set; }
        public long Státus { get; private set; }

        public Adat_Kiegészítő_Kiegmunkakör(long id, string megnevezés, long státus)
        {
            Id = id;
            Megnevezés = megnevezés;
            Státus = státus;
        }
    }

    public class Adat_Kiegészítő_Munkaidő
    {
        public string Munkarendelnevezés { get; private set; }

        public double Munkaidő { get; private set; }

        public Adat_Kiegészítő_Munkaidő(string munkarendelnevezés, double munkaidő)
        {
            Munkarendelnevezés = munkarendelnevezés;
            Munkaidő = munkaidő;
        }
    }

    public class Adat_Kiegészítő_Részmunkakör
    {
        public long Id { get; private set; }
        public string Megnevezés { get; private set; }
        public long Státus { get; private set; }

        public Adat_Kiegészítő_Részmunkakör(long id, string megnevezés, long státus)
        {
            Id = id;
            Megnevezés = megnevezés;
            Státus = státus;
        }
    }

    public class Adat_Kiegészítő_Védelem
    {
        public long Sorszám { get; private set; }
        public string Megnevezés { get; private set; }

        public Adat_Kiegészítő_Védelem(long sorszám, string megnevezés)
        {
            Sorszám = sorszám;
            Megnevezés = megnevezés;
        }
    }

    public class Adat_Kiegészítő_Főkategóriatábla
    {
        public long Sorszám { get; private set; }
        public string Főkategória { get; private set; }

        public Adat_Kiegészítő_Főkategóriatábla(long sorszám, string főkategória)
        {
            Sorszám = sorszám;
            Főkategória = főkategória;
        }
    }

    public class Adat_Kiegészítő_Típusrendezéstábla
    {
        public long Sorszám { get; private set; }
        public string Főkategória { get; private set; }
        public string Típus { get; private set; }
        public string AlTípus { get; private set; }
        public string Telephely { get; private set; }
        public string Telephelyitípus { get; private set; }

        public Adat_Kiegészítő_Típusrendezéstábla(long sorszám, string főkategória, string típus, string alTípus, string telephely, string telephelyitípus)
        {
            Sorszám = sorszám;
            Főkategória = főkategória;
            Típus = típus;
            AlTípus = alTípus;
            Telephely = telephely;
            Telephelyitípus = telephelyitípus;
        }
    }

    public class Adat_Kiegészítő_Típusaltípustábla
    {
        public long Sorszám { get; private set; }
        public string Főkategória { get; private set; }
        public string Típus { get; private set; }
        public string AlTípus { get; private set; }

        public Adat_Kiegészítő_Típusaltípustábla(long sorszám, string főkategória, string típus, string alTípus)
        {
            Sorszám = sorszám;
            Főkategória = főkategória;
            Típus = típus;
            AlTípus = alTípus;
        }
    }

    public class Adat_Kiegészítő_Fortetípus
    {
        public long Sorszám { get; private set; }
        public string Ftípus { get; private set; }
        public string Telephely { get; private set; }
        public string Telephelyitípus { get; private set; }

        public Adat_Kiegészítő_Fortetípus(long sorszám, string ftípus, string telephely, string telephelyitípus)
        {
            Sorszám = sorszám;
            Ftípus = ftípus;
            Telephely = telephely;
            Telephelyitípus = telephelyitípus;
        }
    }

    public class Adat_Kiegészítő_Mentésihelyek
    {
        public long Sorszám { get; private set; }
        public string Alprogram { get; private set; }
        public string Elérésiút { get; private set; }

        public Adat_Kiegészítő_Mentésihelyek(long sorszám, string alprogram, string elérésiút)
        {
            Sorszám = sorszám;
            Alprogram = alprogram;
            Elérésiút = elérésiút;
        }
    }

    public class Adat_Kiegészítő_Típuszínektábla
    {
        public string Típus { get; private set; }
        public long Színszám { get; private set; }

        public Adat_Kiegészítő_Típuszínektábla(string típus, long színszám)
        {
            Típus = típus;
            Színszám = színszám;
        }
    }

    public class Adat_Kiegészítő_Idő_Kor
    {
        public long Id { get; private set; }
        public long Kiadási { get; private set; }
        public long Érkezési { get; private set; }

        public Adat_Kiegészítő_Idő_Kor(long id, long kiadási, long érkezési)
        {
            Id = id;
            Kiadási = kiadási;
            Érkezési = érkezési;
        }
    }

    public class Adat_Kiegészítő_Adatok_Terjesztés
    {
        public long Id { get; private set; }
        public string Szöveg { get; private set; }
        public string Email { get; private set; }

        public Adat_Kiegészítő_Adatok_Terjesztés(long id, string szöveg, string email)
        {
            Id = id;
            Szöveg = szöveg;
            Email = email;
        }
    }

    public class Adat_Kiegészítő_Idő_Tábla
    {
        public long Sorszám { get; private set; }
        public DateTime Reggel { get; private set; }
        public DateTime Este { get; private set; }
        public DateTime Délután { get; private set; }

        public Adat_Kiegészítő_Idő_Tábla(long sorszám, DateTime reggel, DateTime este, DateTime délután)
        {
            Sorszám = sorszám;
            Reggel = reggel;
            Este = este;
            Délután = délután;
        }


    }

    public class Adat_Kiegészítő_Reklám
    {
        public string Méret { get; private set; }

        public Adat_Kiegészítő_Reklám(string méret)
        {
            Méret = méret;
        }
    }

    public class Adat_Kiegészítő_Forte_Vonal
    {
        public string ForteVonal { get; private set; }

        public Adat_Kiegészítő_Forte_Vonal(string forteVonal)
        {
            ForteVonal = forteVonal;
        }
    }

    public class Adat_Kiegészítő_Munkakör
    {
        public long Id { get; private set; }
        public string Megnevezés { get; private set; }
        public string Kategória { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Kiegészítő_Munkakör(long id, string megnevezés, string kategória, bool státus)
        {
            Id = id;
            Megnevezés = megnevezés;
            Kategória = kategória;
            Státus = státus;
        }
    }
}

