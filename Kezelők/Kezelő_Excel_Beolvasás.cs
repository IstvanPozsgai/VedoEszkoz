using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using MyA = Adatbázis;


namespace VédőEszköz
{
    public class Kezelő_Excel_Beolvasás
    {
        readonly string hely = $@"{Application.StartupPath}\Adatok\Beolvasás.mdb";
        readonly string jelszó = "sajátmagam";
        readonly string táblanév = "Tábla_Excel_Beolvasás";
        public Kezelő_Excel_Beolvasás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Excel_Beolvasás(hely.KönyvSzerk());
        }

        public List<Adat_Excel_Beolvasás> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Excel_Beolvasás> Adatok = new List<Adat_Excel_Beolvasás>();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                string érték = rekord["Változónév"].ToStrTrim();
                                Adat_Excel_Beolvasás Adat = new Adat_Excel_Beolvasás(
                                        rekord["csoport"].ToStrTrim(),
                                        rekord["oszlop"].ToÉrt_Int(),
                                        rekord["fejléc"].ToStrTrim(),
                                        rekord["Státusz"].ToÉrt_Bool(),
                                        rekord["Változónév"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Excel_Beolvasás Adat)
        {
            string szöveg = $"INSERT INTO {táblanév} ";
            szöveg += " ( csoport, oszlop, fejléc, Státusz , Változónév )";
            szöveg += " VALUES ";
            szöveg += $" ('{Adat.Csoport}', {Adat.Oszlop}, '{Adat.Fejléc}', {Adat.Státusz}, '{Adat.Változónév}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Rögzítés(List<Adat_Excel_Beolvasás> Adatok)
        {
            List<string> szövegek = new List<string>();
            foreach (Adat_Excel_Beolvasás Adat in Adatok)
            {
                string szöveg = $"INSERT INTO {táblanév} ";
                szöveg += " ( csoport, oszlop, fejléc, Státusz , Változónév )";
                szöveg += " VALUES ";
                szöveg += $" ('{Adat.Csoport}', {Adat.Oszlop}, '{Adat.Fejléc}', {Adat.Státusz}, '{Adat.Változónév}')";
                szövegek.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, szövegek);
        }

        public void Módosítás(Adat_Excel_Beolvasás Adat)
        {
            try
            {
                string szöveg = $"UPDATE  {táblanév} SET ";
                szöveg += $" fejléc='{Adat.Fejléc}', ";
                szöveg += $" Változónév='{Adat.Változónév}'";
                szöveg += $" WHERE [csoport]= '{Adat.Csoport}' and [oszlop]={Adat.Oszlop}";
                szöveg += $" and [Státusz]={Adat.Státusz}";
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

        public void Törlés(Adat_Excel_Beolvasás Adat)
        {
            try
            {
                string szöveg = $"UPDATE  {táblanév} SET ";
                szöveg += $" Státusz=true";
                szöveg += $" WHERE [csoport]= '{Adat.Csoport}'  and [oszlop]={Adat.Oszlop}";
                szöveg += $" and [Státusz]='{Adat.Státusz}'";
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
