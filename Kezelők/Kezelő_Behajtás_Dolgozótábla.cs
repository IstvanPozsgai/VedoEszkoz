using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

using MyA = Adatbázis;


namespace VédőEszköz
{
    public class Kezelő_Behajtás_Dolgozótábla
    {
        readonly string jelszó = "egérpad";
        readonly string hely = $@"{Application.StartupPath}\Adatok\Behajtási_alap.mdb";

        public Kezelő_Behajtás_Dolgozótábla()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Alap(hely.KönyvSzerk());
        }

        public List<Adat_Behajtás_Dolgozótábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM Dolgozóktábla";
            List<Adat_Behajtás_Dolgozótábla> Adatok = new List<Adat_Behajtás_Dolgozótábla>();
            Adat_Behajtás_Dolgozótábla Adat;

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
                                Adat = new Adat_Behajtás_Dolgozótábla(
                                    rekord["Dolgozószám"].ToStrTrim(),
                                    rekord["Dolgozónév"].ToStrTrim(),
                                    rekord["Szervezetiegység"].ToStrTrim(),
                                    rekord["Munkakör"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Behajtás_Dolgozótábla Adat)
        {
            try
            {
                string szöveg = "UPDATE dolgozóktábla SET ";
                szöveg += $" Dolgozónév='{Adat.Dolgozónév}', ";
                szöveg += $" munkakör='{Adat.Munkakör}', ";
                szöveg += $" szervezetiegység='{Adat.Szervezetiegység}', ";
                szöveg += $" státus={Adat.Státus}";
                szöveg += $"  WHERE Dolgozószám='{Adat.Dolgozószám}'";
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

        public void Rögzítés(Adat_Behajtás_Dolgozótábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO dolgozóktábla ( Dolgozószám, Dolgozónév, munkakör, szervezetiegység, státus )  VALUES ( ";
                szöveg += $"'{Adat.Dolgozószám}', ";
                szöveg += $"'{Adat.Dolgozónév}', ";
                szöveg += $"'{Adat.Munkakör}', ";
                szöveg += $"'{Adat.Szervezetiegység}', ";
                szöveg += $"{Adat.Státus}) ";

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
