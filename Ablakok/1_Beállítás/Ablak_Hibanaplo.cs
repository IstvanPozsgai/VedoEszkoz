using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VédőEszköz
{

    public partial class Ablak_Hibanaplo : Form
    {
        Hibanapló_Részletes Ablak;
        readonly DataTable AdatTábla = new DataTable();

#pragma warning disable IDE0044
        List<string> SorAdat = new List<string>();
#pragma warning restore IDE0044


        public Ablak_Hibanaplo()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Hibanaplo_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Fejlec();
            Tablalista_kiírás();
        }

        private void Tablalista_kiírás()
        {
            ABFeltöltése();
            Hibanaplo_Tablazat.CleanFilterAndSort();
            Hibanaplo_Tablazat.DataSource = AdatTábla;
            Hibanaplo_Tablazat.Sort(Hibanaplo_Tablazat.Columns["TeljesIdő"], ListSortDirection.Descending);
            OszlopSzélesség();
            Hibanaplo_Tablazat.Refresh();
            Hibanaplo_Tablazat.Visible = true;
            Hibanaplo_Tablazat.ClearSelection();
        }

        private void Fejlec()
        {
            // Dátum;Idő;Telephely;Felhasználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; TeljesIdő
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Idő");
            AdatTábla.Columns.Add("Telephely");
            AdatTábla.Columns.Add("Felhasználó");
            AdatTábla.Columns.Add("Hiba üzenet");
            AdatTábla.Columns.Add("Hiba osztály");
            AdatTábla.Columns.Add("Hiba metódus");
            AdatTábla.Columns.Add("Névtér");
            AdatTábla.Columns.Add("Egyéb");
            AdatTábla.Columns.Add("TeljesIdő");
        }

        private void ABFeltöltése()
        {
            try
            {
                AdatTábla.Clear();

                int ideiEv = DateTime.Now.Year;
                List<string> osszesSor = new List<string>();
                osszesSor.AddRange(ÉvesLogFajltBetolt(ideiEv).Skip(1));
                osszesSor.AddRange(ÉvesLogFajltBetolt(ideiEv - 1).Skip(1));

                foreach (string sor in osszesSor)
                {
                    // Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum
                    DataRow Soradat = AdatTábla.NewRow();
                    string[] mezok = sor.Split(';');
                    string[] darabol = mezok[0].Split(' ');
                    string Dátum = darabol[0].ToÉrt_DaTeTime().ToString("yyyy.MM.dd");
                    string Idő = darabol[1].Replace(".", ":").ToÉrt_DaTeTime().ToString("HH:mm:ss");

                    Soradat["Dátum"] = Dátum;
                    Soradat["Idő"] = Idő;
                    Soradat["Telephely"] = mezok[1];
                    Soradat["Felhasználó"] = mezok[2];
                    Soradat["Hiba üzenet"] = mezok[3];
                    Soradat["Hiba osztály"] = mezok[4];
                    Soradat["Hiba metódus"] = mezok[5];
                    Soradat["Névtér"] = mezok[6];
                    Soradat["Egyéb"] = mezok[7];
                    Soradat["TeljesIdő"] = mezok[0];
                    AdatTábla.Rows.Add(Soradat);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Hiányzó fájl", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OszlopSzélesség()
        {
            Hibanaplo_Tablazat.Columns["Dátum"].Width = 100;
            Hibanaplo_Tablazat.Columns["Idő"].Width = 85;
            Hibanaplo_Tablazat.Columns["Telephely"].Width = 130;
            Hibanaplo_Tablazat.Columns["Felhasználó"].Width = 115;
            Hibanaplo_Tablazat.Columns["Hiba üzenet"].Width = 450;
            Hibanaplo_Tablazat.Columns["Hiba osztály"].Width = 300;
            Hibanaplo_Tablazat.Columns["Hiba metódus"].Width = 300;
            Hibanaplo_Tablazat.Columns["Névtér"].Width = 100;
            Hibanaplo_Tablazat.Columns["Egyéb"].Width = 100;

            Hibanaplo_Tablazat.Columns["TeljesIdő"].Visible = false;
        }

        private string[] ÉvesLogFajltBetolt(int ev)
        {
            string[] Válasz = new string[] { };
            try
            {
                string fajlUtvonal = $@"{Application.StartupPath}\Adatok\Hibanapló\{ev}\hiba{ev}.csv";
                if (!FileLetezik(ev)) throw new HibásBevittAdat($"A {ev}. évi hibanapló fájl nem található.\n\n{fajlUtvonal}");
                Válasz = File.ReadAllLines(fajlUtvonal, Encoding.GetEncoding(1250));
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
            return Válasz;
        }

        private bool FileLetezik(int ev)
        {
            string fajlUtvonal = $@"{Application.StartupPath}\Adatok\Hibanapló\{ev}\hiba{ev}.csv";

            return File.Exists(fajlUtvonal);
        }

        private void Részletek_Click(object sender, EventArgs e)
        {
            try
            {
                if (SorAdat.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva érvényes sor.");
                Ablak?.Close();
                Ablak = new Hibanapló_Részletes();
                Ablak.RészletesAdatok(SorAdat);
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

        private void Hibanaplo_Tablazat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Hibanaplo_Tablazat.Rows[e.RowIndex].Selected = true;
                SorAdat.Clear();
                for (int oszlop = 0; oszlop < Hibanaplo_Tablazat.Columns.Count; oszlop++)
                {
                    SorAdat.Add(Hibanaplo_Tablazat.Rows[e.RowIndex].Cells[oszlop].Value.ToStrTrim());
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
    }
}
