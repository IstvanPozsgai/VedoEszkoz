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
    public class Kezelő_Oldalok
    {
        readonly string hely;
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Oldalak";

        private bool IsSQLite => Path.GetExtension(hely).ToLower() == ".db";

        public Kezelő_Oldalok()
        {
            // Automatikus választás: ha létezik a .db, azt használja, egyébként az .mdb-t
            string alapUtvonal = $@"{Application.StartupPath}\VédőAdatok\Új_Belépés";

            if (File.Exists(alapUtvonal + ".db"))
                hely = alapUtvonal + ".db";
            else
                hely = alapUtvonal + ".mdb";

            if (!File.Exists(hely))
            {
                if (IsSQLite) Adatbázis_Létrehozás.Adatbázis_Oldalak(hely);
                else Adatbázis_Létrehozás.Adatbázis_Oldalak(hely.KönyvSzerk());
            }

            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév))
                Adatbázis_Létrehozás.Adatbázis_Oldalak(hely);
        }

        private IDbConnection KapcsolatLétrehozás()
        {
            if (IsSQLite)
                return new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};");

            return new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}");
        }

        public List<Adat_Oldalak> Lista_Adatok()
        {
            List<Adat_Oldalak> Adatok = new List<Adat_Oldalak>();
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
                            bool lathato = IsSQLite ? rekord["Látható"].ToÉrt_BoolSQLite() : rekord["Látható"].ToÉrt_Bool();
                            bool torolt = IsSQLite ? rekord["Törölt"].ToÉrt_BoolSQLite() : rekord["Törölt"].ToÉrt_Bool();

                            Adat_Oldalak Adat = new Adat_Oldalak(
                                    rekord["OldalId"].ToÉrt_Int(),
                                    rekord["FromName"].ToStrTrim(),
                                    rekord["MenuName"].ToStrTrim(),
                                    rekord["MenuFelirat"].ToStrTrim(),
                                    lathato,
                                    torolt);
                            Adatok.Add(Adat);
                        }
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

        public void Rögzítés(Adat_Oldalak Adat)
        {
            try
            {
                // Értékek átalakítása az adatbázis típusának megfelelően
                string L = IsSQLite ? (Adat.Látható ? "1" : "0") : Adat.Látható.ToString();
                string T = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();

                string szöveg = $"INSERT INTO {táblanév} (FromName, MenuName, MenuFelirat, Látható, Törölt) VALUES (";
                szöveg += $"'{Adat.FromName}', '{Adat.MenuName}', '{Adat.MenuFelirat}', {L}, {T})";
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

        public void Módosítás(Adat_Oldalak Adat)
        {
            try
            {
                string L = IsSQLite ? (Adat.Látható ? "1" : "0") : Adat.Látható.ToString();
                string T = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"FromName ='{Adat.FromName}', ";
                szöveg += $"MenuName ='{Adat.MenuName}', ";
                szöveg += $"MenuFelirat ='{Adat.MenuFelirat}', ";
                szöveg += $"Látható ={L}, ";
                szöveg += $"Törölt ={T} ";
                szöveg += $"WHERE OldalId = {Adat.OldalId}";
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