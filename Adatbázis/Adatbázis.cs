using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Windows.Forms;
using VédőEszköz;

internal static partial class Adatbázis
{
    private static SqliteConnection KapcsolatLétrehozás(string holvan, string ABjelszó)
    {
        return new SqliteConnection($"Data Source={holvan};Password={ABjelszó};");
    }

    public static void ABMódosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            using (SqliteConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                using (SqliteCommand Parancs = new SqliteCommand(SQLszöveg, Kapcsolat))
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
            using (SqliteConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
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

    public static void ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        ABMódosítás(holvan, ABjelszó, SQLszöveg);
    }

    public static bool ABvanTábla(string holvan, string ABjelszó, string SQLszöveg)
    {
        bool válasz = false;
        try
        {
            using (SqliteConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                using (SqliteCommand Parancs = new SqliteCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (SqliteDataReader rekord = Parancs.ExecuteReader())
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