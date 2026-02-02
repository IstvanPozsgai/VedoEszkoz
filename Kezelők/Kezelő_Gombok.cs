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

        private bool IsSQLite => Path.GetExtension(hely).ToLower() == ".db" || Path.GetExtension(hely).ToLower() == ".sqlite";

        public Kezelő_Gombok()
        {
            string alapUtvonal = $@"{Application.StartupPath}\VédőAdatok\ÚJ_Belépés";

            if (File.Exists(alapUtvonal + ".db"))
                hely = alapUtvonal + ".db";
            else if (File.Exists(alapUtvonal + ".mdb"))
                hely = alapUtvonal + ".mdb";
            else
            {
                hely = alapUtvonal + ".mdb";
                Adatbázis_Létrehozás.Adatbázis_Gombok(hely);
            }

            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév))
                Adatbázis_Létrehozás.Adatbázis_Gombok(hely);
        }

        private IDbConnection KapcsolatLétrehozás()
        {
            if (IsSQLite)
                return new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};");

            return new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}");
        }

        public List<Adat_Gombok> Lista_Adatok()
        {
            List<Adat_Gombok> Adatok = new List<Adat_Gombok>();
            string szöveg = $"SELECT * FROM {táblanév}";

            using (IDbConnection Kapcsolat = KapcsolatLétrehozás())
            {
                using (IDbCommand Parancs = Kapcsolat.CreateCommand())
                {
                    Parancs.CommandText = szöveg;
                    Kapcsolat.Open();
                    using (IDataReader rekord = Parancs.ExecuteReader())
                    {
                        while (rekord.Read())
                        {
                            Adat_Gombok Adat = new Adat_Gombok(
                                    rekord["GombokId"].ToÉrt_Int(),
                                    rekord["FromName"].ToStrTrim(),
                                    rekord["GombName"].ToStrTrim(),
                                    rekord["GombFelirat"].ToStrTrim(),
                                    rekord["Szervezet"].ToStrTrim(),
                                    rekord["Látható"].ToÉrt_Bool(),
                                    rekord["Törölt"].ToÉrt_Bool());
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
                // SQLite esetén 1/0, MDB esetén True/False használatos
                string lathato = IsSQLite ? (Adat.Látható ? "1" : "0") : Adat.Látható.ToString();
                string torolt = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();

                string szöveg = $"INSERT INTO {táblanév} (FromName, GombName, GombFelirat, Szervezet, Látható, Törölt) VALUES (";
                szöveg += $"'{Adat.FromName}', '{Adat.GombName}', '{Adat.GombFelirat}', '{Adat.Szervezet}', {lathato}, {torolt})";
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
                string lathato = IsSQLite ? (Adat.Látható ? "1" : "0") : Adat.Látható.ToString();
                string torolt = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"FromName ='{Adat.FromName}', ";
                szöveg += $"GombName ='{Adat.GombName}', ";
                szöveg += $"GombFelirat ='{Adat.GombFelirat}', ";
                szöveg += $"Szervezet ='{Adat.Szervezet}', ";
                szöveg += $"Látható ={lathato}, ";
                szöveg += $"Törölt ={torolt} ";
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