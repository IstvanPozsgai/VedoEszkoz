using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        /// <summary>
        /// Cellán belüli szöveg formázásokat láncba kell megadni, a lánc minden elemére vonatkozóan
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="aláhúzott"></param>
        /// <param name="dőlt"></param>
        /// <param name="vastag"></param>
        /// <param name="kezdet"></param>
        /// <param name="hossz"></param>
        public static void Cella_Betű(Beállítás_CellaSzöveg beállítás)
        {
            try
            {
                CellaBeállítás.Add(beállítás);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Cella_Betű() \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static void AlkalmazCellaFormázás(string fájlnév, List<Beállítás_CellaSzöveg> Beállítások)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fájlnév, true))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                List<Sheet> sheets = workbookPart.Workbook.Sheets?.Elements<Sheet>().ToList();

                foreach (Beállítás_CellaSzöveg beállítás in Beállítások)
                {
                    // Munkalap keresése név alapján
                    Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == beállítás.MunkalapNév);
                    if (sheet?.Id == null) continue;

                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>() ?? new SheetData();

                    // Sor és oszlop meghatározása cellahivatkozásból (pl. "B5")
                    if (!TryParseCellReference(beállítás.Cella, out uint rowIndex, out string colLetter)) continue;

                    // Sor megtalálása vagy létrehozása
                    Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
                    if (row == null)
                    {
                        row = new Row() { RowIndex = rowIndex };
                        sheetData.Append(row);
                    }

                    // Cell megtalálása vagy létrehozása
                    string fullRef = $"{colLetter}{rowIndex}";
                    Cell cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference?.Value == fullRef);
                    if (cell == null)
                    {
                        cell = new Cell() { CellReference = fullRef };
                        row.Append(cell);
                    }

                    // Előző tartalom törlése
                    cell.RemoveAllChildren();

                    // InlineString inicializálása
                    cell.DataType = CellValues.InlineString;
                    InlineString inlineString = new InlineString();
                    cell.Append(inlineString);

                    // Szöveg felbontása futásokra (formázott + nem formázott részek)
                    var runs = SplitTextIntoRuns(beállítás.FullText, beállítás.Beállítások);
                    foreach (var run in runs)
                    {
                        // Új Run csak akkor, ha van szöveg
                        if (string.IsNullOrEmpty(run.Text)) continue; // vagy dobjon figyelmeztetést, de ne adj hozzá üres Text-et

                        var runElement = new Run();

                        var runProps = new RunProperties();
                        runProps.Append(new FontSize { Val = beállítás.Betű.Méret });
                        runProps.Append(new RunFont { Val = beállítás.Betű.Név });
                        runProps.Append(new Color { Theme = 1 });
                        runProps.Append(new FontFamily { Val = 1 });

                        if (run.Bold) runProps.Append(new Bold());
                        if (run.Italic) runProps.Append(new Italic());
                        if (run.Underline) runProps.Append(new Underline());

                        runElement.Append(runProps);

                        // Csak akkor hozz létre Text elemet, ha van nem-null szöveg
                        var text = new Text { Text = run.Text };

                        // Space attribútum csak akkor, ha whitespace fontos ÉS a szöveg nem üres
                        if (run.Text.Length > 0 && (run.Text.Contains(" ") || run.Text.StartsWith(" ") || run.Text.EndsWith(" ")))
                            text.Space = SpaceProcessingModeValues.Preserve;

                        runElement.Append(text);
                        inlineString.Append(runElement);
                    }
                }
                doc.Save(); // OpenXML 2.5+ támogatja
            }
        }

        private static List<(string Text, bool Bold, bool Italic, bool Underline, bool IsFormatted)>
       SplitTextIntoRuns(string fullText, List<RichTextRun> Beállítások)
        {
            List<(string, bool, bool, bool, bool)> result = new List<(string, bool, bool, bool, bool)>();
            List<(int Start, int End, RichTextRun Run)> spans = new List<(int Start, int End, RichTextRun Run)>();

            // Formázott tartományok rendezése
            foreach (RichTextRun run in Beállítások.OrderBy(r => r.Start))
            {
                spans.Add((run.Start, run.Start + run.Hossz, run));
            }

            int currentPos = 0;
            foreach (var (start, end, run) in spans)
            {
                // Nem formázott rész a jelenlegi pozíciótól a formázott kezdetéig
                if (currentPos < start)
                {
                    result.Add((fullText.Substring(currentPos, start - currentPos), false, false, false, false));
                }

                // Formázott rész
                result.Add((fullText.Substring(start, end - start), run.Vastag, run.Dőlt, run.Aláhúzott, true));
                currentPos = end;
            }

            // Utolsó nem formázott rész
            if (currentPos < fullText.Length)
            {
                result.Add((fullText.Substring(currentPos), false, false, false, false));
            }

            return result;
        }

        private static bool TryParseCellReference(string cellRef, out uint row, out string colLetter)
        {
            row = 0;
            colLetter = null;

            var match = Regex.Match(cellRef.Trim(), @"^([A-Za-z]+)(\d+)$");
            if (!match.Success) return false;

            colLetter = match.Groups[1].Value.ToUpper();
            return uint.TryParse(match.Groups[2].Value, out row);
        }
    }
}
