using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;


namespace VédőEszköz
{
    public static partial class MyClosedXML_Excel
    {
        readonly static Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
        private static readonly XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";


   

        private static DateTime? ParseIdo(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return null; // Hiányzó adat

            // Támogatott formátumok (ISO 8601, magyar dátum, csak idő)
            var formats = new[] {
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy.MM.dd. H:mm",
                "H:mm"        };

            if (DateTime.TryParseExact(s.Trim(), formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dt))
                return dt;

            // Hibás formátum: naplózd vagy dobj kivételt
            throw new FormatException($"Nem értelmezhető időérték: '{s}'");
        }

        public static DateTime KidobóDátumEll(string Fájlnév)
        {
            DateTime Válasz = new DateTime(1900, 1, 1);
            try
            {

                // fájl beolvasása nyers szövegként
                string raw = File.ReadAllText(Fájlnév);
                // tisztítás
                string clean = TisztitXML(raw);
                // XML betöltése a tisztított szövegből
                XDocument doc = XDocument.Parse(clean);

                // A dátum a 2. sor első cellájában van
                var row = doc.Descendants(ss + "Row").Skip(1).FirstOrDefault();
                if (row == null) return Válasz;
                var cell = row.Element(ss + "Cell");
                if (cell == null) return Válasz;
                string value = XmlCell.GetValue(cell);
                if (DateTime.TryParse(value, out Válasz)) return Válasz;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KidobóDátumEll", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Válasz;
        }

        public static string TisztitXML(string input)
        {
            return new string(input.Where(ch =>
        ch == 0x9 || ch == 0xA || ch == 0xD || ch >= 0x20
        ).ToArray());
        }

        public static bool Betöltéshelyes(string Melyik, List<string> FejlécBeolvasott)
        {
            bool válasz = true;
            try
            {
                List<Adat_Excel_Beolvasás> Adatok = KézBeolvasás.Lista_Adatok();
                //csak azokat az adatokat nézzük amit be kell tölteni.
                Adatok = (from a in Adatok
                          where a.Csoport == Melyik.Trim()
                          && a.Státusz == false
                          && a.Változónév.Trim() != "0"
                          orderby a.Oszlop
                          select a).ToList();
                // Végignézzük a változók listáját és ha van benne olyan ami nincs a táblázatban átállítjuk a státusszát
                foreach (Adat_Excel_Beolvasás rekord in Adatok)
                {
                    bool volt = false;
                    int i = 0;
                    while (volt == false && i < FejlécBeolvasott.Count)
                    {
                        if (rekord.Fejléc.Trim() == FejlécBeolvasott[i].Trim()) volt = true;
                        i++;
                    }
                    if (!volt)
                    {
                        válasz = false;
                        break;
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Függvénygyűjtemény - Betöltéshelyes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

        private static Dictionary<string, int> FejlécBeolvasásD(XDocument doc)
        {
            Dictionary<string, int> Válasz = new Dictionary<string, int>();
            try
            {
                List<XElement> rows = doc.Descendants(ss + "Row").ToList();
                // A fejléc a 6. sor (index 5)
                XElement headerRow = rows[5];
                List<XElement> headerCells = headerRow.Elements(ss + "Cell").ToList();
                // beolvassuk listába a fejlécet
                for (int i = 0; i < headerCells.Count; i++)
                {

                    string Fejléc = XmlCell.GetValue(headerCells[i]).Trim();
                    Válasz[Fejléc] = i;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "FejlécBeolvasás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }


        private static List<string> FejlécBeolvasás(XDocument doc)
        {
            List<string> Válasz = new List<string>();
            try
            {
                List<XElement> rows = doc.Descendants(ss + "Row").ToList();
                // A fejléc a 6. sor (index 5)
                XElement headerRow = rows[5];
                List<XElement> headerCells = headerRow.Elements(ss + "Cell").ToList();
                // beolvassuk listába a fejlécet
                for (int i = 0; i < headerCells.Count; i++)
                {
                    string Fejléc = XmlCell.GetValue(headerCells[i]).Trim();
                    Válasz.Add(Fejléc);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "FejlécBeolvasás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }
    }
}

public static class XmlCell
{
    private static readonly XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

    public static string GetValue(XElement cell)
    {
        return cell.Element(ss + "Data")?.Value?.Trim() ?? "";
    }
}


