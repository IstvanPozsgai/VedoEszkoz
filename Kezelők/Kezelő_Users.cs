using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VédőEszköz
{
    public class Kezelő_Users
    {
        readonly string hely = Path.Combine(Application.StartupPath, "VédőAdatok", "Új_Belépés.db");
		readonly string Password = "ForgalmiUtasítás";
        readonly string TableName = "Tábla_Users";
        string ConnectionString;

        public Kezelő_Users()
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
                    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserName TEXT,
                    WinUserName TEXT,
                    Dolgozószám TEXT,
                    Password TEXT,
                    Dátum TEXT,
                    Frissít INTEGER,
                    Törölt INTEGER,
                    Szervezetek TEXT,
                    Szervezet TEXT,
                    GlobalAdmin INTEGER,
                    TelepAdmin INTEGER
                );";
			try
            {
				var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
		public void Döntés(Adat_Users adat)
		{
			var lista = Lista_Adatok();
			var meglévő = lista.FirstOrDefault(u => u.UserId == adat.UserId);

			if (meglévő == null || adat.UserId == 0)
				Rögzítés(adat);
			else
				Módosítás(adat);
		}

		// CREATE / INSERT
		public void Rögzítés(Adat_Users adat)
        {
            var sql = $@"
                INSERT INTO {TableName}
                (UserName, WinUserName, Dolgozószám, Password, Dátum, Frissít, Törölt, Szervezetek, Szervezet, GlobalAdmin, TelepAdmin)
                VALUES
                (@UserName, @WinUserName, @Dolgozoszam, @Password, @Datum, @Frissit, @Torolt, @Szervezetek, @Szervezet, @GlobalAdmin, @TelepAdmin)";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);

                cmd.Parameters.AddWithValue("@UserName", adat.UserName);
                cmd.Parameters.AddWithValue("@WinUserName", adat.WinUserName);
                cmd.Parameters.AddWithValue("@Dolgozoszam", adat.Dolgozószám);
                cmd.Parameters.AddWithValue("@Password", adat.Password);
                cmd.Parameters.AddWithValue("@Datum", adat.DátumSQL);
                cmd.Parameters.AddWithValue("@Frissit", adat.FrissítInt);
                cmd.Parameters.AddWithValue("@Torolt", adat.TöröltInt);
                cmd.Parameters.AddWithValue("@Szervezetek", adat.Szervezetek);
                cmd.Parameters.AddWithValue("@Szervezet", adat.Szervezet);
                cmd.Parameters.AddWithValue("@GlobalAdmin", adat.GlobalAdminInt);
                cmd.Parameters.AddWithValue("@TelepAdmin", adat.TelepAdminInt);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // READ / SELECT
        public List<Adat_Users> Lista_Adatok()
        {
            var lista = new List<Adat_Users>();
            var sql = $"SELECT * FROM {TableName}";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Adat_Users(
                        reader["UserId"].ToString().ToÉrt_Int(),
                        reader["UserName"].ToString().Trim(),
                        reader["WinUserName"].ToString().Trim(),
                        reader["Dolgozószám"].ToString().Trim(),
                        reader["Password"].ToString().Trim(),
                        reader["Dátum"].ToString().ToÉrt_DaTeTime(),
                        reader["Frissít"].ToString().ToÉrt_Int() == 1,
                        reader["Törölt"].ToString().ToÉrt_Int() == 1,
                        reader["Szervezetek"].ToString().Trim(),
                        reader["Szervezet"].ToString().Trim(),
                        reader["GlobalAdmin"].ToString().ToÉrt_Int() == 1,
                        reader["TelepAdmin"].ToString().ToÉrt_Int() == 1
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

        // UPDATE általános
        public void Módosítás(Adat_Users adat)
        {
            var sql = $@"
                UPDATE {TableName} SET
                    WinUserName=@WinUserName,
                    Dátum=@Datum,
                    Törölt=@Torolt,
                    Szervezetek=@Szervezetek,
                    Szervezet=@Szervezet,
                    GlobalAdmin=@GlobalAdmin,
                    TelepAdmin=@TelepAdmin
                WHERE UserId=@UserId";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);

                cmd.Parameters.AddWithValue("@WinUserName", adat.WinUserName);
                cmd.Parameters.AddWithValue("@Datum", adat.DátumSQL);
                cmd.Parameters.AddWithValue("@Torolt", adat.TöröltInt);
                cmd.Parameters.AddWithValue("@Szervezetek", adat.Szervezetek);
                cmd.Parameters.AddWithValue("@Szervezet", adat.Szervezet);
                cmd.Parameters.AddWithValue("@GlobalAdmin", adat.GlobalAdminInt);
                cmd.Parameters.AddWithValue("@TelepAdmin", adat.TelepAdminInt);
                cmd.Parameters.AddWithValue("@UserId", adat.UserId);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // UPDATE jelszó
        public void MódosításJelszó(Adat_Users adat)
        {
            var sql = $@"
                UPDATE {TableName} SET
                    Password=@Password,
                    Dátum=@Datum,
                    Frissít=@Frissit
                WHERE UserId=@UserId";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Password", adat.Password);
                cmd.Parameters.AddWithValue("@Datum", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Frissit", adat.FrissítInt);
                cmd.Parameters.AddWithValue("@UserId", adat.UserId);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // DELETE
        public void DeleteData(int userId)
        {
            var sql = $"DELETE FROM {TableName} WHERE UserId=@UserId";

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = new SqliteCommand(sql, connection);

                cmd.Parameters.AddWithValue("@UserId", userId);
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
