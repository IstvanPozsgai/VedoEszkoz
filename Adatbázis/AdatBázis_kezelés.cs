using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace VédőEszköz
{
    public static partial class AdatBázis_kezelés
    {
        public static void AB_Adat_Bázis_Létrehozás(string hely, string jelszó)
        {
            try
            {
                if (File.Exists(hely)) return;

                using (var conn = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={hely};Password={jelszó};"))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "AB_Adat_Bázis_Létrehozás", ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        public static void AB_Adat_Tábla_Létrehozás(string hely, string jelszó, string szöveg, string naplózandó)
        {
            try
            {
                using (SqliteConnection Kapcsolat = new SqliteConnection($"Data Source={hely};Password={jelszó};"))
                {
                    Kapcsolat.Open();
                    using (SqliteCommand Parancs = new SqliteCommand(szöveg, Kapcsolat))
                    {
                        Parancs.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, naplózandó, ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        public static bool TáblaEllenőrzés(string hely, string jelszó, string táblanév)
        {
            bool van = false;
            try
            {
                using (SqliteConnection Kapcsolat = new SqliteConnection($"Data Source={hely};Password={jelszó};"))
                {
                    Kapcsolat.Open();
                    DataTable dt = Kapcsolat.GetSchema("Tables", new string[] { null, null, táblanév, null });
                    van = dt.Rows.Count > 0;
                }
            }
            catch (Exception) { van = false; }
            return van;
        }

        public static void ABMódosítás(string holvan, string ABjelszó, string SQLszöveg)
        {
            try
            {
                using (SqliteConnection Kapcsolat = new SqliteConnection($"Data Source={holvan};Password={ABjelszó};"))
                {
                    Kapcsolat.Open();
                    using (SqliteCommand Parancs = new SqliteCommand(SQLszöveg, Kapcsolat))
                    {
                        Parancs.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Adat módosítás:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
                throw;
            }
        }

        public static void ABMódosítás(string holvan, string ABjelszó, System.Collections.Generic.List<string> SQLszöveg)
        {
            try
            {
                using (SqliteConnection Kapcsolat = new SqliteConnection($"Data Source={holvan};Password={ABjelszó};"))
                {
                    Kapcsolat.Open();
                    using (var tranzakció = Kapcsolat.BeginTransaction())
                    {
                        foreach (string sql in SQLszöveg)
                        {
                            using (SqliteCommand Parancs = new SqliteCommand(sql, Kapcsolat, tranzakció))
                            {
                                Parancs.ExecuteNonQuery();
                            }
                        }
                        tranzakció.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Tranzakciós hiba:\n{holvan}", ex.StackTrace, ex.Source, ex.HResult);
                throw;
            }
        }
    }
}
