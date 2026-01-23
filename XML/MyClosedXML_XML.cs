using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        readonly static Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
        private static readonly XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";


        public static List<Adat_Kidobó> BeolvasKidobó(string Fájlnév)
        {
            List<Adat_Kidobó> Adatok = new List<Adat_Kidobó>();
            try
            {
                // fájl beolvasása nyers szövegként
                var enc = Encoding.GetEncoding("ISO-8859-2");
                string raw;
                using (var sr = new StreamReader(Fájlnév, enc, detectEncodingFromByteOrderMarks: false))
                { raw = sr.ReadToEnd(); }
                string clean = TisztitXML(raw);

                XDocument doc;
                try
                {
                    doc = XDocument.Parse(clean);
                }
                catch (Exception ex1)
                {
                    throw new HibásBevittAdat("A fájl nem olvasható: " + ex1.Message);
                }
                Dictionary<string, int> Fejlécek = FejlécBeolvasásD(doc);
                //Ellenőrzés
                if (!Betöltéshelyes("Kidobó", FejlécBeolvasás(doc))) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");


                //Meghatározzuk a beolvasó tábla elnevezéseit 
                //Oszlopnevek beállítása
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();
                string oszlopVisz = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Viszonylat" select a.Fejléc).FirstOrDefault();
                string oszlopForg = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Forgalmiszám" select a.Fejléc).FirstOrDefault();
                string oszlopJvez = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Jvez" select a.Fejléc).FirstOrDefault();
                string oszlopKezd = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Kezdés" select a.Fejléc).FirstOrDefault();
                string oszlopVég = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Végzés" select a.Fejléc).FirstOrDefault();
                string oszlopKezdH = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Kezdéshely" select a.Fejléc).FirstOrDefault();
                string oszlopVégH = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Végzéshely" select a.Fejléc).FirstOrDefault();
                string oszlopKód = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Kód" select a.Fejléc).FirstOrDefault();
                string oszlopSzer = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Szerelvénytípus" select a.Fejléc).FirstOrDefault();
                string oszlopTörzs = (from a in oszlopnév where a.Csoport == "Kidobó" && a.Státusz == false && a.Változónév == "Törzsszám" select a.Fejléc).FirstOrDefault();


                //  Ha a fejléc jó → mehet a sima index alapú beolvasás
                var rows = doc.Descendants(ss + "Row").ToList();

                for (int r = 6; r < rows.Count; r++)
                {
                    XElement row = rows[r];
                    List<XElement> cells = row.Elements(ss + "Cell").ToList();
                    // Segédfüggvény: oszlopnév alapján érték
                    string GetCellValue(string columnName)
                    {
                        if (Fejlécek.TryGetValue(columnName, out int index) && index < cells.Count)
                            return XmlCell.GetValue(cells[index]);
                        return null;
                    }

                    if (cells.Count == 0) continue;

                    string viszSzolg = GetCellValue(oszlopVisz); ;
                    if (string.IsNullOrWhiteSpace(viszSzolg)) continue;    //Üres sort kihagy

                    string[] darabol = viszSzolg.Split('/');

                    string Viszonylat = darabol[0].Trim();
                    string Forgalmiszám = GetCellValue(oszlopForg);
                    string Szolgálatiszám = darabol[1].Trim();
                    string Jvez = GetCellValue(oszlopJvez);
                    DateTime Kezdés = ParseIdo(GetCellValue(oszlopKezd)) ?? new DateTime(1900,1,1);
                    DateTime Végzés = ParseIdo(GetCellValue(oszlopVég)) ?? new DateTime(1900,1,1);
                    string Kezdéshely = GetCellValue(oszlopKezdH);
                    string Végzéshely = GetCellValue(oszlopVégH);
                    string Kód = GetCellValue(oszlopKód);
                    string Szerelvénytípus = GetCellValue(oszlopSzer);
                    string Törzsszám = GetCellValue(oszlopTörzs);

                    Adat_Kidobó adat = new Adat_Kidobó(
                        Viszonylat,
                        Forgalmiszám,
                        Szolgálatiszám,
                        Jvez,
                        Kezdés,
                        Végzés,
                        Kezdéshely,
                        Végzéshely,
                        Kód,
                        "_",
                        "_",
                        "_",
                        Szerelvénytípus,
                        Törzsszám
                    );
                    Adatok.Add(adat);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "BeolvasKidobó", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Adatok;
        }



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


