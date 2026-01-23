using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VédőEszköz;

internal static partial class Adatbázis
{

    /// <summary>
    /// Adatbázisban módosít a küldött szöveg alapján (SQL)
    /// </summary>
    /// <param name="holvan"> A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó"> Adatbázis jelszó </param>
    /// <param name="SQLszöveg"> SQl módosítási szöveg </param>
    public static void ABMódosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            string kapcsolatiszöveg = "";
            if (holvan.Contains(".mdb"))
                kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";
            else
                kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{holvan}'; Jet OLEDB:Database Password ={ABjelszó}";

            // módosítjuk az adatokat
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg, Kapcsolat))
                {
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


    /// <summary>
    /// Adatbázisban módosít a küldött szöveg alapján (SQL)
    /// </summary>
    /// <param name="holvan"> A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó"> Adatbázis jelszó </param>
    /// <param name="SQLszöveg">Lista SQl módosítási szöveg </param>
    public static void ABMódosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        bool hiba = false;
        string szöveg = "";
        try
        {
            // módosítjuk az adatokat
            string kapcsolatiszöveg = "";
            if (holvan.Contains(".mdb"))
                kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";
            else
                kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{holvan}'; Jet OLEDB:Database Password ={ABjelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                for (int i = 0; i < SQLszöveg.Count; i++)
                {
                    try
                    {
                        szöveg = SQLszöveg[i];
                        using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg[i], Kapcsolat))
                        {
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="holvan">A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó">Adatbázis jelszó</param>
    /// <param name="SQLszöveg">SQl módosítási szöveg </param>
    public static void ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            // módosítjuk az adatokat
            string kapcsolatiszöveg = "";
            if (holvan.Contains(".mdb"))
                kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";
            else
                kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{holvan}'; Jet OLEDB:Database Password ={ABjelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg, Kapcsolat))
                {
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="holvan">A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó">Adatbázis jelszó</param>
    /// <param name="SQLszöveg">Lista SQl módosítási szöveg </param>
    public static void ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        try
        {
            // módosítjuk az adatokat
            string kapcsolatiszöveg = "";
            if (holvan.Contains(".mdb"))
                kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";
            else
                kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{holvan}'; Jet OLEDB:Database Password ={ABjelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                for (int i = 0; i < SQLszöveg.Count; i++)
                {
                    try
                    {
                        using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg[i], Kapcsolat))
                        {
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
            string kapcsolatiszöveg = "";
            if (holvan.Contains(".mdb"))
                kapcsolatiszöveg = "Provider=Microsoft.Jet.OleDb.4.0;Data Source= '" + holvan + "'; Jet Oledb:Database Password=" + ABjelszó;
            else
                kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{holvan}'; Jet OLEDB:Database Password ={ABjelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
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