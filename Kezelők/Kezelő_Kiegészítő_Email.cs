using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

using MyA = Adatbázis;

namespace VédőEszköz
{
    public class Kezelő_Kiegészítő_Email
    {
        readonly string jelszó = "Mocó";
        readonly string hely = $@"{Application.StartupPath}\adatok\Kiegészítő2.mdb";
        string táblanév = "hibanaplo_email";

        // Statikusan tárolom, hogy csak egyszer kelljen betölteni a címeket.
        public static string ÖsszesEmailCím { get; set; } = string.Empty;

        public string Email_Cimek(bool forceReload = false)
        {
            if (!string.IsNullOrEmpty(ÖsszesEmailCím) && !forceReload)
                return ÖsszesEmailCím;

            List<string> adatok = new List<string>();
            string kapcsolatSzöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}';Jet OLEDB:Database Password={jelszó}";
            string lekérdezés = $"SELECT cim FROM {táblanév}";

            try
            {
                using (OleDbConnection kapcsolat = new OleDbConnection(kapcsolatSzöveg))
                {
                    kapcsolat.Open();
                    using (OleDbCommand parancs = new OleDbCommand(lekérdezés, kapcsolat))
                    using (OleDbDataReader rekord = parancs.ExecuteReader())
                    {
                        while (rekord.Read())
                        {
                            if (rekord["cim"] != DBNull.Value)
                                adatok.Add(rekord["cim"].ToString());
                        }
                    }
                }
                ÖsszesEmailCím = string.Join(";", adatok.Distinct());
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Hiba az e-mail címek beolvasásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ÖsszesEmailCím;
        }

        public void Rögzítés(string cim)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (cim) VALUES ('{cim}')";
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

        public void Törlés(string cim)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE cim='{cim}'";
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

        public void Módosít(string régiCim, string újCim)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET cim='{újCim}' WHERE cim='{régiCim}'";
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

