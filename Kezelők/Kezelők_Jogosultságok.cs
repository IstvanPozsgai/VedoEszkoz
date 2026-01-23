using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MyA = Adatbázis;

namespace VédőEszköz
{
    public class Kezelő_Jogosultságok
    {
        readonly string hely = $@"{Application.StartupPath}\Adatok\ÚJ_Belépés.mdb";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Jogosultság";

        public Kezelő_Jogosultságok()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Jogosultság(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Jogosultság(hely);
        }

        public List<Adat_Jogosultságok> Lista_Adatok(bool Minden = false)
        {
            List<Adat_Jogosultságok> Adatok = new List<Adat_Jogosultságok>();

            string szöveg = $"SELECT * FROM {táblanév} WHERE Törölt={false}";
            if (Minden) szöveg = $"SELECT * FROM {táblanév} ";
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
                                Adat_Jogosultságok Adat = new Adat_Jogosultságok(
                                        rekord["UserId"].ToÉrt_Int(),
                                        rekord["OldalId"].ToÉrt_Int(),
                                        rekord["GombokId"].ToÉrt_Int(),
                                        rekord["SzervezetId"].ToÉrt_Int(),
                                        rekord["Törölt"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        /// <summary>
        /// Megnézzük, hogy volt-e rögzítve ha volt és ha nem kell akkor töröljük a régi adatokat és rögzítjük az újakat.
        /// </summary>
        /// <param name="Adatok"></param>
        public void Rögzítés(List<Adat_Jogosultságok> Adatok)
        {
            try
            {
                List<Adat_Jogosultságok> AdatokRégi = Lista_Adatok(true);
                List<string> SzövegGyR = new List<string>();
                List<string> SzövegGyM = new List<string>();
                foreach (Adat_Jogosultságok Adat in Adatok)
                {
                    // Ha a régi adatok között nincs benne akkor rögzítjük az újakat.
                    if (!AdatokRégi.Any(a => a.SzervezetId == Adat.SzervezetId && a.UserId == Adat.UserId && a.OldalId == Adat.OldalId && a.GombokId == Adat.GombokId))
                    {
                        string szöveg = $"INSERT INTO {táblanév} ( UserId, OldalId, GombokId, SzervezetId, Törölt) VALUES (";
                        szöveg += $"{Adat.UserId}, {Adat.OldalId}, {Adat.GombokId}, {Adat.SzervezetId}, {Adat.Törölt})";
                        SzövegGyR.Add(szöveg);
                    }
                    else
                    {
                        string szöveg = $"UPDATE {táblanév} SET ";
                        szöveg += $"Törölt ={Adat.Törölt} ";
                        szöveg += $"WHERE SzervezetId = {Adat.SzervezetId} AND ";
                        szöveg += $"UserId ={Adat.UserId} AND ";
                        szöveg += $"OldalId ={Adat.OldalId} AND ";
                        szöveg += $"GombokId ={Adat.GombokId}";
                        SzövegGyM.Add(szöveg);
                    }
                }
                if (SzövegGyR.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGyR);
                if (SzövegGyM.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGyM);
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


        /// <summary>
        /// Módosítjuk az ablakhoz tartozó jogokat töröltre
        /// </summary>
        /// <param name="Adatok"></param>
        public void Törlés(Adat_Jogosultságok Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"Törölt ={true} ";
                szöveg += $"WHERE UserId ={Adat.UserId} AND ";
                szöveg += $"OldalId ={Adat.OldalId} AND ";
                szöveg += $"GombokId ={Adat.GombokId}";
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


        public void Törlés(List<Adat_Jogosultságok> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jogosultságok Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"Törölt ={true} ";
                    szöveg += $"WHERE UserId ={Adat.UserId} AND ";
                    szöveg += $"OldalId ={Adat.OldalId} AND ";
                    szöveg += $"GombokId ={Adat.GombokId}";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
