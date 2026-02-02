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
                            (rekord["Dátum"].ToÉrt_DaTeTime()),
                            (rekord["Frissít"].ToÉrt_Int()) == 1,
                            (rekord["Törölt"].ToÉrt_Int()) == 1,
                            rekord["Szervezetek"].ToString().Trim(),
                            rekord["Szervezet"].ToString().Trim(),
                            (rekord["GlobalAdmin"].ToÉrt_Int()) == 1,
                            (rekord["TelepAdmin"].ToÉrt_Int()) == 1
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
            string frissit = Adat.Frissít ? "1" : "0";
            string torolt = Adat.Törölt ? "1" : "0";
            string gAdmin = Adat.GlobalAdmin ? "1" : "0";
            string tAdmin = Adat.TelepAdmin ? "1" : "0";

            string szöveg = $"INSERT INTO {táblanév} (UserName, WinUserName, Dolgozószám, [Password], Dátum, Frissít, Törölt, Szervezetek, Szervezet, GlobalAdmin, TelepAdmin) VALUES (";
            szöveg += $"'{Adat.UserName}', '{Adat.WinUserName}', '{Adat.Dolgozószám}', '{Adat.Password}', '{Adat.Dátum:yyyy-MM-dd}', {frissit}, {torolt}, '{Adat.Szervezetek}', '{Adat.Szervezet}', {gAdmin}, {tAdmin})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(Adat_Users Adat)
        {
            string torolt = Adat.Törölt ? "1" : "0";
            string gAdmin = Adat.GlobalAdmin ? "1" : "0";
            string tAdmin = Adat.TelepAdmin ? "1" : "0";

            string szöveg = $"UPDATE {táblanév} SET WinUserName ='{Adat.WinUserName}', Dátum ='{Adat.Dátum:yyyy-MM-dd}', ";
            szöveg += $"Törölt ={torolt}, Szervezetek ='{Adat.Szervezetek}', Szervezet ='{Adat.Szervezet}', ";
            szöveg += $"GlobalAdmin={gAdmin}, TelepAdmin={tAdmin} WHERE UserId = {Adat.UserId}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void MódosításJeszó(Adat_Users Adat)
        {
            string frissit = Adat.Frissít ? "1" : "0";
            string szöveg = $"UPDATE {táblanév} SET [Password] ='{Adat.Password}', Dátum ='{DateTime.Today:yyyy-MM-dd}', ";
            szöveg += $"Frissít ={frissit} WHERE UserId = {Adat.UserId}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}