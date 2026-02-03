using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VédőEszköz
{
    public class Kezelő_Szervezet
    {
        readonly string hely = Path.Combine(Application.StartupPath, "VédőAdatok", "Alapadatok.db");
        readonly string jelszó = "csavarhúzó";
        readonly string táblanév = "Tábla_Szervezet";
        readonly string connectionString;

        public Kezelő_Szervezet()
        {
            EnsureDirectory();
            connectionString = BuildConnectionString();
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
                Password = jelszó
            }.ToString();
        }

        private void CreateTableIfNotExists()
        {
            var sql = $@"
                CREATE TABLE IF NOT EXISTS {táblanév} (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Szervezet TEXT NOT NULL,
                    Státus INTEGER NOT NULL
                );";

            using var conn = new SqliteConnection(connectionString);
            conn.Open();
            using var cmd = new SqliteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        // READ
        public List<Adat_Szervezet> Lista_Adatok()
        {
            var lista = new List<Adat_Szervezet>();
            var sql = $"SELECT * FROM {táblanév} ORDER BY Id";

            using var conn = new SqliteConnection(connectionString);
            conn.Open();
            using var cmd = new SqliteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Adat_Szervezet(
                    reader["Id"].ToString().ToÉrt_Int(),
                    reader["Szervezet"].ToString().Trim(),
                    reader["Státus"].ToString().ToÉrt_Int() == 1
                ));
            }

            return lista;
        }

        // INSERT vagy UPDATE döntés
        public void Döntés(Adat_Szervezet adat)
        {
            var lista = Lista_Adatok();
            var létezik = lista.FirstOrDefault(a => a.Id == adat.Id);

            if (létezik == null || adat.Id == 0)
            {
                if (lista.Any(a => a.Szervezet == adat.Szervezet && !a.Státus))
                    throw new HibásBevittAdat("Van már ilyen nevű szervezet.");

                Rögzítés(adat);
            }
            else
            {
                Módosítás(adat);
            }
        }

        // INSERT
        public void Rögzítés(Adat_Szervezet adat)
        {
            var sql = $@"
                INSERT INTO {táblanév} (Szervezet, Státus)
                VALUES (@Szervezet, @Status)";

            using var conn = new SqliteConnection(connectionString);
            conn.Open();
            using var cmd = new SqliteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Szervezet", adat.Szervezet);
            cmd.Parameters.AddWithValue("@Status", adat.Státus ? 1 : 0);

            cmd.ExecuteNonQuery();
        }

        // UPDATE
        public void Módosítás(Adat_Szervezet adat)
        {
            var sql = $@"
                UPDATE {táblanév}
                SET Szervezet=@Szervezet,
                    Státus=@Status
                WHERE Id=@Id";

            using var conn = new SqliteConnection(connectionString);
            conn.Open();
            using var cmd = new SqliteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Szervezet", adat.Szervezet);
            cmd.Parameters.AddWithValue("@Status", adat.Státus ? 1 : 0);
            cmd.Parameters.AddWithValue("@Id", adat.Id);

            cmd.ExecuteNonQuery();
        }
    }
}
