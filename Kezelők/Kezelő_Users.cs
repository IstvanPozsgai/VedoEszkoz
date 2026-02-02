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
    public class Kezelő_Users
    {
        readonly string hely;
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Users";

        private bool IsSQLite => Path.GetExtension(hely).ToLower() == ".db";

        public Kezelő_Users()
        {
            string mdbUtvonal = $@"{Application.StartupPath}\VédőAdatok\Új_Belépés.mdb";
            string dbUtvonal = $@"{Application.StartupPath}\VédőAdatok\Új_Belépés.db";

            if (File.Exists(dbUtvonal)) hely = dbUtvonal;
            else hely = mdbUtvonal;

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Users(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Users(hely);
        }

        private IDbConnection KapcsolatLétrehozás()
        {
            if (IsSQLite) return new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};");
            return new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}");
        }

        public List<Adat_Users> Lista_Adatok()
        {
            List<Adat_Users> Adatok = new List<Adat_Users>();
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
                            Adat_Users Adat = new Adat_Users(
                                    rekord["UserId"].ToÉrt_Int(),
                                    rekord["UserName"].ToStrTrim(),
                                    rekord["WinUserName"].ToStrTrim(),
                                    rekord["Dolgozószám"].ToStrTrim(),
                                    rekord["Password"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    IsSQLite ? rekord["Frissít"].ToÉrt_BoolSQLite() : rekord["Frissít"].ToÉrt_Bool(),
                                    IsSQLite ? rekord["Törölt"].ToÉrt_BoolSQLite() : rekord["Törölt"].ToÉrt_Bool(),
                                    rekord["Szervezetek"].ToStrTrim(),
                                    rekord["Szervezet"].ToStrTrim(),
                                    IsSQLite ? rekord["GlobalAdmin"].ToÉrt_BoolSQLite() : rekord["GlobalAdmin"].ToÉrt_Bool(),
                                    IsSQLite ? rekord["TelepAdmin"].ToÉrt_BoolSQLite() : rekord["TelepAdmin"].ToÉrt_Bool()
                            );
                            Adatok.Add(Adat);
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Users Adat)
        {
            try
            {
                List<Adat_Users> Adatok = Lista_Adatok();
                if (!Adatok.Any(a => a.UserId == Adat.UserId)) Rögzítés(Adat);
                else Módosítás(Adat);
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

        public void Rögzítés(Adat_Users Adat)
        {
            try
            {
                string pword = string.IsNullOrEmpty(Adat.Password.Trim()) ? "123456" : Adat.Password;
                string frissit = IsSQLite ? "1" : "true";
                string torolt = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();
                string gAdmin = IsSQLite ? (Adat.GlobalAdmin ? "1" : "0") : Adat.GlobalAdmin.ToString();
                string tAdmin = IsSQLite ? (Adat.TelepAdmin ? "1" : "0") : Adat.TelepAdmin.ToString();

                string szöveg = $"INSERT INTO {táblanév} (UserName, WinUserName, Dolgozószám, [Password], Dátum, frissít, Törölt, Szervezetek, Szervezet, GlobalAdmin, TelepAdmin) VALUES (";
                szöveg += $"'{Adat.UserName}', '{Adat.WinUserName}', '{Adat.Dolgozószám}', '{pword}', '{Adat.Dátum:yyyy.MM.dd}', {frissit}, {torolt},";
                szöveg += $" '{Adat.Szervezetek}', '{Adat.Szervezet}', {gAdmin}, {tAdmin})";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(Adat_Users Adat)
        {
            try
            {
                string torolt = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();
                string gAdmin = IsSQLite ? (Adat.GlobalAdmin ? "1" : "0") : Adat.GlobalAdmin.ToString();
                string tAdmin = IsSQLite ? (Adat.TelepAdmin ? "1" : "0") : Adat.TelepAdmin.ToString();

                string szöveg = $"UPDATE {táblanév} SET WinUserName ='{Adat.WinUserName}', Dátum ='{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"Törölt ={torolt}, Szervezetek ='{Adat.Szervezetek}', Szervezet ='{Adat.Szervezet}', ";
                szöveg += $"GlobalAdmin={gAdmin}, TelepAdmin={tAdmin} WHERE UserId = {Adat.UserId}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MódosításJeszó(Adat_Users Adat)
        {
            try
            {
                string frissit = IsSQLite ? (Adat.Frissít ? "1" : "0") : Adat.Frissít.ToString();
                string szöveg = $"UPDATE {táblanév} SET [Password] ='{Adat.Password}', Dátum ='{DateTime.Today:yyyy.MM.dd}', ";
                szöveg += $"Frissít ={frissit} WHERE UserId = {Adat.UserId}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}