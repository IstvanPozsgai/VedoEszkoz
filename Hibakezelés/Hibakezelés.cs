using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MyF = Függvénygyűjtemény;
using MyO = Microsoft.Office.Interop.Outlook;

namespace VédőEszköz
{
    class HibásBevittAdat : Exception
    {
        //üres konstruktor
        public HibásBevittAdat() { }

        //string paraméter befogadása
        public HibásBevittAdat(string message) : base(message) { }
        //innerExteption
        public HibásBevittAdat(string message, Exception innerException)
        : base(message, innerException) { }
    }

    public static class HibaNapló
    {
        /// <summary>
        /// Hiba 
        /// </summary>
        /// <param name="hibaUzenet">ex.Message</param>
        /// <param name="osztaly">this.ToString()</param>
        /// <param name="metodus">ex.StackTrace</param>
        /// <param name="névtér">ex.Source</param>

        public static void Log(string hibaUzenet, string osztaly, string metodus, string névtér, int HibaKód, string Egyéb = "_", bool Kell = true)
        {
            if (!Kell) return;
            string Képernyőfájl = KépernyőKép();

            //Beírjuk a napi fájlba
            string szöveg = "\n=======================================================================\n";
            szöveg += $"Idő: {DateTime.Now:yyyy.MM.dd HH.mm.ss}\n";
            szöveg += $"Telephely: {Program.PostásTelephely}\n";
            szöveg += $"Felhasználó: {Program.PostásNév}\n";
            szöveg += $"hibaUzenet: {hibaUzenet}\n\n";
            szöveg += $"osztaly: {osztaly}\n";
            szöveg += $"metodus: {metodus}\n";
            szöveg += $"névtér: {névtér}\n";
            szöveg += $"Egyéb: {Egyéb}\n";
            szöveg += $"Hibakód: {HibaKód}\n";
            szöveg += " --\n";

            // E-mail küldés
            //Email(Képernyőfájl, szöveg, HibaKód);
            // JAVÍTANDÓ:Kell a napi fájl?
            string hely = $@"{Application.StartupPath}\Adatok\Hibanapló\{DateTime.Today:yyyy}".KönyvSzerk();
            hely += $@"\Hiba{DateTime.Today:yyyyMMdd}.log";
            File.AppendAllText(hely, szöveg);

            // beírjuk a csv fájlba
            hely = $@"{Application.StartupPath}\Adatok\Hibanapló\{DateTime.Today:yyyy}\hiba{DateTime.Today:yyyy}.csv";
            string szöveg2;
            if (!File.Exists(hely))
            {
                //fejléc 
                szöveg2 = "Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum\n";
                File.AppendAllText(hely, szöveg2, System.Text.Encoding.GetEncoding("iso-8859-2"));
            }
            szöveg2 = DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ";"
                        + Program.PostásTelephely + ";"
                        + Program.PostásNév + ";"
                        + MyF.Szöveg_Tisztítás(hibaUzenet, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(osztaly, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(metodus, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(névtér, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(Egyéb, 0, -1, true) + ";"
                        + DateTime.Today.ToString("yyyy.MM.dd") + "\n";
            File.AppendAllText(hely, szöveg2, Encoding.GetEncoding("iso-8859-2"));


            if (KiléptetőVizsgálat(szöveg))
            {
                //Buborék
                NotifyIcon BuborékAblak = new NotifyIcon
                {
                    Icon = SystemIcons.Error,
                    BalloonTipTitle = "Hálózati hiba?",
                    BalloonTipText = "A védő program hálózati hiba miatt leáll.",
                    BalloonTipIcon = ToolTipIcon.Info,
                    Visible = true
                };
                BuborékAblak.ShowBalloonTip(30000);
                Application.Exit();
            }
            else
            {
                //Buborék
                NotifyIcon BuborékAblak = new NotifyIcon
                {
                    Icon = SystemIcons.Error,
                    BalloonTipTitle = "Programhiba",
                    BalloonTipText = "A hiba képernyőképpel el lett küldve a pozsgaii@bkv.hu címre.",
                    BalloonTipIcon = ToolTipIcon.Info,
                    Visible = true
                };
                BuborékAblak.ShowBalloonTip(30000);
            }
        }

        private static string KépernyőKép()
        {
            string Válasz = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Hiba_{DateTime.Now:yyyyMMddHHmmss}.bmp";
            string selection = "Védő_C#";
            Image img = ScreenshotHelper.GetBitmapScreenshot(selection);
            img?.Save(Válasz, ImageFormat.Jpeg);

            return Válasz;
        }

        private static void Email(string hely, string hiba, int hibakod)
        {
            if (EmailVizsgál(hiba))
            {

                MyO._Application _app = new MyO.Application();
                MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);
                // címzett
                mail.To = $"{Kezelő_Kiegészítő_Email.ÖsszesEmailCím}";
                // üzenet tárgya
                mail.Subject = $"Hibanapló {DateTime.Now:yyyyMMddHHmmss}";
                mail.Body = hiba;
                mail.Importance = MyO.OlImportance.olImportanceNormal;
                if (File.Exists(hely)) mail.Attachments.Add(hely);
                ((MyO._MailItem)mail).Send();
            }
        }

        private static bool EmailVizsgál(string hiba)
        {
            bool Válasz = true;
            if (hiba.Contains("800AC472")) Válasz = false;
            if (hiba.Contains("Nem található a következő elérési út egy része")) Válasz = false;
            if (hiba.Contains("A folyamat nem éri el a következő fájlt, mert azt egy másik folyamat használja")) Válasz = false;

            return Válasz;
        }

        private static bool KiléptetőVizsgálat(string hiba)
        {
            bool Válasz = false;
            if (hiba.Contains("Nem található a következő elérési út egy része")) Válasz = true;
            return Válasz;
        }
    }
}
