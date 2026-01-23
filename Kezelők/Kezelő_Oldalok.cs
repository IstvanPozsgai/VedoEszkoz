using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using MyA = Adatbázis;

namespace VédőEszköz
{
    public class Kezelő_Oldalok
    {
        readonly string hely = $@"{Application.StartupPath}\VédőAdatok\Új_Belépés.mdb";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Oldalak";

        public Kezelő_Oldalok()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Oldalak(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Oldalak(hely);
        }

        public List<Adat_Oldalak> Lista_Adatok()
        {
            List<Adat_Oldalak> Adatok = new List<Adat_Oldalak>();
            string szöveg = $"SELECT * FROM {táblanév}";
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat_Oldalak Adat = new Adat_Oldalak(
                                        rekord["OldalId"].ToÉrt_Int(),
                                        rekord["FromName"].ToStrTrim(),
                                        rekord["MenuName"].ToStrTrim(),
                                        rekord["MenuFelirat"].ToStrTrim(),
                                        rekord["Látható"].ToÉrt_Bool(),
                                        rekord["Törölt"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Oldalak Adat)
        {
            try
            {
                List<Adat_Oldalak> Adatok = Lista_Adatok();
                if (!Adatok.Any(a => a.OldalId == Adat.OldalId))
                    Rögzítés(Adat);
                else
                    Módosítás(Adat);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_Oldalak Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (FromName, MenuName, MenuFelirat, Látható, Törölt) VALUES (";
                szöveg += $"'{Adat.FromName}', '{Adat.MenuName}', '{Adat.MenuFelirat}', {Adat.Látható}, {Adat.Törölt})";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(Adat_Oldalak Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"FromName ='{Adat.FromName}', ";
                szöveg += $"MenuName ='{Adat.MenuName}', ";
                szöveg += $"MenuFelirat ='{Adat.MenuFelirat}', ";
                szöveg += $"Látható ={Adat.Látható}, ";
                szöveg += $"Törölt ={Adat.Törölt} ";
                szöveg += $"WHERE OldalId = {Adat.OldalId}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
