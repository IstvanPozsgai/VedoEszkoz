using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VédőEszköz;
using MyA = Adatbázis;

namespace VédőEszköz
{
    public class Kezelő_Users
    {
        readonly string hely;
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Users";

        public Kezelő_Users()
        {
            hely = Path.Combine(Application.StartupPath, "VédőAdatok", "Új_Belépés.db");

            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Adatbázis_Users(hely);

            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév))
                Adatbázis_Létrehozás.Adatbázis_Users(hely);
        }

        public List<Adat_Users> Lista_Adatok()
        {
            List<Adat_Users> Adatok = new List<Adat_Users>();
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY UserName";

            using (SQLiteConnection Kapcsolat = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                Kapcsolat.Open();
                using (SQLiteCommand Parancs = new SQLiteCommand(szöveg, Kapcsolat))
                using (var rekord = Parancs.ExecuteReader())
                {
                    while (rekord.Read())
                    {
                        Adatok.Add(new Adat_Users(
                            rekord["UserId"].ToÉrt_Int(),
                            rekord["UserName"].ToString().Trim(),
                            rekord["WinUserName"].ToString().Trim(),
                            rekord["Dolgozószám"].ToString().Trim(),
                            rekord["Password"].ToString().Trim(),
                            rekord["Dátum"].ToÉrt_DaTeTime(),
                            rekord["Frissít"].ToÉrt_Int() == 1,
                            rekord["Törölt"].ToÉrt_Int() == 1,
                            rekord["Szervezetek"].ToString().Trim(),
                            rekord["Szervezet"].ToString().Trim(),
                            rekord["GlobalAdmin"].ToÉrt_Int() == 1,
                            rekord["TelepAdmin"].ToÉrt_Int() == 1
                        ));
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Users Adat)
        {
            List<Adat_Users> Adatok = Lista_Adatok();
            if (!Adatok.Any(a => a.UserId == Adat.UserId))
                Rögzítés(Adat);
            else
                Módosítás(Adat);
        }

        public void Rögzítés(Adat_Users Adat)
        {
            using (var conn = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                conn.Open();

                string sql = $@"
        INSERT INTO {táblanév}
        (UserName, WinUserName, Dolgozószám, [Password], Dátum, Frissít, Törölt, Szervezetek, Szervezet, GlobalAdmin, TelepAdmin)
        VALUES
        (@UserName, @WinUserName, @Dolgozoszam, @Password, @Datum, @Frissit, @Torolt, @Szervezetek, @Szervezet, @GlobalAdmin, @TelepAdmin)";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", Adat.UserName);
                    cmd.Parameters.AddWithValue("@WinUserName", Adat.WinUserName);
                    cmd.Parameters.AddWithValue("@Dolgozoszam", Adat.Dolgozószám);
                    cmd.Parameters.AddWithValue("@Password", Adat.Password);
                    cmd.Parameters.AddWithValue("@Datum", Adat.Dátum.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Frissit", Adat.Frissít ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Torolt", Adat.Törölt ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Szervezetek", Adat.Szervezetek);
                    cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                    cmd.Parameters.AddWithValue("@GlobalAdmin", Adat.GlobalAdmin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@TelepAdmin", Adat.TelepAdmin ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }

        }


        public void Módosítás(Adat_Users Adat)
        {
            using (var conn = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                conn.Open();

                string sql = $@"
            UPDATE {táblanév}
            SET WinUserName = @WinUserName,
                Dátum = @Datum,
                Törölt = @Torolt,
                Szervezetek = @Szervezetek,
                Szervezet = @Szervezet,
                GlobalAdmin = @GlobalAdmin,
                TelepAdmin = @TelepAdmin
            WHERE UserId = @UserId";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@WinUserName", Adat.WinUserName);
                    cmd.Parameters.AddWithValue("@Datum", Adat.Dátum.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Torolt", Adat.Törölt ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Szervezetek", Adat.Szervezetek);
                    cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                    cmd.Parameters.AddWithValue("@GlobalAdmin", Adat.GlobalAdmin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@TelepAdmin", Adat.TelepAdmin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@UserId", Adat.UserId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void MódosításJeszó(Adat_Users Adat)
        {
            using (var conn = new SQLiteConnection($"Data Source={hely};Version=3;Password={jelszó};"))
            {
                conn.Open();

                string sql = $@"
            UPDATE {táblanév}
            SET [Password] = @Password,
                Dátum = @Datum,
                Frissít = @Frissit
            WHERE UserId = @UserId";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", Adat.Password);
                    cmd.Parameters.AddWithValue("@Datum", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Frissit", Adat.Frissít ? 1 : 0);
                    cmd.Parameters.AddWithValue("@UserId", Adat.UserId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}