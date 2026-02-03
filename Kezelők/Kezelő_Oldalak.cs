using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace VédőEszköz
{
    public class Kezelő_Oldalak
    {
        readonly string hely = Path.Combine(Application.StartupPath, "VédőAdatok", "Új_Belépés.db");
        readonly string Password = "ForgalmiUtasítás";
        readonly string TableName = "Tábla_Oldalak";
        string ConnectionString;

        public Kezelő_Oldalak()
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
                    OldalId INTEGER PRIMARY KEY AUTOINCREMENT,
                    FromName TEXT,
                    MenuName TEXT,
                    MenuFelirat TEXT,
                    Látható INTEGER,
                    Törölt INTEGER
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

        // CREATE / INSERT
        public void InsertData(Adat_Oldalak adat)
        {
            var sql = $@"
                INSERT INTO {TableName}
                (FromName, MenuName, MenuFelirat, Látható, Törölt)
                VALUES (@FromName, @MenuName, @MenuFelirat, @Lathato, @Torolt)";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);

                cmd.Parameters.AddWithValue("@FromName", adat.FromName);
                cmd.Parameters.AddWithValue("@MenuName", adat.MenuName);
                cmd.Parameters.AddWithValue("@MenuFelirat", adat.MenuFelirat);
                cmd.Parameters.AddWithValue("@Lathato", adat.Látható ? 1 : 0);
                cmd.Parameters.AddWithValue("@Torolt", adat.Törölt ? 1 : 0);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // READ / SELECT
        public List<Adat_Oldalak> Lista_Adatok()
        {
            var lista = new List<Adat_Oldalak>();
            var sql = $"SELECT * FROM {TableName}";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Adat_Oldalak(
                        reader["OldalId"].ToString().ToÉrt_Int(),
                        reader["FromName"].ToString().Trim(),
                        reader["MenuName"].ToString().Trim(),
                        reader["MenuFelirat"].ToString().Trim(),
                        reader["Látható"].ToString().ToÉrt_Int() == 1,
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

        // UPDATE
        public void UpdateData(Adat_Oldalak adat)
        {
            var sql = $@"
                UPDATE {TableName} SET
                    FromName=@FromName,
                    MenuName=@MenuName,
                    MenuFelirat=@MenuFelirat,
                    Látható=@Lathato,
                    Törölt=@Torolt
                WHERE OldalId=@OldalId";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);

                cmd.Parameters.AddWithValue("@FromName", adat.FromName);
                cmd.Parameters.AddWithValue("@MenuName", adat.MenuName);
                cmd.Parameters.AddWithValue("@MenuFelirat", adat.MenuFelirat);
                cmd.Parameters.AddWithValue("@Lathato", adat.Látható ? 1 : 0);
                cmd.Parameters.AddWithValue("@Torolt", adat.Törölt ? 1 : 0);
                cmd.Parameters.AddWithValue("@OldalId", adat.OldalId);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // DELETE
        public void DeleteData(int oldalId)
        {
            var sql = $"DELETE FROM {TableName} WHERE OldalId=@OldalId";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);

                cmd.Parameters.AddWithValue("@OldalId", oldalId);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Döntés (INSERT vagy UPDATE)
        public void Döntés(Adat_Oldalak adat)
        {
            try
            {
                var lista = Lista_Adatok();
                if (!lista.Exists(a => a.OldalId == adat.OldalId))
                    InsertData(adat);
                else
                    UpdateData(adat);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az adatok mentésekor: {ex.Message}");
            }
        }
    }
}
