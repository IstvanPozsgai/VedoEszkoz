using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VédőEszköz
{
    public static class Hely_Ellenőr
    {
        public static string KönyvSzerk(this string fájl)
        {
            string Válasz = fájl;
            try
            {
                if (File.Exists(fájl)) return Válasz;
                string[] Könyvtár = fájl.Split('\\');
                string alap = Könyvtár[0];
                for (int i = 1; i < Könyvtár.Length; i++)
                {
                    if (!Könyvtár[i].Contains(".mdb"))
                    {
                        alap += $@"\{Könyvtár[i]}";
                        if (!Directory.Exists(alap)) Directory.CreateDirectory(alap);
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Hely_Ellenőr : Ellenőrzés :{fájl}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }


        /// <summary>
        /// Feltölti a Postás_Telephelyek listát, mely majd a könyvtár ellenőrzéshez kell
        /// </summary>
        /// <param name="hely"></param>
        /// <returns></returns>
        private static void TelephelyekFelöltése()
        {
            try
            {
                Kezelő_Kiegészítő_Könyvtár Kéz = new Kezelő_Kiegészítő_Könyvtár();
                Program.Postás_Telephelyek = Kéz.Lista_Adatok().OrderBy(a => a.Név).Select(a => a.Név).ToList();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TelephelyekFelöltése", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Könyvtárszerkezet(string Telephely)
        {
            try
            {
                string hely;
                if (Telephely == "Főmérnökség")
                {
                    //Minden könyvtár
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Üzenetek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Képek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Napló".KönyvSzerk();
                    //Főmérnökség
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Hibanapló".KönyvSzerk();
                }
                if (!(Telephely == "" || Telephely == "Főmérnökség"))
                {
                    //Minden könyvtár
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Üzenetek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Képek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Napló".KönyvSzerk();
                    //telephelyi
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv\Futás".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv\{DateTime.Today.Year}\Nap".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv\{DateTime.Today.Year}\Zser".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Beosztás".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Naplózás".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Villamos".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Segéd".KönyvSzerk();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Hely_Ellenőr : Könyvtárszerkezet ", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
