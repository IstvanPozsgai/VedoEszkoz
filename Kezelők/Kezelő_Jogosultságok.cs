using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace VédőEszköz
{
    public class Kezelő_Jogosultságok
    {
        readonly string hely = Path.Combine(Application.StartupPath, "VédőAdatok", "ÚJ_Belépés.db");
        readonly string Password = "ForgalmiUtasítás";
        readonly string TableName = "Tábla_Jogosultság";
        string ConnectionString;

        public Kezelő_Jogosultságok()
        {
            EnsureDirectory();
            ConnectionString = BuildConnectionString();
            CreateTableIfNotExists();
        }

        private void EnsureDirectory()
        {
            var dir = Path.GetDirectoryName(hely);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        private string BuildConnectionString()
        {
            return new SqliteConnectionStringBuilder
            {
                DataSource = hely,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = Password
            }.ToString();
        }

        private void CreateTableIfNotExists()
        {
            var sql = $@"
                CREATE TABLE IF NOT EXISTS {TableName} (
                    UserId INTEGER,
                    OldalId INTEGER,
                    GombokId INTEGER,
                    SzervezetId INTEGER,
                    Törölt INTEGER,
                    PRIMARY KEY(UserId, OldalId, GombokId)
                );";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // READ / SELECT
        public List<Adat_Jogosultságok> Lista_Adatok(bool Minden = false)
        {
            var lista = new List<Adat_Jogosultságok>();
            var sql = Minden
                ? $"SELECT * FROM {TableName}"
                : $"SELECT * FROM {TableName} WHERE Törölt=0";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Adat_Jogosultságok(
                        reader["UserId"].ToString().ToÉrt_Int(),
                        reader["OldalId"].ToString().ToÉrt_Int(),
                        reader["GombokId"].ToString().ToÉrt_Int(),
                        reader["SzervezetId"].ToString().ToÉrt_Int(),
                        reader["Törölt"].ToString().ToÉrt_Int() == 1
                    ));
                }

                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista;
        }

        // INSERT / UPDATE (INSERT OR REPLACE)
        public void Rögzítés(List<Adat_Jogosultságok> adatok)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var tr = connection.BeginTransaction();

                foreach (var a in adatok)
                {
                    var sql = $@"
                        INSERT OR REPLACE INTO {TableName}
                        (UserId, OldalId, GombokId, SzervezetId, Törölt)
                        VALUES (@UserId, @OldalId, @GombokId, @SzervezetId, @Torolt)";

                    using var cmd = new SqliteCommand(sql, connection, tr);
                    cmd.Parameters.AddWithValue("@UserId", a.UserId);
                    cmd.Parameters.AddWithValue("@OldalId", a.OldalId);
                    cmd.Parameters.AddWithValue("@GombokId", a.GombokId);
                    cmd.Parameters.AddWithValue("@SzervezetId", a.SzervezetId);
                    cmd.Parameters.AddWithValue("@Torolt", a.Törölt ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }

                tr.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a mentés során: {ex.Message}");
            }
        }

        // SOFT DELETE
        public void Törlés(Adat_Jogosultságok adat)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                var sql = $@"
                    UPDATE {TableName} 
                    SET Törölt=1
                    WHERE UserId=@UserId AND OldalId=@OldalId AND GombokId=@GombokId";

                using var cmd = new SqliteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@UserId", adat.UserId);
                cmd.Parameters.AddWithValue("@OldalId", adat.OldalId);
                cmd.Parameters.AddWithValue("@GombokId", adat.GombokId);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a törlés során: {ex.Message}");
            }
        }

        // SOFT DELETE több rekord
        public void Törlés(List<Adat_Jogosultságok> adatok)
        {
            foreach (var adat in adatok)
                Törlés(adat);
        }
    }
}
