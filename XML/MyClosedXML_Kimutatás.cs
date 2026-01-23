using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace VédőEszköz
{
    public static partial class MyClosedXML_Excel
    {
        /// <summary>
        /// Kimutatás (Pivot Table) készítése ClosedXML segítségével.
        /// Ez a FŐ metódus, amely kezeli az összesítési módokat is.
        /// </summary>
        public static void Kimutatás_Fő(Beállítás_Kimutatás beállítás)
        {
            try
            {
                IXLWorksheet Adatok_lap = xlWorkBook.Worksheet(beállítás.Munkalapnév);

                // → ÚJ SOR: Adatok tisztítása XML-hibás karakterektől
                TisztitsdMegAdatLapotXmlHibakarakterektol(Adatok_lap);


                //Ha nincs kimutatás lap akkor létrehoz egyet
                IXLWorksheet Kimutatás_lap;
                try
                {
                    Kimutatás_lap = xlWorkBook.Worksheet(beállítás.Kimutatás_Munkalapnév);
                }
                catch
                {
                    Kimutatás_lap = xlWorkBook.Worksheets.Add(beállítás.Kimutatás_Munkalapnév);
                }

                IXLRange AdatRange = Adatok_lap.Range(beállítás.Balfelső, beállítás.Jobbalsó);
                IXLCell celCella = Kimutatás_lap.Cell(beállítás.Kimutatás_cella);
                IXLPivotTable pivotTable = Kimutatás_lap.PivotTables.Add(beállítás.Kimutatás_név, celCella, AdatRange);

                //  Sorok
                if (beállítás.SorNév != null && beállítás.SorNév.Count > 0)
                    foreach (string nev in beállítás.SorNév)
                    {
                        IXLPivotField field = pivotTable.RowLabels.Add(nev);
                        field.AddSubtotal(XLSubtotalFunction.Automatic);
                    }

                // Oszlopok
                if (beállítás.OszlopNév != null && beállítás.OszlopNév.Count > 0)
                    foreach (string nev in beállítás.OszlopNév)
                        pivotTable.ColumnLabels.Add(nev);

                //    Szűrők
                if (beállítás.SzűrőNév != null && beállítás.SzűrőNév.Count > 0)
                    foreach (string nev in beállítás.SzűrőNév)
                        pivotTable.ReportFilters.Add(nev);

                //   Értékek
                if (beállítás.ÖsszesítNév != null && beállítás.ÖsszesítNév.Count > 0)
                {
                    for (int i = 0; i < beállítás.ÖsszesítNév.Count; i++)
                    {
                        string mezoNev = beállítás.ÖsszesítNév[i];
                        string mod = (beállítás.Összesítés_módja != null && i < beállítás.Összesítés_módja.Count) ? beállítás.Összesítés_módja[i] : "xlSum";

                        IXLPivotValue valueField = pivotTable.Values.Add(mezoNev);

                        switch (mod)
                        {
                            case "xlCount": // Darabszám
                                valueField.SummaryFormula = XLPivotSummary.Count;
                                valueField.CustomName = mezoNev + " Összeg";
                                break;

                            case "xlSum": // Összeg
                            default:
                                valueField.SummaryFormula = XLPivotSummary.Sum;
                                valueField.CustomName = mezoNev + " db";
                                break;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kimutatás_Fő \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Eltávolítja az összes érvénytelen XML karaktert (0x00–0x1F, kivéve \t, \n, \r) 
        /// a megadott munkalap használt celláiból.
        /// Csak a szöveges értékeket módosítja.
        /// </summary>
        /// <param name="adatLap">A tisztítandó munkalap (pl. "Adatok")</param>
        /// <returns>A tisztított munkalap (helyben módosítva)</returns>
        public static IXLWorksheet TisztitsdMegAdatLapotXmlHibakarakterektol(IXLWorksheet adatLap)
        {
            if (adatLap == null)
                throw new ArgumentNullException(nameof(adatLap));

            var hasznaltTartomany = adatLap.RangeUsed();
            if (hasznaltTartomany == null)
                return adatLap;

            foreach (IXLCell cell in hasznaltTartomany.Cells())
            {
                // XLCellValue struct, soha nem null → ToString() biztonságos
                string cellSzoveg = cell.Value.ToString();

                // Csak akkor tisztítunk, ha van érték ÉS az szövegként értelmezhető
                if (!string.IsNullOrEmpty(cellSzoveg))
                {
                    string tisztaSzoveg = TisztaXmlString(cellSzoveg);
                    if (!string.Equals(cellSzoveg, tisztaSzoveg, StringComparison.Ordinal))
                    {
                        cell.Value = tisztaSzoveg;
                    }
                }
            }

            return adatLap;
        }

        /// <summary>
        /// Eltávolítja az érvénytelen XML vezérlőkaraktereket egy sztringből.
        /// Megőrzi: \t (0x09), \n (0x0A), \r (0x0D)
        /// Eltávolítja: 0x00–0x08, 0x0B–0x0C, 0x0E–0x1F
        /// </summary>
        private static string TisztaXmlString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            bool talaltHibas = false;
            foreach (char c in input)
            {
                if (c < 0x20 && c != '\t' && c != '\n' && c != '\r')
                {
                    talaltHibas = true;
                    break;
                }
            }

            if (!talaltHibas)
                return input;

            var sb = new StringBuilder(input.Length);
            foreach (char c in input)
            {
                if (c >= 0x20 || c == '\t' || c == '\n' || c == '\r')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
