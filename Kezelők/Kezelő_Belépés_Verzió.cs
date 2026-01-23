using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using MyA = Adatbázis;


namespace VédőEszköz
{

    public class Kezelő_Belépés_Verzió
    {
        readonly string hely = $@"{Application.StartupPath}\Adatok\Új_Belépés.mdb";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Verzió";

        public Kezelő_Belépés_Verzió()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Verzió(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Verzió(hely);
        }

        public List<Adat_Belépés_Verzió> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Belépés_Verzió> Adatok = new List<Adat_Belépés_Verzió>();
            Adat_Belépés_Verzió Adat;

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
                                Adat = new Adat_Belépés_Verzió(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["Verzió"].ToÉrt_Double()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Belépés_Verzió Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (id, verzió ) VALUES ({Adat.Id}, {Adat.Verzió})";
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

        public void Módosítás(Adat_Belépés_Verzió Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET verzió={Adat.Verzió}  WHERE ID={Adat.Id}";
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
