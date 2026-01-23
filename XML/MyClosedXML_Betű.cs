using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Windows.Forms;




namespace VédőEszköz
{

    public static partial class MyClosedXML_Excel
    {
        public static void Betű(string munkalapnév, string mit, Beállítás_Betű beállítás)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange cella = munkalap.Range(mit);
                cella.Style.Font.FontColor = XLColor.FromColor(beállítás.Szín);
                cella.Style.Font.Italic = beállítás.Dőlt;
                cella.Style.Font.Bold = beállítás.Vastag;
                cella.Style.Font.SetFontName(beállítás.Név);
                cella.Style.Font.FontSize = beállítás.Méret;
                if (!string.IsNullOrWhiteSpace(beállítás.Formátum)) cella.Style.NumberFormat.Format = beállítás.Formátum;
                if (beállítás.Aláhúzott) cella.Style.Font.Underline = XLFontUnderlineValues.Single;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit})  Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Munkalap_betű(string munkalapnév, Beállítás_Betű beállítás)
        {
            try
            {

                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                // Stílus beállítása a munkalap alapértelmezett stílusára (ez éri el a teljes munkalapot)
                munkalap.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                munkalap.Style.Font.SetFontName(beállítás.Név);
                munkalap.Style.Font.FontSize = beállítás.Méret;
                munkalap.Style.Font.Strikethrough = false;
                munkalap.Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Baseline;
                munkalap.Style.Font.Underline = XLFontUnderlineValues.None;
                // ClosedXML nem támogatja közvetlenül ThemeColor, TintAndShade, OutlineFont, Shadow stb. ugyanúgy,
                // mint az Interop – ezeket általában nem kell külön visszaállítani, mert alapértelmezés szerint nincsenek bekapcsolva.

            }
            catch (Exception ex)
            {
                var hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_betű(név: {beállítás.Név}, méret: {beállítás.Méret}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
