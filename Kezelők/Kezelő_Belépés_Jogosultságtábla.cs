using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

using MyA = Adatbázis;

namespace VédőEszköz
{
    public class Kezelő_Belépés_Jogosultságtábla
    {
        readonly string jelszó = "forgalmiutasítás";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Belépés.mdb";
            //Nincs kidolgozva
            //if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Belépés_Jogosultságtábla> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM Jogosultságtábla order by név";
            List<Adat_Belépés_Jogosultságtábla> Adatok = new List<Adat_Belépés_Jogosultságtábla>();
            Adat_Belépés_Jogosultságtábla Adat;

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
                                Adat = new Adat_Belépés_Jogosultságtábla(
                                    rekord["név"].ToStrTrim(),
                                    rekord["jogkörúj1"].ToStrTrim(),
                                    rekord["jogkörÚj2"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;

        }

        public void Rögzítés(string Telephely, Adat_Belépés_Jogosultságtábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO Jogosultságtábla (név, Jogkörúj1, Jogkörúj2) ";
                szöveg += $" Values('{Adat.Név}', ";
                szöveg += $"'{Adat.Jogkörúj1}', ";
                szöveg += $"'{Adat.Jogkörúj2}' )";
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

        public void Módosítás(string Telephely, Adat_Belépés_Jogosultságtábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"Update Jogosultságtábla set ";
                szöveg += $"jogkörúj1='{Adat.Jogkörúj1}' ";
                szöveg += $"WHERE név= '{Adat.Név}'";
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

        public void Törlés(string Telephely, Adat_Belépés_Jogosultságtábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE * From Jogosultságtábla where név='{Adat.Név}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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
