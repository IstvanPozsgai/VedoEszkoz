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
    public class Kezelő_Jogosultságok
    {
        readonly string hely;
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Jogosultság";

        private bool IsSQLite => Path.GetExtension(hely).ToLower() == ".db";

        public Kezelő_Jogosultságok()
        {
            string mdbUtvonal = $@"{Application.StartupPath}\Adatok\ÚJ_Belépés.mdb";
            string dbUtvonal = $@"{Application.StartupPath}\Adatok\ÚJ_Belépés.db";

            if (File.Exists(dbUtvonal)) hely = dbUtvonal;
            else hely = mdbUtvonal;

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Jogosultság(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Jogosultság(hely);
        }

        private IDbConnection KapcsolatLétrehozás()
        {
            if (IsSQLite) return new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};");
            return new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}");
        }

        public List<Adat_Jogosultságok> Lista_Adatok(bool Minden = false)
        {
            List<Adat_Jogosultságok> Adatok = new List<Adat_Jogosultságok>();
            string toroltFeltetel = IsSQLite ? "0" : "false";
            string szöveg = Minden ? $"SELECT * FROM {táblanév}" : $"SELECT * FROM {táblanév} WHERE Törölt={toroltFeltetel}";

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
                            bool torolt = IsSQLite ? rekord["Törölt"].ToÉrt_BoolSQLite() : rekord["Törölt"].ToÉrt_Bool();
                            Adat_Jogosultságok Adat = new Adat_Jogosultságok(
                                    rekord["UserId"].ToÉrt_Int(),
                                    rekord["OldalId"].ToÉrt_Int(),
                                    rekord["GombokId"].ToÉrt_Int(),
                                    rekord["SzervezetId"].ToÉrt_Int(),
                                    torolt);
                            Adatok.Add(Adat);
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(List<Adat_Jogosultságok> Adatok)
        {
            try
            {
                List<Adat_Jogosultságok> AdatokRégi = Lista_Adatok(true);
                List<string> SzövegGyR = new List<string>();
                List<string> SzövegGyM = new List<string>();
                foreach (Adat_Jogosultságok Adat in Adatok)
                {
                    string T = IsSQLite ? (Adat.Törölt ? "1" : "0") : Adat.Törölt.ToString();
                    if (!AdatokRégi.Any(a => a.SzervezetId == Adat.SzervezetId && a.UserId == Adat.UserId && a.OldalId == Adat.OldalId && a.GombokId == Adat.GombokId))
                    {
                        string szöveg = $"INSERT INTO {táblanév} (UserId, OldalId, GombokId, SzervezetId, Törölt) VALUES (";
                        szöveg += $"{Adat.UserId}, {Adat.OldalId}, {Adat.GombokId}, {Adat.SzervezetId}, {T})";
                        SzövegGyR.Add(szöveg);
                    }
                    else
                    {
                        string szöveg = $"UPDATE {táblanév} SET Törölt ={T} WHERE SzervezetId = {Adat.SzervezetId} AND ";
                        szöveg += $"UserId ={Adat.UserId} AND OldalId ={Adat.OldalId} AND GombokId ={Adat.GombokId}";
                        SzövegGyM.Add(szöveg);
                    }
                }
                if (SzövegGyR.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGyR);
                if (SzövegGyM.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGyM);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Törlés(Adat_Jogosultságok Adat)
        {
            try
            {
                string T = IsSQLite ? "1" : "true";
                string szöveg = $"UPDATE {táblanév} SET Törölt ={T} WHERE UserId ={Adat.UserId} AND OldalId ={Adat.OldalId} AND GombokId ={Adat.GombokId}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Törlés(List<Adat_Jogosultságok> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                string T = IsSQLite ? "1" : "true";
                foreach (Adat_Jogosultságok Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET Törölt ={T} WHERE UserId ={Adat.UserId} AND OldalId ={Adat.OldalId} AND GombokId ={Adat.GombokId}";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}