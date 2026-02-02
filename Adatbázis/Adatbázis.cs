using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using VédőEszköz;

internal static partial class Adatbázis
{
    private static SQLiteConnection KapcsolatLétrehozás(string holvan, string ABjelszó)
    {
        return new SQLiteConnection($"Data Source={holvan};Version=3;Password={ABjelszó};");
    }

    public static void ABMódosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            using (SQLiteConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                using (SQLiteCommand Parancs = new SQLiteCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
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

    public static void ABMódosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        try
        {
            using (SQLiteConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                Kapcsolat.Open();
                using (var tranzakció = Kapcsolat.BeginTransaction())
                {
                    foreach (string sql in SQLszöveg)
                    {
                        using (SQLiteCommand Parancs = new SQLiteCommand(sql, Kapcsolat, tranzakció))
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

    public static void ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        ABMódosítás(holvan, ABjelszó, SQLszöveg);
    }

    public static bool ABvanTábla(string holvan, string ABjelszó, string SQLszöveg)
    {
        bool válasz = false;
        try
        {
            using (SQLiteConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                using (SQLiteCommand Parancs = new SQLiteCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (SQLiteDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.Read()) válasz = true;
                    }
                }
            }
        }
        catch (Exception) { válasz = false; }
        return válasz;
    }
}