using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;



using static System.IO.File;
using MyF = Függvénygyűjtemény;

namespace VédőEszköz
{
    public partial class AblakBejelentkezés
    {
        readonly Kezelő_Users Kéz = new Kezelő_Users();
        readonly Kezelő_Belépés_Verzió KézVerzió = new Kezelő_Belépés_Verzió();
        readonly Kezelő_Gombok KézGombok = new Kezelő_Gombok();
        readonly Kezelő_Oldalok KézOldal = new Kezelő_Oldalok();
        readonly Kezelő_Kiegészítő_Könyvtár KézKönyvtár = new Kezelő_Kiegészítő_Könyvtár();

        List<Adat_Users> Adatok = new List<Adat_Users>();


        bool Beléphet = true;

        #region Alap
        public AblakBejelentkezés()
        {
            SQLitePCL.Batteries.Init();
            InitializeComponent();
            Start();
        }

        private void AblakBejelentkezés_Load(object sender, EventArgs e)
        {
            CmbUserName.Focus();
            AcceptButton = BtnBelépés;
        }

        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VedoLapok\Főoldal.html";
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

        private void Start()
        {
            lblVerzió.Text = "Verzió: " + Application.ProductVersion;
            lblProgramnév.Text = Application.ProductName;
            Timer_kilép.Enabled = false;
            Hálózat();
            Dátumformátumellenőrzés();
            Karbantartásellenőrzés();
            FelhasználókLista();
            FelhasználókFeltöltése();
            if (Beléphet) WinVan();

        }

        private void FelhasználókLista()
        {
            Adatok = Kéz.Lista_Adatok();
            Adatok = Adatok.Where(a => a.Törölt == false).ToList();
        }

        private void FelhasználókFeltöltése()
        {
            try
            {
                CmbUserName.Items.Clear();


                foreach (Adat_Users Adat in Adatok)
                {
                    CmbUserName.Items.Add(Adat.UserName.ToUpper());
                }

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

        private void Hálózat()
        {
            if (Application.StartupPath.Substring(0, 2) == @"\\")
            {
                FigyKiírás("A programot csak hálózati meghajtón keresztül lehet elindítani. \n Kérem csatlakoztasson hálózati meghajtót.");
                KönyvtárEllenőr KV = new KönyvtárEllenőr();
                KV.Megfelelő(Application.StartupPath);
                Beléphet = false;
            }
        }

        private void Dátumformátumellenőrzés()
        {
            try
            {
                string formátum = @"^([12]\d{3}.(0[1-9]|1[0-2]).(0[1-9]|[12]\d|3[01]). ([0-2][0-9]|[0-9]):([0-5][0-9]):([0-5][0-9]))$";

                if (!Regex.IsMatch(DateTime.Now.ToString(), formátum))
                {
                    // Ha rossz a dátum formátum, akkor átállítjuk
                    string dateFormat = "yyyy.MM.dd.";
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                    key.SetValue("sShortDate", dateFormat);

                    //Ha kijavítottuk, akkor és tájékozatunk
                    string szöveg = "A számítógépen az idő formátum nem volt jól beállíva, ezért a program átírta a rendszer időformátumát. \n A helyes működéshez a program újraindul.";
                    MessageBox.Show(szöveg, "Számítógép beállítási Hiba javítva lett", MessageBoxButtons.OK);
                    Application.Restart();

                    if (!Regex.IsMatch(DateTime.Now.ToString(), formátum))
                    {
                        //Ha továbbra is rossz a dátum formátum akkor kilépünk üzenettel
                        szöveg = "A számítógépen az idő formátum nincs jól beállíva, ezért a program leáll.\r\n";
                        szöveg += " A helyes formátum: éééé.HH.nn. nincs szóköz a pont után.\r\n";
                        szöveg += " Beállítani a területi/ régió beállításoknál kell.";
                        FigyKiírás(szöveg);
                        //     HibaNapló.Log("Hibás  dátum forma.", $"Dátum: {DateTime.Now}\n Felhasználó : {Environment.UserName}", "Dátumformátumellenőrzés", "", 0);
                        Timer_kilép.Enabled = true;

                        Application.Exit();
                    }
                    else
                    {
                        //Ha kijavítottuk, akkor és tájékozatunk
                        szöveg = "A számítógépen az idő formátum nem volt jól beállíva, ezért a program átírta a rendszer időformátumát.";
                        MessageBox.Show(szöveg, "Számítógép beállítási Hiba javítva lett", MessageBoxButtons.OK);
                    }
                }
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

        private void Karbantartásellenőrzés()
        {
            // ha karbantartás van nem engedünk belépni
            string hely = $@"{Application.StartupPath}\Adatok\Szerszám\a.txt";
            if (Exists(hely))
            {
                FigyKiírás("\nJelenleg az adatok karbantartása folyik. \r\n\n Kis türelmet kérek ....");
                Beléphet = false;
            }
            else
                Verzióellenőrzés();
        }
        // JAVÍTANDÓ:
        private void WinVan()
        {
            try
            {
                //List<Adat_Belépés_WinTábla> Adatok = KézWin.Lista_Adatok();

                //if (Adatok != null)
                //{
                //    Adat_Belépés_WinTábla Elem = (from a in Adatok
                //                                  where a.WinUser.ToUpper() == Environment.UserName.ToUpper()
                //                                  select a).FirstOrDefault();

                //    //Ha van ilyen dolgozó, akkor beléptetjük
                //    if (Elem != null)
                //    {
                //        AdatokBelépésTelephely = Kéz_Bejelentkezés.Lista_Adatok(Elem.Telephely);
                //        Adat_Belépés_Bejelentkezés Kiaz = (from a in AdatokBelépésTelephely
                //                                           where a.Név.ToUpper() == Elem.Név.ToUpper()
                //                                           select a).FirstOrDefault();

                //        Belépés(Elem.Telephely, Elem.Név.ToUpper(), Kiaz.Jelszó.ToUpper());
                //    }
                //}
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

        private void Verzióellenőrzés()
        {
            try
            {
                List<Adat_Belépés_Verzió> Adatok = KézVerzió.Lista_Adatok();

                Adat_Belépés_Verzió Elem = (from a in Adatok
                                            where a.Id == 2
                                            select a).FirstOrDefault();
                if (Elem != null)
                {
                    if (Application.ProductVersion.Replace(".", "").ToÉrt_Double() < Elem.Verzió)
                    {
                        FigyKiírás("Elavult a program verzió!\n\n Új parancsikont kell készíteni.\n\n Ha szükséges a számítógépet újra kell indítani.");
                        Beléphet = false;
                    }
                }
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

        private void FigyKiírás(string szöveg)
        {

            Timer_kilép.Enabled = true;

            Label Figyelmeztetés = new Label
            {
                Left = 5,
                Top = 5,
                Width = 555,
                Height = 275,
                Text = szöveg,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Tomato,
                Font = new System.Drawing.Font("Arial", 20, FontStyle.Bold),
                Visible = true
            };
            this.Controls.Add(Figyelmeztetés);

            Timer_kilép.Enabled = true;
            Application.Exit();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // kilépünk
            Application.Exit();
        }
        #endregion


        #region Beléptetés
        private void Btnlekérdezés_Click(object sender, EventArgs e)
        {
            Adat_Users Belép = (from a in Adatok
                                where a.UserName.ToUpper() == "VENDÉG"
                                && a.Törölt == false
                                select a).FirstOrDefault() ?? throw new HibásBevittAdat("Hibás felhasználónév.");
            Belépés(Belép);
        }

        public void BtnBelépés_Click(object sender, EventArgs e)
        {
            Belépés(CmbUserName.Text.Trim(), Jelszó.HashPassword(TxtPassword.Text.Trim()));
        }

        private void Belépés(string UserName, string Begépeltjelszó)
        {
            try
            {
                Adat_Users Belép = (from a in Adatok
                                    where a.UserName.ToUpper() == UserName.ToUpper().Trim()
                                    && a.Törölt == false
                                    select a).FirstOrDefault() ?? throw new HibásBevittAdat("Hibás felhasználónév.");

                if (Begépeltjelszó != Belép.Password) throw new HibásBevittAdat("Hibás jelszó!");
                if (Belép.Frissít)
                {
                    MessageBox.Show("A jelszó még nem lett módosítva az alapbeállításról, a jelszó módosítani szükséges.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //kérjük, hogy változtassa meg a jelszavát
                    Subjelszómódosítás(Belép);
                    Adatok = Kéz.Lista_Adatok();
                    return;
                }
                if (Jelszó.HashPassword(TxtPassword.Text.Trim()) != Belép.Password) throw new HibásBevittAdat("Hibás jelszó!");
                Belépés(Belép);
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

        /// <summary>
        /// Beállítjuk , hogy ki lép be, milyen joggal és a programba és beléptetjük
        /// </summary>
        private void Belépés(Adat_Users Elem)
        {
            //Program.PostásJogkör majd törölni kell
            Program.PostásJogkör = "R0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                                   "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                                   "000000000000000000000000000000000000000000000000000000000000";
            Program.PostásNév = Elem.UserName;
            Program.PostásNévId = Elem.UserId;
            Program.PostásTelephely = Elem.Szervezet;
            Program.PostásGombok = KézGombok.Lista_Adatok();
            Program.PostásOldalak = KézOldal.Lista_Adatok();
            Program.PostásKönyvtár = KézKönyvtár.Lista_Adatok();
            Program.Postás_Felhasználó = Elem;
            //Valamint, hogy mire van jogosultsága
            Ablak_Főoldal Főoldalablak = new Ablak_Főoldal();
            Főoldalablak.Show();
            this.Hide();
        }

        private void Subjelszómódosítás(Adat_Users Adat)
        {
            Ablak_Jelszó_Változtatás jelszó_váltás = new Ablak_Jelszó_Változtatás(Adat);
            jelszó_váltás.Változás += FelhasználókLista;
            jelszó_váltás.ShowDialog();

            TxtPassword.Text = "";
            TxtPassword.Focus();
        }

        private void BtnMégse_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbUserName.Text.Trim() != "")
            {
                TxtPassword.Text = "";
                TxtPassword.Focus();
            }
        }
        #endregion


        #region Jelszó Módosítás
        private void BtnJelszóMódosítás_Click(object sender, EventArgs e)
        {
            if (CmbUserName.Text.Trim() == "") return;
            TxtPassword.Text = "";
            Adat_Users Belép = (from a in Adatok
                                where a.UserName.ToUpper() == CmbUserName.Text.ToUpper().Trim()
                                && a.Törölt == false
                                select a).FirstOrDefault() ?? throw new HibásBevittAdat("Hibás felhasználónév.");

            Subjelszómódosítás(Belép);
        }
        #endregion
    }
}