using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vedo.Egyeb;
using Vedo.Kezelők;
using Vedo.Vedo_Adat;

using MyE = Vedo.Module_Excel;

namespace Vedo.Ablakok
{
    public partial class Ablak_Formok : Form
    {
        readonly Kezelő_Oldalok Kéz = new Kezelő_Oldalok();
#pragma warning disable IDE0044
        DataTable AdatTáblaALap = new DataTable();
#pragma warning restore IDE0044
        List<Adat_Oldalak> Adatok = new List<Adat_Oldalak>();

        public Ablak_Formok()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            MenükFeltöltése();
            FormFeltöltése();
            Adatok = Kéz.Lista_Adatok();
            Alap_tábla_író();
       //     GombLathatosagKezelo.Beallit(this);
        }

        private void Ablak_Anyagok_Load(object sender, System.EventArgs e)
        {

        }


        /// <summary>
        /// Előkészítjük a beviteli mezőket az új adat fogadására
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Új_adat_Click(object sender, System.EventArgs e)
        {
            TxtId.Text = "";
            Ablaknév.Text = "";
            MenüNév.Text = "";
            MenüFelirat.Text = "";
            Láthatóság.Checked = false;
            Törölt.Checked = false;
        }

        /// <summary>
        /// Főoldalon lévő menük feltöltése a comboboxba
        /// </summary>
        private void MenükFeltöltése()
        {
            try
            {
                MenüNév.Items.Add("");
                MenüFelirat.Items.Add("");
                foreach (ToolStripMenuItem item in Program.PostásMenü)
                {
                    MenüNév.Items.Add(item.Name);
                    MenüFelirat.Items.Add(item.Text);
                }
                MenüNév.Text = "";
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
        /// Feltöltjük a formok listáját a comboboxba
        /// </summary>
        private void FormFeltöltése()
        {
            try
            {
                Ablaknév.Items.Add("");
                List<Type> Adatok = AblakokGombok.FormokListázásaType();
                foreach (Type item in Adatok)
                {
                    Ablaknév.Items.Add(item.Name);
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

        /// <summary>
        /// Rögzítjük az új adatokat a táblába
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alap_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(MenüNév.Text.Trim())) throw new HibásBevittAdat("Kérem adja meg a Menü nevét!");

                Adat_Oldalak adat = new Adat_Oldalak(
                        TxtId.Text.ToÉrt_Int(),
                        Ablaknév.Text.ToStrTrim(),
                        MenüNév.Text.ToStrTrim(),
                        MenüFelirat.Text.ToStrTrim(),
                        Láthatóság.Checked,
                        Törölt.Checked);

                Kéz.Döntés(adat);
                Adatok = Kéz.Lista_Adatok();
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

        /// <summary>
        /// Frissíti a táblázat adatait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            Alap_tábla_író();
        }

        /// <summary>
        /// A táblázat adatait írja ki a DataGridView-ba
        /// </summary>
        private void Alap_tábla_író()
        {
            try
            {
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

        /// <summary>
        /// Az adatok kiírása a táblázatba
        /// </summary>
        private void AlapTáblaTartalom()
        {
            AdatTáblaALap.Clear();
            foreach (Adat_Oldalak rekord in Adatok)
            {
                DataRow Soradat = AdatTáblaALap.NewRow();

                Soradat["Oldal Id"] = rekord.OldalId;
                Soradat["Form Név"] = rekord.FromName;
                Soradat["Menü Név"] = rekord.MenuName;
                Soradat["Menü Felirat"] = rekord.MenuFelirat;
                Soradat["Látható"] = rekord.Látható ? "Igen" : "Nem";
                Soradat["Törölt"] = rekord.Törölt ? "Igen" : "Nem";
                AdatTáblaALap.Rows.Add(Soradat);
            }
        }

        /// <summary>
        /// Fejléc beállítása a táblázatnak
        /// </summary>
        private void AlapTáblaFejléc()
        {
            try
            {
                AdatTáblaALap.Columns.Clear();
                AdatTáblaALap.Columns.Add("Oldal Id");
                AdatTáblaALap.Columns.Add("Menü Név");
                AdatTáblaALap.Columns.Add("Menü Felirat");
                AdatTáblaALap.Columns.Add("Form Név");
                AdatTáblaALap.Columns.Add("Látható");
                AdatTáblaALap.Columns.Add("Törölt");
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
        /// Oldalszélesség beállítása a táblázatnak
        /// </summary>
        private void AlapTáblaOszlopSzélesség()
        {
            Tábla.Columns["Oldal Id"].Width = 130;
            Tábla.Columns["Form Név"].Width = 400;
            Tábla.Columns["Menü Név"].Width = 400;
            Tábla.Columns["Menü Felirat"].Width = 450;
            Tábla.Columns["Látható"].Width = 100;
            Tábla.Columns["Törölt"].Width = 100;

        }

        /// <summary>
        /// Táblázatot kimenti Excelbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    FileName = "Menük_és_Ablakok_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length);
                MyE.DataGridViewToExcel(fájlexc, Tábla, true);
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

        /// <summary>
        /// Kiírja a kiválasztott sor adatait a beviteli mezőkbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int ID = Tábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int();
            Adatokkiírása(ID);
        }

        /// <summary>
        /// Kiválasztott adat listázása a beviteli mezőbe
        /// </summary>
        /// <param name="ID"></param>
        private void Adatokkiírása(int ID)
        {
            try
            {
                Adat_Oldalak adat = (from a in Adatok
                                     where a.OldalId == ID
                                     select a).FirstOrDefault();
                if (adat == null) return;
                TxtId.Text = adat.OldalId.ToString();
                MenüFelirat.Text = adat.MenuFelirat;
                MenüNév.Text = adat.MenuName;
                Ablaknév.Text = adat.FromName;
                Láthatóság.Checked = adat.Látható;
                Törölt.Checked = adat.Törölt;
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
        /// Ha választjuk a menü nevét utánna kiírja a nevét
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenüFelirat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (MenüFelirat.SelectedIndex < 1) return;
            MenüFelirat.Text = MenüFelirat.Items[MenüFelirat.SelectedIndex].ToString();
            ToolStripMenuItem item = Program.PostásMenü.Where(x => x.Text == MenüFelirat.Text).FirstOrDefault();
            if (item != null)
            {
                MenüNév.Text = item.Name;
            }
        }
    }
}
