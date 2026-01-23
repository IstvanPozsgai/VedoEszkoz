using System;
using System.Drawing;
using System.Windows.Forms;

namespace VédőEszköz
{
    public static class Kezelő_Kép
    {
        public static void KépMegnyitás(PictureBox Keret, string helykép, ToolTip ToolTip1)
        {
            Keret.Image?.Dispose();
            Keret.Image = null;
            GC.Collect();

            using (Image kép = Image.FromFile(helykép))
            {
                Keret.Image = new Bitmap(kép);
                ToolTip1.SetToolTip(Keret, helykép);
            }
            Keret.Visible = true;
        }
    }
}
