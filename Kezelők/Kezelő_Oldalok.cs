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
        readonly string hely = $@"{Application.StartupPath}\VédőAdatok\Új_Belépés.db";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Oldalak";

        private bool IsSQLite => Path.GetExtension(hely).ToLower() == ".db";

        public Kezelő_Oldalok()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Oldalak(hely);
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Oldalak(hely);
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
                            Adat_Oldalak Adat = new Adat_Oldalak(
                                    rekord["OldalId"].ToÉrt_Int(),
                                    rekord["FromName"].ToStrTrim(),
                                    rekord["MenuName"].ToStrTrim(),
                                    rekord["MenuFelirat"].ToStrTrim(),
                                    rekord["Látható"].ToÉrt_Bool(),
                                    rekord["Törölt"].ToÉrt_Bool());
                            Adatok.Add(Adat);
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Oldalak Adat)
        {
            try
            {
                string L = IsSQLite ? (Adat.Látható ? "1" : "0") : Adat.Látható.ToString();
                string T = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();

                string szöveg = $"INSERT INTO {táblanév} (FromName, MenuName, MenuFelirat, Látható, Törölt) VALUES (";
                szöveg += $"'{Adat.FromName}', '{Adat.MenuName}', '{Adat.MenuFelirat}', {L}, {T})";
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