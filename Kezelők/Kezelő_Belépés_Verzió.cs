using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace VédőEszköz
{
    public class Kezelő_Belépés_Verzió
    {
        readonly string hely = Path.Combine(Application.StartupPath, "VédőAdatok", "Új_Belépés.db");
        readonly string Password = "ForgalmiUtasítás";
        readonly string TableName = "Tábla_Verzió";
        string ConnectionString;

        public Kezelő_Belépés_Verzió()
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
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Verzió REAL
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

        // READ
        public List<Adat_Belépés_Verzió> Lista_Adatok()
        {
            var lista = new List<Adat_Belépés_Verzió>();
            var sql = $"SELECT * FROM {TableName}";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Adat_Belépés_Verzió(
                        reader["id"].ToString().ToÉrt_Long(),
                        reader["Verzió"].ToString().ToÉrt_Double()
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

        // INSERT
        public void Rögzítés(double verzio)
        {
            var sql = $"INSERT INTO {TableName} (Verzió) VALUES (@Verzio)";
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Verzio", verzio);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // UPDATE
        public void Módosítás(long id, double verzio)
        {
            var sql = $"UPDATE {TableName} SET Verzió=@Verzio WHERE id=@Id";
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Verzio", verzio);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
