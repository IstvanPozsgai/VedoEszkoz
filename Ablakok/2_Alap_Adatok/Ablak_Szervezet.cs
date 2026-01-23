using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vedo.Kezelők;
using Vedo.Vedo_Adat;
using MyE = Vedo.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Vedo.Ablakok
{
    public partial class Ablak_Szervezet : Form
    {
        readonly Kezelő_Szervezet KézSzervezet = new Kezelő_Szervezet();
#pragma warning disable IDE0044
        DataTable AdatTáblaALap = new DataTable();
#pragma warning restore IDE0044
        List<Adat_Szervezet> Adatok = new List<Adat_Szervezet>();

        public Ablak_Szervezet()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Szervezet_Load(object sender, System.EventArgs e)
        {
            Alap_tábla_író();
        }

        private void Start()
        {
            GombLathatosagKezelo.Beallit(this);
        }

        private void Új_adat_Click(object sender, EventArgs e)
        {
            new Ablak_Szervezet_Kezelo().Kezelés(null, Alap_tábla_író);
        }

        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            Alap_tábla_író();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Szervezet" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };

                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length);
                MyE.DataGridViewToExcel(fájlexc, Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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

        private void Alap_tábla_író()
        {
            try
            {
                Adatok = KézSzervezet.Lista_Adatok();

                List<OszlopDefinició> oszlopok = new List<OszlopDefinició>
                {
                    new OszlopDefinició("Id", "Azonosító", 120, "", DataGridViewContentAlignment.MiddleLeft),
                    new OszlopDefinició("Szervezet", "Szervezet Neve", 340),
                    new OszlopDefinició("Státus", "Törölt", 120, "", DataGridViewContentAlignment.MiddleCenter)
                };

                TáblaKezelő.Feltöltés(Tábla, KötésiOsztály, Adatok, oszlopok);

                TáblaSzínezés();

                Tábla.Visible = true;
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

        private void TáblaSzínezés()
        {
            try
            {
                if (Tábla.Rows.Count < 1) return;

                foreach (DataGridViewRow Sor in Tábla.Rows)
                {
                    if (Sor.Cells["Státus"].Value.ToStrTrim() == "Törölt")
                        Sor.Cells["Státus"].Style.BackColor = Color.OrangeRed;
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

        private void AlapTáblaTartalom()
        {
            try
            {
                AdatTáblaALap.Clear();

                foreach (Adat_Szervezet rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaALap.NewRow();
                    Soradat["Id"] = rekord.Id;
                    Soradat["Szervezet"] = rekord.Szervezet;
                    Soradat["Státus"] = rekord.Státus == true ? "Törölt" : "Aktív";
                    AdatTáblaALap.Rows.Add(Soradat);
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) throw new HibásBevittAdat("Nincs kiválasztva érvényes sor.");

                // Adatok kinyerése a rácsból biztonságosan
                string idStr = Tábla.Rows[e.RowIndex].Cells["Id"].Value?.ToString() ?? "0";
                string nev = Tábla.Rows[e.RowIndex].Cells["Szervezet"].Value?.ToString() ?? "";
                string statusStr = Tábla.Rows[e.RowIndex].Cells["Státus"].Value?.ToString() ?? "Aktív";

                int id = int.Parse(idStr);
                bool isTorolt = (statusStr == "Törölt");

                Adat_Szervezet kivalasztott = new Adat_Szervezet(id, nev, isTorolt);
                new Ablak_Szervezet_Kezelo().Kezelés(kivalasztott, Alap_tábla_író);
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