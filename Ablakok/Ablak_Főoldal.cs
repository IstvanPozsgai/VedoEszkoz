using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MyF = Függvénygyűjtemény;

namespace VédőEszköz
{
    public partial class Ablak_Főoldal
    {
        readonly Kezelő_Oldalak KézOldal = new Kezelő_Oldalak();
        readonly Kezelő_Jogosultságok KézJog = new Kezelő_Jogosultságok();
        readonly Kezelő_Belépés_Verzió Kéz_Belépés_Verzió = new Kezelő_Belépés_Verzió();

        List<Adat_Belépés_Verzió> AdatokVerzó = new List<Adat_Belépés_Verzió>();

        private static PerformanceCounter myCounter;

        bool CTRL_le = false;
        bool Shift_le = false;
        bool Alt_le = false;

        public Ablak_Főoldal()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// Menü sorszámából el kell venni egyet, hogy jó helyre mutasson
        /// </summary>
        private void MenüBeállítása()
        {
            try
            {
                //Kikapcsoljuk a jogosultságtól függő ablakokat
                Program.PostásOldalak = KézOldal.Lista_Adatok();
                foreach (ToolStripMenuItem item in Program.PostásMenü)
                {
                    Adat_Oldalak Adat = Program.PostásOldalak.FirstOrDefault(a => a.MenuName == item.Name);
                    if (Adat != null) item.Enabled = Adat.Látható;
                }

                //Beállítjuk a jogosultságot amit felhasználóknak adtunk
                List<Adat_Jogosultságok> JogAdatok = Program.PostásJogosultságok;
                if (JogAdatok == null) return;

                List<int> JogIDék = JogAdatok.Select(a => a.OldalId).Distinct().ToList();
                foreach (ToolStripMenuItem item in Program.PostásMenü)
                {
                    Adat_Oldalak OldalAdat = Program.PostásOldalak.FirstOrDefault(a => a.MenuName == item.Name);
                    if (OldalAdat != null)
                    {
                        if (JogIDék.Contains(OldalAdat.OldalId)) item.Enabled = true;
                    }
                }

                //Admin felhasználó menüinek láthatósága
                ablakokBeállításaToolStripMenuItem.Visible = false;
                gombokBeállításaToolStripMenuItem.Visible = false;
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
        private void A_Főoldal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void A_Főoldal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift) Shift_le = true;
            if (e.Alt) Alt_le = true;
            if (e.Control) CTRL_le = true;
        }

        private void A_Főoldal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Shift) Shift_le = false;
            if (e.Alt) Alt_le = false;
            if (e.Control) CTRL_le = false;

        }
        #region Főoldal elemek
        private void Könyvtárak_Létrehozása()
        {
            if (Program.PostásTelephely.Trim() != "")
                Hely_Ellenőr.Könyvtárszerkezet(Program.PostásTelephely.Trim());
        }
        private void Start()
        {
            Könyvtárak_Létrehozása();
            Képetvált();
            Program.PostásJogosultságok = KézJog.Lista_Adatok().Where(a => a.UserId == Program.PostásNévId).ToList();
            Program.PostásMenü = AblakokGombok.MenüListaKészítés(MenuStrip);
            Program.PostásOldalak = KézOldal.Lista_Adatok();
            MenüBeállítása();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // 5 percenként frissíti az üzeneteket, stb.
                // beállítása a tulajdonságokban  5 perc=300000 érték
                Képetvált();

                // ha látszódik a figyelmeztetés, akkor kiléptetjük
                if (Figyelmeztetés.Visible == true)
                {
                    if (!PerformanceCounterCategory.Exists("Processor"))
                    {
                        if (MessageBox.Show("Az objektumfeldolgozó nem létezik! Kilép a program.", "A program karbantartás miatt kiléptet.", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Close();
                        }
                        else
                        {
                            Close();
                        }
                    }

                    if (!PerformanceCounterCategory.CounterExists("% Processor Time", "Processor"))
                    {
                        if (MessageBox.Show("Számláló % Processzoridő nem létezik! Kilép a program.", "A program karbantartás miatt kiléptet.", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Close();
                        }
                        else
                        {
                            Close();
                        }
                    }
                    myCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

                    if ((int)Math.Round(myCounter.NextValue()) < 2)
                        Close();
                }


                // ha létezik a fájl akkor megjelenítjük a figyelmeztető üzenetet
                string hely = Application.StartupPath + @"\Adatok\a.txt";
                if (File.Exists(hely))
                {
                    // ha épp dolgozik akkor figyelmezetjük, hogy ki kell lépni
                    FigyKiírás($"Karbantartás miatt a program\n ~{DateTime.Now.AddMinutes(1):HH:mm}- kor\n kiléptet.");
                    Timer1.Enabled = false;
                    Timer1.Interval = 60000;
                    Timer1.Enabled = true;
                }

                Verziószám_kiírás();
                // Verzió váltás  akkor megjelenítjük a figyelmeztető üzenetet
                if (Convert.ToDouble(TároltVerzió.Text.Trim()) > Convert.ToDouble(Application.ProductVersion.Replace(".", "").Trim()))
                {
                    FigyKiírás($"Elavult a program verzió,\n ezért a program ki fog léptetni\n ~{DateTime.Now.AddMinutes(5d):HH:mm}- kor.");
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Meghatározatlan hiba"))
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FigyKiírás(string szöveg)
        {
            Figyelmeztetés.Left = 10;
            Figyelmeztetés.Top = 20;
            Figyelmeztetés.Width = this.Width - 20;
            Figyelmeztetés.Height = this.Height - 40;
            Figyelmeztetés.Text = szöveg;
            Figyelmeztetés.Visible = true;
        }


        #region Képkezelés
        private void Képetvált()
        {
            int választottkép = -1;
            string[] dirs = { "_" };

            try
            {
                string hely = $@"{Application.StartupPath}\Adatok\Képek".KönyvSzerk();

                dirs = Directory.GetFiles(hely, "*.jpg")
                                .Where(kép => ÉrvényesKép(kép, 500_000, 1920, 1080))
                                .ToArray();

                if (dirs.Length == 0) return;

                if (dirs.Length == 1)
                    választottkép = 0;
                else
                {
                    Random rnd = new Random();
                    választottkép = rnd.Next(dirs.Length);
                }

                string helykép = dirs[választottkép];
                Kezelő_Kép.KépMegnyitás(Képkeret, helykép, ToolTip1);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Meghatározatlan hiba"))
                {
                    HibaNapló.Log(ex.Message, this.ToString() + $"\nKép1:{dirs?[választottkép]}", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + $"\nKép1:{dirs?[választottkép]}" + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ÉrvényesKép(string helykép, long MaxMéret, int MaxSzélesség, int MaxMagasság/*, long ÖsszPixel*/)
        {
            try
            {
                // A FileInfo-val lekerdezhetjuk a fajl adatait (pl. meret, nev),
                // es fajlmuveleteket vegezhetunk rajta, mint a torles vagy datumok kezelese.

                FileInfo Flnf = new FileInfo(helykép);

                if (Flnf.Length > MaxMéret)
                {
                    //  FájlTörlés(helykép);
                    return false;
                }

                using (Image kép = Image.FromFile(helykép))
                {
                    //int ÖsszPixel = kép.Width * kép.Height;
                    if (kép.Width > MaxSzélesség || kép.Height > MaxMagasság /*|| ÖsszPixel > ÖsszPixel*/)
                    {
                        kép.Dispose();
                        //  FájlTörlés(helykép);
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                //   FájlTörlés(helykép);
                return false;
            }
        }
        #endregion

        private void Súgómenü_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\\VillamosLapok\Főoldal.html".KönyvSzerk();
                MyF.Megnyitás(hely);
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

        #endregion

        #region Beállítások menük

        Ablak_Felhasználó Új_ablak_Felhasználó;
        private void FelhasználókBeállításaMenü_Click(object sender, EventArgs e)
        {
            if (Új_ablak_Felhasználó == null)
            {
                Új_ablak_Felhasználó = new Ablak_Felhasználó();
                Új_ablak_Felhasználó.FormClosed += Új_ablak_Felhasználó_Closed;
                Új_ablak_Felhasználó.Show();
            }
            else
            {
                Új_ablak_Felhasználó.Activate();
                Új_ablak_Felhasználó.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_ablak_Felhasználó_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_Felhasználó = null;
        }
        #endregion


        #region Verzió kezelés
        private void VerzióListaFeltöltés()
        {
            try
            {
                AdatokVerzó.Clear();
                AdatokVerzó = Kéz_Belépés_Verzió.Lista_Adatok();
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

        private void Verziószám_kiírás()
        {
            VerzióListaFeltöltés();
            Adat_Belépés_Verzió Elem = (from a in AdatokVerzó
                                        where a.Id == 2
                                        select a).FirstOrDefault();
            if (Elem != null) TároltVerzió.Text = Elem.Verzió.ToString();
        }

        private void Verzió_Váltás_Click(object sender, EventArgs e)
        {
            Adat_Belépés_Verzió Elem = (from a in AdatokVerzó
                                        where a.Id == 2
                                        select a).FirstOrDefault();

            double verziószám = double.Parse(Application.ProductVersion.Replace(".", ""));

            if (Elem != null)
                Kéz_Belépés_Verzió.Módosítás(2, verziószám);
            else
                Kéz_Belépés_Verzió.Rögzítés(verziószám);

            Verziószám_kiírás();
            MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuStrip_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Rejtett.Visible = CTRL_le && Shift_le && Alt_le;
            Verziószám_kiírás();
        }
        #endregion


        #region Ablakok beállítása

        private void Ablakok_Click(object sender, EventArgs e)
        {


        }


        #endregion



        Ablak_Formok Új_Ablak_Formok;
        private void AblakokBeállításaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Új_Ablak_Formok == null)
            {
                Új_Ablak_Formok = new Ablak_Formok();
                Új_Ablak_Formok.FormClosed += Új_Ablak_Formok_FormClosed;
                Új_Ablak_Formok.Show();
            }
            else
            {
                Új_Ablak_Formok.Activate();
                Új_Ablak_Formok.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Formok_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Formok = null;
        }

        Ablak_Gombok Új_Ablak_Gombok;
        private void GombokBeállításaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Új_Ablak_Gombok == null)
            {
                Új_Ablak_Gombok = new Ablak_Gombok();
                Új_Ablak_Gombok.FormClosed += Új_Új_Ablak_Gombok_FormClosed;
                Új_Ablak_Gombok.Show();
            }
            else
            {
                Új_Ablak_Gombok.Activate();
                Új_Ablak_Gombok.WindowState = FormWindowState.Maximized;
            }

        }

        private void Új_Új_Ablak_Gombok_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Gombok = null;
        }

        Ablak_Felhasználó Új_Ablak_Felhasználó;
        private void FelhasználókLétrehozásaTörléseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Felhasználó == null)
            {
                Új_Ablak_Felhasználó = new Ablak_Felhasználó();
                Új_Ablak_Felhasználó.FormClosed += Új_Ablak_Felhasználó_FormClosed;
                Új_Ablak_Felhasználó.Show();
            }
            else
            {
                Új_Ablak_Felhasználó.Activate();
                Új_Ablak_Felhasználó.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Felhasználó_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Felhasználó = null;
        }

        Ablak_JogKiosztás Új_Ablak_JogKiosztás;
        private void JogosultságKiosztásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_JogKiosztás == null)
            {
                Új_Ablak_JogKiosztás = new Ablak_JogKiosztás();
                Új_Ablak_JogKiosztás.FormClosed += Új_Ablak_JogKiosztás_FormClosed;
                Új_Ablak_JogKiosztás.Show();
            }
            else
            {
                Új_Ablak_JogKiosztás.Activate();
                Új_Ablak_JogKiosztás.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_JogKiosztás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_JogKiosztás = null;
        }

        Ablak_Hibanaplo Új_Ablak_Hibanaplo;
        private void HibanaploMenu_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Hibanaplo == null)
            {
                Új_Ablak_Hibanaplo = new Ablak_Hibanaplo();
                Új_Ablak_Hibanaplo.FormClosed += Ablak_Hibanaplo_FormClosed;
                Új_Ablak_Hibanaplo.Show();
            }
            else
            {
                Új_Ablak_Hibanaplo.Activate();
                Új_Ablak_Hibanaplo.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Hibanaplo_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Hibanaplo = null;
        }
        //    Ablak_Védő Uj_ablak_Vedo;
        private void VedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Uj_ablak_Vedo == null)
            //{
            //    Uj_ablak_Vedo = new Ablak_Védő();
            //    Uj_ablak_Vedo.FormClosed += Uj_ablak_Vedo_Closed;
            //    Uj_ablak_Vedo.Show();
            //}
            //else
            //{
            //    Uj_ablak_Vedo.Activate();
            //    Uj_ablak_Vedo.WindowState = FormWindowState.Normal;
            //}
        }
        private void Uj_ablak_Vedo_Closed(object sender, FormClosedEventArgs e)
        {
            //       Uj_ablak_Vedo = null;
        }

        #region Dolgozó
        //Ablak_Dolgozók Új_Ablak_Dolgozók;
        private void ToolStripMenuItem_Dolgozó_Click(object sender, EventArgs e)
        {
            //if (Új_Ablak_Dolgozók == null)
            //{
            //    Új_Ablak_Dolgozók = new Ablak_Dolgozók();
            //    Új_Ablak_Dolgozók.FormClosed += Új_Ablak_Dolgozók_FormClosed;
            //    Új_Ablak_Dolgozók.Show();
            //}
            //else
            //{
            //    Új_Ablak_Dolgozók.Activate();
            //    Új_Ablak_Dolgozók.WindowState = FormWindowState.Maximized;
            //}

        }

        private void Új_Ablak_Dolgozók_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Új_Ablak_Dolgozók = null;
        }
        #endregion

        #region Szervezet
        Ablak_Szervezet Új_Ablak_Szervezet;
        private void ToolStripMenuItem_Szervezet_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Szervezet == null)
            {
                Új_Ablak_Szervezet = new Ablak_Szervezet();
                Új_Ablak_Szervezet.FormClosed += Új_Ablak_Szervezet_FormClosed;
                Új_Ablak_Szervezet.Show();
            }
            else
            {
                Új_Ablak_Szervezet.Activate();
                Új_Ablak_Szervezet.WindowState = FormWindowState.Maximized;
            }

        }

        private void Új_Ablak_Szervezet_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Szervezet = null;
        }
        #endregion

        #region Járandóság
        //Ablak_Járandóság Új_Ablak_Járandóság;
        private void ToolStripMenuItem_Járandóság_Click(object sender, EventArgs e)
        {
            //if (Új_Ablak_Járandóság == null)
            //{
            //    Új_Ablak_Járandóság = new Ablak_Járandóság();
            //    Új_Ablak_Járandóság.FormClosed += Új_Ablak_Járandóság_FormClosed;
            //    Új_Ablak_Járandóság.Show();
            //}
            //else
            //{
            //    Új_Ablak_Járandóság.Activate();
            //    Új_Ablak_Járandóság.WindowState = FormWindowState.Maximized;
            //}

        }

        private void Új_Ablak_Járandóság_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Új_Ablak_Járandóság = null;
        }

        #endregion

        private void Ablak_Főoldal_Load(object sender, EventArgs e)
        {

        }

        private void Menükinyitás_Click(object sender, EventArgs e)
        {
            ablakokBeállításaToolStripMenuItem.Visible = true;
            ablakokBeállításaToolStripMenuItem.Enabled = true;
            gombokBeállításaToolStripMenuItem.Visible = true;
            gombokBeállításaToolStripMenuItem.Enabled = true;
        }

        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}