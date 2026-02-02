using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MyA = Adatbázis;

namespace VédőEszköz
{
    public class Kezelő_Gombok
    {
        readonly string hely;
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Gombok";

        public Kezelő_Gombok()
        {
            hely = Path.Combine(Application.StartupPath, "VédőAdatok", "ÚJ_Belépés.db");

            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Adatbázis_Gombok(hely);

            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév))
                Adatbázis_Létrehozás.Adatbázis_Gombok(hely);
        }

        public List<Adat_Gombok> Lista_Adatok()
        {
            List<Adat_Gombok> Adatok = new List<Adat_Gombok>();
            string szöveg = $"SELECT * FROM {táblanév}";

            using (SQLiteConnection Kapcsolat = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                Kapcsolat.Open();
                using (SQLiteCommand Parancs = new SQLiteCommand(szöveg, Kapcsolat))
                using (SQLiteDataReader rekord = Parancs.ExecuteReader())
                {
                    while (rekord.Read())
                    {
                        Adat_Gombok Adat = new Adat_Gombok(
                            (rekord["GombokId"].ToÉrt_Int()),
                            rekord["FromName"].ToString().Trim(),
                            rekord["GombName"].ToString().Trim(),
                            rekord["GombFelirat"].ToString().Trim(),
                            rekord["Szervezet"].ToString().Trim(),
                            (rekord["Látható"].ToÉrt_Int()) == 1,
                            (rekord["Törölt"].ToÉrt_Int()) == 1);
                        Adatok.Add(Adat);
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Gombok Adat)
        {
            try
            {
                List<Adat_Gombok> Adatok = Lista_Adatok();

                Adat_Gombok gomb = Adatok.FirstOrDefault(a =>
                    a.GombName == Adat.GombName &&
                    a.FromName == Adat.FromName &&
                    !a.Törölt);

                if (gomb == null && Adat.GombokId == 0)
                    Rögzítés(Adat);

                else
                {
                    Adat_Gombok gomb1 = Adatok.FirstOrDefault(a =>
                        a.GombName == Adat.GombName &&
                        a.FromName == Adat.FromName &&
                        a.GombokId == Adat.GombokId);

                    if (gomb1 != null)
                        Módosítás(Adat);
                    else
                        throw new HibásBevittAdat($"Ez az azonosító ({Adat.GombokId}) már szerepel a rendszerben!");
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_Gombok Adat)
        {
            try
            {
                string L = Adat.Látható ? "1" : "0";
                string T = Adat.Törölt ? "1" : "0";

                string szöveg = $"INSERT INTO {táblanév} (FromName, GombName, GombFelirat, Szervezet, Látható, Törölt) VALUES (";
                szöveg += $"'{Adat.FromName}', '{Adat.GombName}', '{Adat.GombFelirat}', '{Adat.Szervezet}', {L}, {T})";

                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                throw;
            }
        }

        public void Módosítás(Adat_Gombok Adat)
        {
            try
            {
                string L = Adat.Látható ? "1" : "0";
                string T = Adat.Törölt ? "1" : "0";

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"FromName ='{Adat.FromName}', GombName ='{Adat.GombName}', GombFelirat ='{Adat.GombFelirat}', ";
                szöveg += $"Szervezet ='{Adat.Szervezet}', Látható ={L}, Törölt ={T} ";
                szöveg += $"WHERE GombokId = {Adat.GombokId}";

                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                throw;
            }
        }
    }
}