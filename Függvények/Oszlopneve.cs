using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Vedo;

public static partial class Függvénygyűjtemény
{
    public static string Oszlopnév(int sorszám)
    {
        string oszlopNev = string.Empty;
        int eredetiSorszám = sorszám;
        try
        {
            if (sorszám < 1) throw new ArgumentOutOfRangeException(nameof(sorszám), "Az oszlopszámnak 1 vagy nagyobbnak kell lennie.");
            while (sorszám > 0)
            {
                sorszám--;
                oszlopNev = (char)('A' + (sorszám % 26)) + oszlopNev;
                sorszám /= 26;
            }
        }
        catch (Exception ex)
        {
            StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
            string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
            HibaNapló.Log(ex.Message, $"Oszlopnév(sorszám {eredetiSorszám}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return oszlopNev;
    }

    public static Color Színnév(int szín)
    {
        Color Válasz;
        if (szín < 0 || szín > 16777215)
        {
            // Ha érvénytelen, akkor fehér színnel hívjuk meg a másikat
            Válasz = Color.White;
        }
        else
        {
            // Konvertáljuk az int-et Color-á, majd hívjuk a másik túlterhelést
            Color color = Color.FromArgb(
                (szín >> 16) & 0xFF, // R
                (szín >> 8) & 0xFF,  // G
                szín & 0xFF          // B
            );
            Válasz = color;
        }
        return Válasz;
    }
}
