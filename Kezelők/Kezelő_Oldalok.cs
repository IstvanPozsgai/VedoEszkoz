using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VédőEszköz;
using MyA = Adatbázis;

namespace VédőEszköz
{
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

        public void Döntés(Adat_Oldalak Adat)
        {
            try
            {
                List<Adat_Oldalak> Adatok = Lista_Adatok();
                if (!Adatok.Any(a => a.OldalId == Adat.OldalId))
                    Rögzítés(Adat);
                else
                    Módosítás(Adat);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kezelő_Oldalok.Döntés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show("Hiba az adatok mentésekor. A hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_Oldalak Adat)
        {
            string L = Adat.Látható ? "1" : "0";
            string T = Adat.Törölt ? "1" : "0";

            string szöveg = $"INSERT INTO {táblanév} (FromName, MenuName, MenuFelirat, Látható, Törölt) VALUES (";
            szöveg += $"'{Adat.FromName}', '{Adat.MenuName}', '{Adat.MenuFelirat}', {L}, {T})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(Adat_Oldalak Adat)
        {
            string L = Adat.Látható ? "1" : "0";
            string T = Adat.Törölt ? "1" : "0";

            string szöveg = $"UPDATE {táblanév} SET ";
            szöveg += $"FromName ='{Adat.FromName}', MenuName ='{Adat.MenuName}', MenuFelirat ='{Adat.MenuFelirat}', ";
            szöveg += $"Látható ={L}, Törölt ={T} WHERE OldalId = {Adat.OldalId}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}