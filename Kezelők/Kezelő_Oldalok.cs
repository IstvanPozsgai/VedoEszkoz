using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using VédőEszköz;

public class Kezelő_Oldalok
{
    readonly string hely;
    readonly string jelszó = "ForgalmiUtasítás";
    readonly string táblanév = "Tábla_Oldalak";

    public Kezelő_Oldalok()
    {
        hely = Path.Combine(Application.StartupPath, "VédőAdatok", "Új_Belépés.db");

        if (!File.Exists(hely))
            Adatbázis_Létrehozás.Adatbázis_Oldalak(hely);

        if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév))
            Adatbázis_Létrehozás.Adatbázis_Oldalak(hely);
    }

    public List<Adat_Oldalak> Lista_Adatok()
    {
        List<Adat_Oldalak> Adatok = new List<Adat_Oldalak>();
        string szöveg = $"SELECT * FROM {táblanév}";

        using (SQLiteConnection Kapcsolat = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
        {
            Kapcsolat.Open();
            using (SQLiteCommand Parancs = new SQLiteCommand(szöveg, Kapcsolat))
            using (var rekord = Parancs.ExecuteReader())
            {
                while (rekord.Read())
                {
                    Adatok.Add(new Adat_Oldalak(
                        (rekord["OldalId"].ToÉrt_Int()),
                        rekord["FromName"].ToString().Trim(),
                        rekord["MenuName"].ToString().Trim(),
                        rekord["MenuFelirat"].ToString().Trim(),
                        (rekord["Látható"].ToÉrt_Int()) == 1,
                        (rekord["Törölt"].ToÉrt_Int()) == 1));
                }
            }
        }
        return Adatok;
    }
}