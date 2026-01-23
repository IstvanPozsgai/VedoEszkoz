using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;
using static Villamos.V_MindenEgyéb.Enumok;


namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {

        public static void AlkalmazFerdeVonalak(string path, List<Beállítás_Ferde> beállítások)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(path, true))
            {
                WorkbookPart workbookPart = document.WorkbookPart;
                WorkbookStylesPart stylesPart = workbookPart.WorkbookStylesPart;

                if (stylesPart == null)
                {
                    stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                    stylesPart.Stylesheet = new Stylesheet(
                        new Fonts(new Font()),
                        new Fills(new Fill()),
                        new Borders(new Border()),
                        new CellFormats(new CellFormat())
                    );
                }

                Stylesheet stylesheet = stylesPart.Stylesheet;

                if (stylesheet.Borders == null)
                    stylesheet.Borders = new Borders(new Border());

                if (stylesheet.CellFormats == null)
                    stylesheet.CellFormats = new CellFormats(new CellFormat());

                foreach (Beállítás_Ferde beállítás in beállítások)
                {
                    Sheet sheet = workbookPart.Workbook.Descendants<Sheet>()
                        .FirstOrDefault(s => s.Name == beállítás.Munkalap);

                    if (sheet == null)
                        continue;

                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    var cellRefs = GetCellReferences(beállítás.Terület);

                    foreach (var cellRef in cellRefs)
                    {
                        ApplyDiagonalToCell(sheetData, cellRef, stylesheet, beállítás);
                    }

                    worksheetPart.Worksheet.Save();
                }

                stylesPart.Stylesheet.Save();
            }
        }

        private static void ApplyDiagonalToCell(SheetData sheetData, string cellRef, Stylesheet stylesheet, Beállítás_Ferde beállítás)
        {
            GetCellCoordinates(cellRef, out int col, out int rowIndex);

            Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
            if (row == null)
            {
                row = new Row() { RowIndex = (uint)rowIndex };
                sheetData.Append(row);
            }

            Cell cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference == cellRef);
            if (cell == null)
            {
                cell = new Cell() { CellReference = cellRef };
                row.Append(cell);
            }

            uint originalStyleIndex = cell.StyleIndex ?? 0;
            CellFormat originalFormat = (CellFormat)stylesheet.CellFormats.ElementAt((int)originalStyleIndex);

            Border newBorder = CloneBorder(originalFormat, stylesheet, beállítás);

            newBorder.DiagonalUp = beállítás.Jobb;
            newBorder.DiagonalDown = !beállítás.Jobb;

            // ✅ FONTOS: diagonális vonal stílusa
            if (newBorder.DiagonalBorder == null)
                newBorder.DiagonalBorder = new DiagonalBorder();

            newBorder.DiagonalBorder.Style = BorderStyleValues.Thin;

            uint newBorderId = (uint)stylesheet.Borders.Count;
            stylesheet.Borders.Append(newBorder);

            uint newFormatId = (uint)stylesheet.CellFormats.Count;

            //CellFormat newFormat = new CellFormat(originalFormat.OuterXml)
            //{
            //    BorderId = newBorderId,
            //    ApplyBorder = true
            //};

            CellFormat newFormat = new CellFormat
            {
                BorderId = newBorderId,
                ApplyBorder = true
            };

            // Másold át MINDEN releváns tulajdonságot az eredeti formából
            if (originalFormat != null)
            {
                if (originalFormat.NumberFormatId != null) newFormat.NumberFormatId = originalFormat.NumberFormatId;
                if (originalFormat.FontId != null) newFormat.FontId = originalFormat.FontId;
                if (originalFormat.FillId != null) newFormat.FillId = originalFormat.FillId;
                if (originalFormat.ApplyNumberFormat != null) newFormat.ApplyNumberFormat = originalFormat.ApplyNumberFormat;
                if (originalFormat.ApplyFont != null) newFormat.ApplyFont = originalFormat.ApplyFont;
                if (originalFormat.ApplyFill != null) newFormat.ApplyFill = originalFormat.ApplyFill;
                if (originalFormat.ApplyAlignment != null) newFormat.ApplyAlignment = originalFormat.ApplyAlignment;
                if (originalFormat.Alignment != null) newFormat.Alignment = (Alignment)originalFormat.Alignment.CloneNode(true);
                // ... ha van egyéb formázás
            }

            stylesheet.CellFormats.Append(newFormat);

            cell.StyleIndex = newFormatId;
        }


        private static Border CloneBorder(CellFormat format, Stylesheet stylesheet, Beállítás_Ferde beállítás)
        {
            uint borderId = format.BorderId ?? 0;
            Border original = (Border)stylesheet.Borders.ElementAt((int)borderId);

            // Klónozás
            LeftBorder left = (LeftBorder)(original.LeftBorder?.CloneNode(true) ?? new LeftBorder());
            RightBorder right = (RightBorder)(original.RightBorder?.CloneNode(true) ?? new RightBorder());
            TopBorder top = (TopBorder)(original.TopBorder?.CloneNode(true) ?? new TopBorder());
            BottomBorder bottom = (BottomBorder)(original.BottomBorder?.CloneNode(true) ?? new BottomBorder());
            DiagonalBorder diagonal = (DiagonalBorder)(original.DiagonalBorder?.CloneNode(true) ?? new DiagonalBorder());

            //// ✅ Itt tudod felülírni az értékeket
            if (beállítás.BalOldal != KeretVastagsag.Alap) left.Style = MilyenVastag(beállítás.BalOldal);
            if (beállítás.JobbOldal != KeretVastagsag.Alap) right.Style = MilyenVastag(beállítás.JobbOldal);
            if (beállítás.Felső != KeretVastagsag.Alap) top.Style = MilyenVastag(beállítás.Felső);
            if (beállítás.Alsó != KeretVastagsag.Alap) bottom.Style = MilyenVastag(beállítás.Alsó);


            //left.Style = BorderStyleValues.Thin;
            //left.Color = new Color() { Rgb = "000000" };
            //// piros
            //// Példa: top border vékony fekete
            //top.Style = BorderStyleValues.Thin;
            //top.Color = new Color() { Rgb = "000000" };

            return new Border(left, right, top, bottom, diagonal);
        }


        private static BorderStyleValues MilyenVastag(KeretVastagsag Oldal)
        {
            if (Oldal == KeretVastagsag.Vékony)
                return BorderStyleValues.Thin;
            else if (Oldal == KeretVastagsag.Közepes)
                return BorderStyleValues.Medium;
            else if (Oldal == KeretVastagsag.Vastag)
                return BorderStyleValues.Thick;
            else
                return BorderStyleValues.None;
        }

        private static List<string> GetCellReferences(string range)
        {
            var result = new List<string>();

            var parts = range.Split(':');
            if (parts.Length == 1)
            {
                result.Add(parts[0].ToUpper());
                return result;
            }

            var start = parts[0].ToUpper();
            var end = parts[1].ToUpper();

            GetCellCoordinates(start, out int startCol, out int startRow);
            GetCellCoordinates(end, out int endCol, out int endRow);

            for (int col = startCol; col <= endCol; col++)
            {
                for (int row = startRow; row <= endRow; row++)
                {
                    result.Add(ColumnNumberToName(col) + row);
                }
            }

            return result;
        }

        private static void GetCellCoordinates(string cellRef, out int col, out int row)
        {
            string colPart = new string(cellRef.TakeWhile(char.IsLetter).ToArray());
            string rowPart = new string(cellRef.SkipWhile(char.IsLetter).ToArray());

            col = ColumnNameToNumber(colPart);
            row = int.Parse(rowPart);
        }

        private static int ColumnNameToNumber(string columnName)
        {
            int sum = 0;
            foreach (char c in columnName)
            {
                sum *= 26;
                sum += (c - 'A' + 1);
            }
            return sum;
        }

        private static string ColumnNumberToName(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

    }




}




