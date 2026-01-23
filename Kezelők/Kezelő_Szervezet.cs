using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MyA = Adatbázis;



namespace VédőEszköz
{
    public class Kezelő_Szervezet
    {
        readonly string hely = $@"{Application.StartupPath}\Adatok\Alapadatok.mdb";
        readonly string jelszó = "csavarhúzó";
        readonly string táblanév = "Tábla_Szervezet";

        public Kezelő_Szervezet()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Szervezet(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Szervezet(hely);
        }

        public List<Adat_Szervezet> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY ID";
            List<Adat_Szervezet> Adatok = new List<Adat_Szervezet>();
            Adat_Szervezet Adat;

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
                                Adat = new Adat_Szervezet(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Szervezet"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Szervezet Adat)
        {
            try
            {
                List<Adat_Szervezet> Adatok = Lista_Adatok();
                if (!Adatok.Any(a => a.Id == Adat.Id))
                {
                    if (Adatok.Any(a => a.Szervezet == Adat.Szervezet && a.Státus == false)) throw new HibásBevittAdat("Van már ilyen néven Szervezet létrehozva.");
                    Rögzítés(Adat);
                }
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

        public void Rögzítés(Adat_Szervezet Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} ( Id, Szervezet, státus) VALUES ";
                szöveg += $"({Sorszám()},  '{Adat.Szervezet}', {Adat.Státus})";
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

        public void Módosítás(Adat_Szervezet Adat)
        {
            try
            {

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"Szervezet='{Adat.Szervezet}', ";
                szöveg += $"Státus={Adat.Státus}";
                szöveg += $" WHERE Id={Adat.Id}";
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

        private int Sorszám()
        {
            int válasz = 1;
            try
            {
                List<Adat_Szervezet> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) válasz = Adatok.Max(a => a.Id) + 1;
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
            return válasz;
        }
    }
}
