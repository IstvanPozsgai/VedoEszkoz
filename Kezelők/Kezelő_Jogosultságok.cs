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

        public void Rögzítés(List<Adat_Jogosultságok> adatok)
        {
            using (var conn = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                conn.Open();
                using (var tr = conn.BeginTransaction())
                {
                    foreach (var a in adatok)
                    {
                        string sql = $@"
                    INSERT OR REPLACE INTO {táblanév}
                    (UserId, OldalId, GombokId, SzervezetId, Törölt)
                    VALUES
                    (@UserId, @OldalId, @GombokId, @SzervezetId, @Torolt)";

                        using (var cmd = new SQLiteCommand(sql, conn, tr))
                        {
                            cmd.Parameters.AddWithValue("@UserId", a.UserId);
                            cmd.Parameters.AddWithValue("@OldalId", a.OldalId);
                            cmd.Parameters.AddWithValue("@GombokId", a.GombokId);
                            cmd.Parameters.AddWithValue("@SzervezetId", a.SzervezetId);
                            cmd.Parameters.AddWithValue("@Torolt", a.Törölt ? 1 : 0);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    tr.Commit();
                }
            }
        }

        public void Törlés(Adat_Jogosultságok Adat)
        {
            using (var conn = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                conn.Open();

                string sql = $@"
            UPDATE {táblanév}
            SET Törölt=1
            WHERE UserId=@UserId AND OldalId=@OldalId AND GombokId=@GombokId";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                    cmd.Parameters.AddWithValue("@OldalId", Adat.OldalId);
                    cmd.Parameters.AddWithValue("@GombokId", Adat.GombokId);
                    cmd.ExecuteNonQuery();
                }
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