using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        private bool IsSQLite => Path.GetExtension(hely).ToLower() == ".db";

        public Kezelő_Gombok()
        {
            string mdbUtvonal = $@"{Application.StartupPath}\VédőAdatok\ÚJ_Belépés.mdb";
            string dbUtvonal = $@"{Application.StartupPath}\VédőAdatok\ÚJ_Belépés.db";

            if (File.Exists(dbUtvonal)) hely = dbUtvonal;
            else hely = mdbUtvonal;

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Gombok(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Gombok(hely);
        }

        private IDbConnection KapcsolatLétrehozás()
        {
            if (IsSQLite) return new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};");
            return new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}");
        }

        public List<Adat_Gombok> Lista_Adatok()
        {
            List<Adat_Gombok> Adatok = new List<Adat_Gombok>();
            using (IDbConnection Kapcsolat = KapcsolatLétrehozás())
            {
                using (IDbCommand Parancs = Kapcsolat.CreateCommand())
                {
                    Parancs.CommandText = $"SELECT * FROM {táblanév}";
                    Kapcsolat.Open();
                    using (IDataReader rekord = Parancs.ExecuteReader())
                    {
                        while (rekord.Read())
                        {
                            bool lathato = IsSQLite ? rekord["Látható"].ToÉrt_BoolSQLite() : rekord["Látható"].ToÉrt_Bool();
                            bool torolt = IsSQLite ? rekord["Törölt"].ToÉrt_BoolSQLite() : rekord["Törölt"].ToÉrt_Bool();

                            Adat_Gombok Adat = new Adat_Gombok(
                                    rekord["GombokId"].ToÉrt_Int(),
                                    rekord["FromName"].ToStrTrim(),
                                    rekord["GombName"].ToStrTrim(),
                                    rekord["GombFelirat"].ToStrTrim(),
                                    rekord["Szervezet"].ToStrTrim(),
                                    lathato,
                                    torolt);
                            Adatok.Add(Adat);
                        }
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
                Adat_Gombok gomb = (from a in Adatok
                                    where a.GombName == Adat.GombName
                                    && a.FromName == Adat.FromName
                                    && a.Törölt == false
                                    select a).FirstOrDefault();
                if (gomb == null && Adat.GombokId == 0)
                    Rögzítés(Adat);
                else
                {
                    Adat_Gombok gomb1 = (from a in Adatok
                                         where a.GombName == Adat.GombName
                                         && a.FromName == Adat.FromName
                                          && a.GombokId == Adat.GombokId
                                         select a).FirstOrDefault();
                    if (gomb1 != null)
                        Módosítás(Adat);
                    else
                    {
                        throw new HibásBevittAdat($"Ez a {gomb1.GombokId} szám alatt már szerepel!");
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_Gombok Adat)
        {
            try
            {
                string L = IsSQLite ? (Adat.Látható ? "1" : "0") : Adat.Látható.ToString();
                string T = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();

                string szöveg = $"INSERT INTO {táblanév} (FromName, GombName, GombFelirat, Szervezet, Látható, Törölt) VALUES (";
                szöveg += $"'{Adat.FromName}', '{Adat.GombName}', '{Adat.GombFelirat}', '{Adat.Szervezet}', {L}, {T})";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(Adat_Gombok Adat)
        {
            try
            {
                string L = IsSQLite ? (Adat.Látható ? "1" : "0") : Adat.Látható.ToString();
                string T = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"FromName ='{Adat.FromName}', GombName ='{Adat.GombName}', GombFelirat ='{Adat.GombFelirat}', ";
                szöveg += $"Szervezet ='{Adat.Szervezet}', Látható ={L}, Törölt ={T} ";
                szöveg += $"WHERE GombokId = {Adat.GombokId}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}