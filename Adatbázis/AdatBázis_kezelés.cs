using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.IO;
using VédőEszköz;

public static partial class AdatBázis_kezelés
{
    private static bool IsSQLite(string hely) =>
        hely.ToLower().EndsWith(".db") || hely.ToLower().EndsWith(".sqlite");

    private static IDbConnection KapcsolatLétrehozás(string hely, string jelszó)
    {
        if (IsSQLite(hely))
            return new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};");

        string kapcsolatiszöveg = hely.Contains(".mdb")
            ? $"Provider=Microsoft.Jet.OleDb.4.0;Data Source='{hely}';Jet Oledb:Database Password={jelszó};"
            : $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{hely}';Jet OLEDB:Database Password={jelszó};";

        return new OleDbConnection(kapcsolatiszöveg);
    }

    public static void AB_Adat_Tábla_Létrehozás(string hely, string jelszó, string szöveg, string táblanév)
    {
        try
        {
            if (TáblaEllenőrzés(hely, jelszó, táblanév)) return;

            using (IDbConnection Kapcsolat = KapcsolatLétrehozás(hely, jelszó))
            {
                using (IDbCommand cmd = Kapcsolat.CreateCommand())
                {
                    cmd.CommandText = szöveg;
                    Kapcsolat.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (System.Exception ex)
        {
            HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
        }
    }

    public static void AB_Adat_Bázis_Létrehozás(string hely, string jelszó)
    {
        try
        {
            if (File.Exists(hely)) return;

            if (IsSQLite(hely))
            {
                SQLiteConnection.CreateFile(hely);
            }
            else
            {
                ADOX.Catalog cat = new ADOX.Catalog();
                string kapcsolatiszöveg = hely.Contains(".mdb")
                    ? $"Provider=Microsoft.Jet.OleDb.4.0;Data Source='{hely}';Jet Oledb:Database Password={jelszó};"
                    : $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{hely}';Jet OLEDB:Database Password={jelszó};";

                cat.Create(kapcsolatiszöveg);

                if (cat.ActiveConnection is ADODB.Connection con)
                    con.Close();
            }
        }
        catch (System.Exception ex)
        {
            HibaNapló.Log(ex.Message, "AB_Adat_Bázis_Létrehozás", ex.StackTrace, ex.Source, ex.HResult);
        }
    }

    public static void AB_Új_Oszlop(string hely, string jelszó, string Tábla, string Oszlop, string Típus)
    {
        try
        {
            using (IDbConnection Kapcsolat = KapcsolatLétrehozás(hely, jelszó))
            {
                using (IDbCommand cmd = Kapcsolat.CreateCommand())
                {
                    string szöveg = $"ALTER TABLE {Tábla} ADD COLUMN {Oszlop} {Típus} ";
                    cmd.CommandText = szöveg;
                    Kapcsolat.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (System.Exception ex)
        {
            HibaNapló.Log(ex.Message, "AB_Új_Oszlop", ex.StackTrace, ex.Source, ex.HResult);
        }
    }

    public static bool TáblaEllenőrzés(string hely, string jelszó, string táblanév)
    {
        bool válasz = false;
        try
        {
            using (IDbConnection connection = KapcsolatLétrehozás(hely, jelszó))
            {
                connection.Open();

                if (connection is SQLiteConnection sqliteConn)
                {
                    using (var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=@name", sqliteConn))
                    {
                        cmd.Parameters.AddWithValue("@name", táblanév);
                        var result = cmd.ExecuteScalar();
                        return result != null;
                    }
                }
                else if (connection is OleDbConnection oledbConn)
                {
                    DataTable schemaTable = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow row in schemaTable.Rows)
                    {
                        if (row["TABLE_NAME"].ToString() == táblanév)
                        {
                            válasz = true;
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
        return válasz;
    }
}