using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using VédőEszköz;

internal static partial class Adatbázis
{
    private static bool IsSQLite(string holvan) =>
        holvan.ToLower().EndsWith(".db") || holvan.ToLower().EndsWith(".sqlite");

    private static IDbConnection KapcsolatLétrehozás(string holvan, string ABjelszó)
    {
        if (IsSQLite(holvan))
            return new SQLiteConnection($"Data Source={holvan};Version=3;Password={ABjelszó};");

        string kapcsolatiszöveg = holvan.Contains(".mdb")
            ? $"Provider=Microsoft.Jet.OleDb.4.0;Data Source='{holvan}';Jet Oledb:Database Password={ABjelszó}"
            : $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{holvan}';Jet OLEDB:Database Password={ABjelszó}";

        return new OleDbConnection(kapcsolatiszöveg);
    }

    public static void ABMódosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            using (IDbConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                using (IDbCommand Parancs = Kapcsolat.CreateCommand())
                {
                    Parancs.CommandText = SQLszöveg;
                    Kapcsolat.Open();
                    Parancs.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Adat módosítás:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
        }
    }

    public static void ABMódosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        bool hiba = false;
        string szöveg = "";
        try
        {
            using (IDbConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                Kapcsolat.Open();
                for (int i = 0; i < SQLszöveg.Count; i++)
                {
                    try
                    {
                        szöveg = SQLszöveg[i];
                        using (IDbCommand Parancs = Kapcsolat.CreateCommand())
                        {
                            Parancs.CommandText = SQLszöveg[i];
                            Parancs.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        HibaNapló.Log(ex.Message, $"Adat módosítás:\n{holvan}\n{szöveg}", ex.StackTrace, ex.Source, ex.HResult);
                        hiba = true;
                        continue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Adat módosítás:\n{holvan}\n{szöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
        }
        if (hiba) throw new Exception("Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
    }

    public static void ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            using (IDbConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                using (IDbCommand Parancs = Kapcsolat.CreateCommand())
                {
                    Parancs.CommandText = SQLszöveg;
                    Kapcsolat.Open();
                    Parancs.ExecuteScalar();
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Adat törlés:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis törlési hiba, az adotok törlése nem történt meg.");
        }
    }

    public static void ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        try
        {
            using (IDbConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                Kapcsolat.Open();
                for (int i = 0; i < SQLszöveg.Count; i++)
                {
                    try
                    {
                        using (IDbCommand Parancs = Kapcsolat.CreateCommand())
                        {
                            Parancs.CommandText = SQLszöveg[i];
                            Parancs.ExecuteScalar();
                        }
                    }
                    catch (Exception ex)
                    {
                        HibaNapló.Log(ex.Message, $"Adat módosítás:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
                        continue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Adat törlés:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis törlési hiba, az adotok törlése nem történt meg.");
        }
    }

    public static bool ABvanTábla(string holvan, string ABjelszó, string SQLszöveg)
    {
        bool válasz = false;
        try
        {
            using (IDbConnection Kapcsolat = KapcsolatLétrehozás(holvan, ABjelszó))
            {
                using (IDbCommand Parancs = Kapcsolat.CreateCommand())
                {
                    Parancs.CommandText = SQLszöveg;
                    Kapcsolat.Open();
                    using (IDataReader rekord = Parancs.ExecuteReader())
                    {
                        válasz = true;
                    }
                }
            }
            return válasz;
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "ABvanTábla", ex.StackTrace, ex.Source, ex.HResult, "_", false);
            return válasz;
        }
    }
}