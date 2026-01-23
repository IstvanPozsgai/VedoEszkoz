using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Villamos.Adatszerkezet;
using A = DocumentFormat.OpenXml.Drawing;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using C = DocumentFormat.OpenXml.Drawing.Charts;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        // EREDETI, kompatibilis hívás: cím nélkül
        public static void Diagram_Beallit(
            string munkalap,
            int felsőx,
            int felsőy,
            string táblafelső,
            string táblaalsó)
        {
            Diagram_Beallit(munkalap, felsőx, felsőy, táblafelső, táblaalsó, null);
        }

        // ÚJ overload: opcionális diagramcím
        public static void Diagram_Beallit(
            string munkalap,
            int felsőx,
            int felsőy,
            string táblafelső,
            string táblaalsó,
            string diagramCim)
        {
            DiagramBeállítások.Add(new Beállítás_Diagram
            {
                Munkalap = munkalap,
                FelsőX = felsőx,
                FelsőY = felsőy,
                TáblaFelső = táblafelső,
                TáblaAlsó = táblaalsó,
                DiagramCim = diagramCim
            });
        }

        private static void SplitCellReference(string cellRef, out string columnName, out uint rowIndex)
        {
            int i = 0;
            while (i < cellRef.Length && !char.IsDigit(cellRef[i]))
                i++;

            columnName = cellRef.Substring(0, i);
            rowIndex = uint.Parse(cellRef.Substring(i));
        }

        // Segédfüggvény: cella lekérése (pl. L2, M3 stb.)
        private static Cell GetCell(SheetData sheetData, string columnName, uint rowIndex)
        {
            var row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
            if (row == null) return null;

            string cellRef = columnName.ToUpperInvariant() + rowIndex.ToString();
            return row.Elements<Cell>()
                      .FirstOrDefault(c => c.CellReference != null && c.CellReference.Value == cellRef);
        }

        // Segédfüggvény: cella szöveges értéke (shared string-et is feloldjuk)
        private static string GetCellText(Cell cell, WorkbookPart wbPart)
        {
            if (cell == null || cell.CellValue == null)
                return null;

            string text = cell.CellValue.Text;

            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                var sst = wbPart.SharedStringTablePart?.SharedStringTable;
                if (sst != null &&
                    int.TryParse(text, out int ssIndex) &&
                    ssIndex >= 0 && ssIndex < sst.Count())
                {
                    return sst.ElementAt(ssIndex).InnerText;
                }
            }

            return text;
        }

        /// <summary>
        /// Kördiagram létrehozása megadott munkalapra és tartományra.
        /// felsőx/felsőy/alsóx/alsóy: most pozicionálásra vannak használva (oszlop/sor becslés).
        /// tábla: 2 oszlop (bal = kategória, jobb = érték), fejléc az első sorban.
        /// diagramCim: ha nem null/üres, akkor chart title-ként megjelenik.
        /// </summary>
        public static void Diagram(
            SpreadsheetDocument document,
            string munkalap,
            int felsőx,
            int felsőy,
            string táblafelső,
            string táblaalsó,
            string diagramCim)
        {
            var wbPart = document.WorkbookPart;
            var sheet = wbPart.Workbook.Descendants<Sheet>()
                                       .FirstOrDefault(s => s.Name == munkalap);
            if (sheet == null)
                throw new Exception($"Nem található ilyen munkalap: {munkalap}");

            var wsPart = (WorksheetPart)wbPart.GetPartById(sheet.Id);
            var sheetData = wsPart.Worksheet.Elements<SheetData>().FirstOrDefault();
            if (sheetData == null)
                return;

            // Tartomány feldarabolása: pl. "A1" → "A", 1; "B10" → "B", 10
            SplitCellReference(táblafelső, out string topCol, out uint topRow);
            SplitCellReference(táblaalsó, out string bottomCol, out uint bottomRow);

            uint dataStartRow = topRow + 1; // fejléc utáni sor
            uint dataEndRow = bottomRow;

            if (dataEndRow < dataStartRow)
                return; // nincs adat

            // 1) Kategória–érték párok kigyűjtése a sheetből
            var pontok = new List<(string Kat, string Ertek)>();

            for (uint r = dataStartRow; r <= dataEndRow; r++)
            {
                var catCell = GetCell(sheetData, topCol, r);
                var valCell = GetCell(sheetData, bottomCol, r);

                var catText = GetCellText(catCell, wbPart);
                var valText = GetCellText(valCell, wbPart);

                if (!string.IsNullOrWhiteSpace(catText) &&
                    !string.IsNullOrWhiteSpace(valText))
                {
                    pontok.Add((catText.Trim(), valText.Trim()));
                }
            }

            if (pontok.Count == 0)
                return; // nincs értelmes adat → nincs diagram

            // 2) DrawingsPart + <drawing> elem a munkalaphoz
            DrawingsPart drawingsPart;
            if (wsPart.DrawingsPart == null)
            {
                drawingsPart = wsPart.AddNewPart<DrawingsPart>();
                drawingsPart.WorksheetDrawing = new Xdr.WorksheetDrawing();

                string relId = wsPart.GetIdOfPart(drawingsPart);
                var drawing = new Drawing() { Id = relId };

                // A <drawing> a headerFooter / pageSetup / pageMargins után jön
                OpenXmlElement insertAfter =
                    wsPart.Worksheet.Elements<HeaderFooter>().LastOrDefault()
                    ?? wsPart.Worksheet.Elements<PageSetup>().LastOrDefault()
                    ?? wsPart.Worksheet.Elements<PageMargins>().LastOrDefault()
                    ?? (OpenXmlElement)sheetData;

                if (insertAfter != null)
                    wsPart.Worksheet.InsertAfter(drawing, insertAfter);
                else
                    wsPart.Worksheet.Append(drawing);

                wsPart.Worksheet.Save();
            }
            else
            {
                drawingsPart = wsPart.DrawingsPart;
                if (drawingsPart.WorksheetDrawing == null)
                    drawingsPart.WorksheetDrawing = new Xdr.WorksheetDrawing();
            }

            // 3) ChartPart létrehozása, PieChart felépítése literal adatokkal
            var chartPart = drawingsPart.AddNewPart<ChartPart>();

            var chartSpace = new C.ChartSpace();
            chartSpace.Append(new C.EditingLanguage() { Val = "hu-HU" });

            var chart = new C.Chart();

            // Cím: ha van diagramCim, akkor Title, különben AutoTitleDeleted
            if (!string.IsNullOrWhiteSpace(diagramCim))
            {
                var title = new C.Title();

                var chartText = new C.ChartText();
                var richText = new C.RichText();

                richText.Append(new A.BodyProperties());
                richText.Append(new A.ListStyle());

                var para = new A.Paragraph();
                var run = new A.Run();
                run.Append(new A.RunProperties() { Language = "hu-HU" });
                run.Append(new A.Text(diagramCim));
                para.Append(run);

                richText.Append(para);
                chartText.Append(richText);
                title.Append(chartText);

                chart.Append(title);
            }
            else
            {
                chart.Append(new C.AutoTitleDeleted() { Val = true });
            }

            var plotArea = new C.PlotArea();
            plotArea.Append(new C.Layout());

            var pieChart = new C.PieChart();
            pieChart.Append(new C.VaryColors() { Val = true });

            uint idx = 0;
            var series = new C.PieChartSeries();
            series.Append(new C.Index() { Val = idx });
            series.Append(new C.Order() { Val = idx });

            // Feliratok beállítása
            var dLbls = new C.DataLabels(
                new C.ShowLegendKey() { Val = false },
                new C.ShowValue() { Val = false },   
                new C.ShowCategoryName() { Val = true },   
                new C.ShowSeriesName() { Val = false },
                new C.ShowPercent() { Val = true },  
                new C.ShowBubbleSize() { Val = false },
                new C.ShowLeaderLines() { Val = true }
            );
            series.Append(dLbls);

            var stringLit = new C.StringLiteral();
            stringLit.Append(new C.PointCount() { Val = (uint)pontok.Count });
            for (uint i = 0; i < pontok.Count; i++)
            {
                var sp = new C.StringPoint()
                {
                    Index = i,
                    NumericValue = new C.NumericValue(pontok[(int)i].Kat)
                };
                stringLit.Append(sp);
            }
            var cat = new C.CategoryAxisData(stringLit);
            series.Append(cat);

            // Értékek literalban
            var numLit = new C.NumberLiteral();
            numLit.Append(new C.FormatCode("General"));
            numLit.Append(new C.PointCount() { Val = (uint)pontok.Count });
            for (uint i = 0; i < pontok.Count; i++)
            {
                var np = new C.NumericPoint()
                {
                    Index = i,
                    NumericValue = new C.NumericValue(pontok[(int)i].Ertek)
                };
                numLit.Append(np);
            }
            var val = new C.Values(numLit);
            series.Append(val);

            pieChart.Append(series);
            plotArea.Append(pieChart);
            chart.Append(plotArea);
            chart.Append(new C.PlotVisibleOnly() { Val = true });

            chartSpace.Append(chart);
            chartPart.ChartSpace = chartSpace;
            chartPart.ChartSpace.Save();

            // 4) Elhelyezés a munkalapon (TwoCellAnchor) – felsőx/felsőy alapján
            var wsDr = drawingsPart.WorksheetDrawing;
            var twoCellAnchor = new Xdr.TwoCellAnchor();

            // felsőx / felsőy → kb. oszlop/sor index (durva skálázás)
            int colStart = Math.Max(0, felsőx / 100);  // pl. 10 → 0, 600 → 6 körül
            int rowStart = Math.Max(0, felsőy / 25);   // pl. 150 → 6 körül

            // adunk neki egy fix "méretet" (6 oszlop széles, 15 sor magas)
            int colEnd = colStart + 6;
            int rowEnd = rowStart + 15;

            var fromMarker = new Xdr.FromMarker(
                new Xdr.ColumnId(colStart.ToString()),
                new Xdr.ColumnOffset("0"),
                new Xdr.RowId(rowStart.ToString()),
                new Xdr.RowOffset("0")
            );

            var toMarker = new Xdr.ToMarker(
                new Xdr.ColumnId(colEnd.ToString()),
                new Xdr.ColumnOffset("0"),
                new Xdr.RowId(rowEnd.ToString()),
                new Xdr.RowOffset("0")
            );

            twoCellAnchor.Append(fromMarker);
            twoCellAnchor.Append(toMarker);

            uint chartId = 1;
            if (wsDr.Elements<Xdr.TwoCellAnchor>().Any())
                chartId = (uint)(wsDr.Elements<Xdr.TwoCellAnchor>().Count() + 1);

            string chartRelId = drawingsPart.GetIdOfPart(chartPart);

            var graphicFrame = new Xdr.GraphicFrame(
                new Xdr.NonVisualGraphicFrameProperties(
                    new Xdr.NonVisualDrawingProperties()
                    {
                        Id = chartId,
                        Name = "Kördiagram " + chartId
                    },
                    new Xdr.NonVisualGraphicFrameDrawingProperties()
                ),
                new Xdr.Transform(
                    new A.Offset() { X = 0L, Y = 0L },
                    new A.Extents() { Cx = 0L, Cy = 0L }
                ),
                new A.Graphic(
                    new A.GraphicData(
                        new C.ChartReference() { Id = chartRelId }
                    )
                    {
                        Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart"
                    }
                )
            );

            twoCellAnchor.Append(graphicFrame);
            twoCellAnchor.Append(new Xdr.ClientData());

            wsDr.Append(twoCellAnchor);
            wsDr.Save();
        }

        private static void AlkalmazDiagramokat(string fájlnév, List<Beállítás_Diagram> diagramok)
        {
            using (var document = SpreadsheetDocument.Open(fájlnév, true))
            {
                foreach (var beállítás in diagramok)
                {
                    MyClosedXML_Excel.Diagram(
                        document,
                        beállítás.Munkalap,
                        beállítás.FelsőX,
                        beállítás.FelsőY,
                        beállítás.TáblaFelső,
                        beállítás.TáblaAlsó,
                        beállítás.DiagramCim);
                }
            }
        }
    }
}
