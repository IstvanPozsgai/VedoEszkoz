using System;
using System.Windows.Forms;



namespace VédőEszköz
{
    public partial class Ablak_Jelszó_Változtatás : Form
    {
        public delegate void Event_Kidobó();
        public event Event_Kidobó Változás;
        readonly Kezelő_Users Kéz = new Kezelő_Users();

#pragma warning disable IDE0044
        Adat_Users Adat;
#pragma warning restore IDE0044
        public Ablak_Jelszó_Változtatás(Adat_Users adat)
        {
            InitializeComponent();
            Adat = adat;
        }


        private void BtnMégse_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btnok_Click(object sender, EventArgs e)
        {
            try
            {
                if (Jelszó.HashPassword(TxtPassword.Text.Trim()) != Adat.Password)
                {
                    TxtPassword.Focus();
                    throw new HibásBevittAdat("A régi jelszó nem egyezik a tárolt adatokkal !");
                }

                // ha nem egyforma a két jelszó akkor kilép
                if (Első.Text.Trim() != Második.Text.Trim())
                {
                    MessageBox.Show("A két jelszó nem egyezik!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Első.Focus();
                    return;
                }
                if (Első.Text.Trim().Length < 5)
                {
                    MessageBox.Show("A jelszónak 5 karakternél hosszabbnak kell lennie !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Első.Focus();
                    return;
                }
                if (Első.Text.Trim().Length > 20)
                {
                    MessageBox.Show("A jelszónak 20 karakternél rövidebbnek kell lennie !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Első.Focus();
                    return;
                }

                Adat_Users adat = new Adat_Users(Adat.UserId, Jelszó.HashPassword(Első.Text.Trim()), false);
                Kéz.MódosításJeszó(adat);
                Változás?.Invoke();
                MessageBox.Show("A jelszó módosításra került !", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

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




        private void TxtPassword_MouseLeave(object sender, EventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = true;
        }

        private void TxtPassword_MouseMove(object sender, MouseEventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = false;
        }

        private void Első_MouseMove(object sender, MouseEventArgs e)
        {
            Első.UseSystemPasswordChar = false;
        }

        private void Első_MouseLeave(object sender, EventArgs e)
        {
            Első.UseSystemPasswordChar = true;
        }
        private void Második_MouseMove(object sender, MouseEventArgs e)
        {
            Második.UseSystemPasswordChar = false;
        }

        private void Második_MouseLeave(object sender, EventArgs e)
        {
            Második.UseSystemPasswordChar = true;
        }

        private void AblakJelszóváltoztatás_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_Jelszó_Változtatás_Load(object sender, EventArgs e)
        {

        }
    }
}