using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


using MyE = Module_Excel;
using MyEn = Minden.Enumok;
using MyF = Függvénygyűjtemény;

namespace VédőEszköz
{
    public partial class Ablak_Anyagok : Form
    {
        readonly Kezelő_Anyag KézAnyag = new Kezelő_Anyag();
#pragma warning disable IDE0044
        DataTable AdatTáblaALap = new DataTable();
#pragma warning restore IDE0044
        List<Adat_Anyag> Adatok = new List<Adat_Anyag>();

        public Ablak_Anyagok()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            StátusokFeltöltése();
            Adatok = KézAnyag.Lista_Adatok();
            Alap_tábla_író();
            GombLathatosagKezelo.Beallit(this);
        }

        private void Ablak_Anyagok_Load(object sender, System.EventArgs e)
        {

        }

        private void Új_adat_Click(object sender, System.EventArgs e)
        {
            Cikkszámok.Text = "";
            Megnevezés.Text = "";
            MennyiségEgység.Text = "";
            Rajzszám.Text = "";
            CmbStátus.Text = "Aktív";
        }

        private void StátusokFeltöltése()
        {
            try
            {
                foreach (string adat in Enum.GetNames(typeof(MyEn.Státus)))
                    CmbStátus.Items.Add(adat);
                CmbStátus.Text = "Aktív";
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

        private void Alap_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (MyF.Szöveg_Tisztítás(Cikkszámok.Text, 0, 10).Trim() == "") throw new HibásBevittAdat("Cikkszám mezőt ki kell tölteni.");
                if (MyF.Szöveg_Tisztítás(Megnevezés.Text, 0, 250).Trim() == "") throw new HibásBevittAdat("Megnevezés mezőt ki kell tölteni.");
                if (MyF.Szöveg_Tisztítás(MennyiségEgység.Text, 0, 10).Trim() == "") throw new HibásBevittAdat("Mennyiség egység mezőt ki kell tölteni.");
                if (CmbStátus.Text != "Aktív" && CmbStátus.Text != "Törölt") throw new HibásBevittAdat("Státus mezőben csak Aktív/Törölt értékeket vehetnek fel.");
                Adat_Anyag ADAT = new Adat_Anyag(
                       MyF.Szöveg_Tisztítás(Cikkszámok.Text),
                       MyF.Szöveg_Tisztítás(Megnevezés.Text),
                       MyF.Szöveg_Tisztítás(Rajzszám.Text),
                       MyF.Szöveg_Tisztítás(MennyiségEgység.Text),
                       CmbStátus.Text != "Aktív");
                KézAnyag.Döntés(ADAT);
                Adatok = KézAnyag.Lista_Adatok();
                Alap_tábla_író();
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

        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            Alap_tábla_író();
        }

        private void Alap_tábla_író()
        {
            try
            {
                Adatok = KézAnyag.Lista_Adatok();
                Tábla.Visible = false;
                Tábla.CleanFilterAndSort();
                AlapTáblaFejléc();
                AlapTáblaTartalom();
                Tábla.DataSource = AdatTáblaALap;
                AlapTáblaOszlopSzélesség();
                Tábla.Visible = true;
                Tábla.Refresh();
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

        private void AlapTáblaTartalom()
        {
            AdatTáblaALap.Clear();
            List<Adat_Anyag> AdatokSzűrt = Adatok;
            AdatokSzűrt = AdatokSzűrt.Where(x => x.Státus == (CmbStátus.Text.Trim() == "Törölt")).ToList();

            foreach (Adat_Anyag rekord in AdatokSzűrt)
            {
                DataRow Soradat = AdatTáblaALap.NewRow();

                Soradat["Cikkszám"] = rekord.Cikkszám;
                Soradat["Megnevezés"] = rekord.Megnevezés;
                Soradat["Rajzszám"] = rekord.Rajzszám;
                Soradat["MennyiségEgység"] = rekord.MennyiségEgység;
                Soradat["Státus"] = rekord.Státus == true ? "Törölt" : "Aktív";
                AdatTáblaALap.Rows.Add(Soradat);
            }


        }

        private void AlapTáblaFejléc()
        {
            try
            {
                AdatTáblaALap.Columns.Clear();
                AdatTáblaALap.Columns.Add("Cikkszám");
                AdatTáblaALap.Columns.Add("Megnevezés");
                AdatTáblaALap.Columns.Add("Rajzszám");
                AdatTáblaALap.Columns.Add("MennyiségEgység");
                AdatTáblaALap.Columns.Add("Státus");
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

        private void AlapTáblaOszlopSzélesség()
        {
            Tábla.Columns["Cikkszám"].Width = 130;
            Tábla.Columns["Megnevezés"].Width = 500;
            Tábla.Columns["Rajzszám"].Width = 200;
            Tábla.Columns["MennyiségEgység"].Width = 150;
            Tábla.Columns["Státus"].Width = 70;

        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Anyag_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length);
                MyE.DataGridViewToExcel(fájlexc, Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc);
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string Cikkszám = Tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            Adatokkiírása(Cikkszám);

        }

        private void Adatokkiírása(string Cikkszám)
        {
            try
            {
                Adat_Anyag adat = (from a in Adatok
                                   where a.Cikkszám == Cikkszám
                                   select a).FirstOrDefault();
                if (adat == null) return;
                Cikkszámok.Text = adat.Cikkszám;
                Megnevezés.Text = adat.Megnevezés;
                MennyiségEgység.Text = adat.MennyiségEgység;
                Rajzszám.Text = adat.Rajzszám;
                CmbStátus.Text = adat.Státus == true ? "Törölt" : "Aktív";

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

        private void Ablak_Anyagok_Load_1(object sender, EventArgs e)
        {

        }
    }
}
