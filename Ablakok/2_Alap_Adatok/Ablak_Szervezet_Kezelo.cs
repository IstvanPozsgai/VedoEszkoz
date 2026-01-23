using InputForms;
using System;
using System.Windows.Forms;


namespace VédőEszköz

{
    public class Ablak_Szervezet_Kezelo
    {
        InputForm form;
        Form Ablak;

        InputTextbox TxtSzervezet;
        InputTextbox TxtRovidNev;
        InputSelect CmbStatus;

        public void Kezelés(Adat_Szervezet Adat, Action FrissitesCallback)
        {
            int id = Adat?.Id ?? 0;
            string nev = Adat?.Szervezet ?? "";
            string rovid = "";
            //Adat?.RovidNev ?? ""; 

            string status = (Adat != null && Adat.Státus) ? "Törölt" : "Aktív";

            Ablak = new Form();
            form = new InputForm(Ablak);
            Kezelő_Szervezet Kezelő = new Kezelő_Szervezet();

            TxtSzervezet = new InputTextbox("Szervezet neve:", nev).SetWidth(350).SetHeight(26);
            TxtRovidNev = new InputTextbox("Rövid név:", rovid).SetWidth(150).SetHeight(26);
            CmbStatus = new InputSelect("Státusz:", new string[] { "Aktív", "Törölt" }, 15);

            CmbStatus.Value = status;

            form.Add("Szervezet", TxtSzervezet)
                .Add("Rovid", TxtRovidNev)
                .Add("Status", CmbStatus)
                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Rögzít")
                .OnSubmit(() =>
                {
                    MentésFolyamat(id, Kezelő, FrissitesCallback);
                });

            Ablak.Width = form.Width + 40;
            Ablak.Height = form.Height + 80;
            Ablak.Text = (id == 0) ? "Új Szervezet felvitele" : "Szervezet módosítása";
            //Ablak.Icon = Properties.Resources.ProgramIkon;
            Ablak.StartPosition = FormStartPosition.CenterScreen;
            Ablak.FormBorderStyle = FormBorderStyle.FixedDialog;
            Ablak.MaximizeBox = false;

            Ablak.ShowDialog();
        }

        private void MentésFolyamat(int id, Kezelő_Szervezet kezelo, Action frissitesCallback)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtSzervezet.Value.ToString()))
                    throw new Exception("A szervezet nevét kötelező kitölteni!");

                string ujNev = TxtSzervezet.Value.ToString();
                string ujRovid = TxtRovidNev.Value.ToString();
                bool torolt = CmbStatus.Value.ToString() == "Törölt";

                Adat_Szervezet rekord = new Adat_Szervezet(id, ujNev, torolt);

                kezelo.Döntés(rekord);

                MessageBox.Show("Sikeres mentés!", "Infó", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frissitesCallback?.Invoke();
                Ablak.Close();
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