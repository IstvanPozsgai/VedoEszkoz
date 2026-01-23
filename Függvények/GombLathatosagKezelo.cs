
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VédõEszköz
{

    public static class GombLathatosagKezelo
    {
        /// <summary>
        /// Beállítja az ablakon található gombok láthatóságát az adatbázis alapján.
        /// </summary>
        /// <param name="form">Az ablak (Form), amin a gombok találhatók.</param>
        public static void Beallit(Form form, string Telephely = "")
        {
            // Lekérjük az adott ablakhoz tartozó gombokat az adatbázisból
            List<Adat_Gombok> gombok = Program.PostásGombok.Where(g => g.FromName == form.Name && !g.Törölt).ToList();

            GombokÁltalános(form, gombok);
            GombokSzemélyes(form, gombok, Telephely);
        }

        /// <summary>
        /// Kikapcsoljuk az ablakon található gombokat, ha a gombok adatbázisban megtalálhatóak, így nem lesznek láthatóak.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="gombok"></param>
        private static void GombokÁltalános(Form form, List<Adat_Gombok> gombok)
        {
            try
            {
                foreach (Adat_Gombok adatGomb in gombok)
                {
                    // Megkeressük a gombot az ablak Controls gyûjteményében
                    Control control = form.Controls.Find(adatGomb.GombName, true).FirstOrDefault();
                    if (control is Button button)
                    {
                        button.Visible = adatGomb.Látható;
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "GombokÁltalános", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Visszakapcsoljuk a gombokat, ha a felhasználónak van jogosultsága az adott gombhoz, az adott telephelyen.
        /// </summary>
        /// <param name="form"></param>
        /// <param name=""></param>
        /// <param name="gombok"></param>
        /// <param name="Telephely"></param>
        private static void GombokSzemélyes(Form form, List<Adat_Gombok> gombok, string Telephely = "")
        {
            try
            {
                int TelephelyID = 0;
                Adat_Kiegészítõ_Könyvtár TelephelyAdat = (from a in Program.PostásKönyvtár
                                                          where a.Név == Telephely
                                                          select a).FirstOrDefault();
                if (TelephelyAdat != null) TelephelyID = TelephelyAdat.ID;

                // Lekérjük az aktuális oldal ID-ját
                Adat_Oldalak AdatOldal = Program.PostásOldalak.Where(o => o.FromName == form.Name).FirstOrDefault();
                if (AdatOldal == null) return;

                // Lekérjük az adott felhasználóhoz tartozó gombokat az adatbázisból
                List<Adat_Jogosultságok> jogosultságok = Program.PostásJogosultságok;
                jogosultságok = (from j in jogosultságok
                                 where j.UserId == Program.PostásNévId
                                 && !j.Törölt
                                 && j.SzervezetId == TelephelyID
                                 && j.OldalId == AdatOldal.OldalId
                                 select j).ToList();

                // ha a jogosultáság táblában van akkor van hozzá joga így láthatóvá tesszük a gombokat
                foreach (Adat_Jogosultságok adatGomb in jogosultságok)
                {
                    Adat_Gombok Egygomb = (from a in gombok
                                           where a.GombokId == adatGomb.GombokId
                                           select a).FirstOrDefault();
                    if (Egygomb != null)
                    {
                        // Megkeressük a gombot az ablak Controls gyûjteményében
                        Control control = form.Controls.Find(Egygomb.GombName, true).FirstOrDefault();
                        if (control is Button button) button.Visible = true;
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "GombokSzemélyes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<string> Telephelyek(string AblakNév)
        {
            List<string> Válasz = new List<string>();
            try
            {
                //Mi az oldal Id-je
                Adat_Oldalak Oldal = (from a in Program.PostásOldalak
                                      where a.FromName == AblakNév
                                      select a).FirstOrDefault();
                int OldalId = 0;
                if (Oldal != null) OldalId = Oldal.OldalId;
                //Azok a jogosultságok amik az adott oldalhoz tartoznak
                List<Adat_Jogosultságok> AdatokA = (from a in Program.PostásJogosultságok
                                                    where a.OldalId == OldalId
                                                    select a).ToList();

                List<Adat_Kiegészítõ_Könyvtár> Ideig = new List<Adat_Kiegészítõ_Könyvtár>();
                foreach (Adat_Jogosultságok Adat in AdatokA)
                {
                    Adat_Kiegészítõ_Könyvtár AdatKönyv = (from a in Program.PostásKönyvtár
                                                          where a.ID == Adat.SzervezetId
                                                          select a).FirstOrDefault();
                    Ideig.Add(AdatKönyv);
                }

                if (Ideig.Count > 0) Válasz = Ideig.OrderBy(a => a.Név).Select(a => a.Név).Distinct().ToList();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "GombLathatosagKezelo - Telephelyek Listázása", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

    }
}