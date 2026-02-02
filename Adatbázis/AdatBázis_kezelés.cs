using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using VédőEszköz;

public static partial class AdatBázis_kezelés
{
    public static void AB_Adat_Bázis_Létrehozás(string hely, string jelszó)
    {
        try
        {
            if (File.Exists(hely)) return;

            SQLiteConnection.CreateFile(hely);
        }
        catch (System.Exception ex)
        {
            HibaNapló.Log(ex.Message, "AB_Adat_Bázis_Létrehozás", ex.StackTrace, ex.Source, ex.HResult);
        }
    }

    public static void AB_Adat_Tábla_Létrehozás(string hely, string jelszó, string szöveg, string naplózandó)
    {
        using (SQLiteConnection Kapcsolat = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
        {
            try
            {
                Kapcsolat.Open();
                using (SQLiteCommand Parancs = new SQLiteCommand(szöveg, Kapcsolat))
                {
                    Parancs.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, naplózandó, ex.StackTrace, ex.Source, ex.HResult);
            }
        }
    }

    public static bool TáblaEllenőrzés(string hely, string jelszó, string táblanév)
    {
        bool van = false;
        try
        {
            using (SQLiteConnection Kapcsolat = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                Kapcsolat.Open();
                DataTable dt = Kapcsolat.GetSchema("Tables", new string[] { null, null, táblanév, null });
                if (dt.Rows.Count > 0) van = true;
            }
        }
        catch (Exception)
        {
            van = false;
        }
        return van;
    }
}