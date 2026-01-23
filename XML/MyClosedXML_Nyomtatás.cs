using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using A = DocumentFormat.OpenXml.Drawing;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;


namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        public static void NyomtatásiTerület_részletes(string munkalap, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                NyomtatásiBeállítások[munkalap] = beállítás;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AlkalmazNyomtatásiBeállításokat(string fájlnév, Dictionary<string, Beállítás_Nyomtatás> beállítások)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fájlnév, true))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                List<Sheet> sheets = workbookPart.Workbook.Sheets?.Elements<Sheet>().ToList();

                if (sheets == null) return;

                for (int i = 0; i < sheets.Count; i++)
                {
                    Sheet sheet = sheets[i];
                    string lapNév = sheet?.Name?.Value;

                    if (string.IsNullOrEmpty(lapNév) || !beállítások.TryGetValue(lapNév, out var beállítás)) continue;

                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    Worksheet worksheet = worksheetPart.Worksheet;

                    // 1. PageSetup létrehozása/frissítése
                    PageSetup pageSetup = worksheet.GetFirstChild<PageSetup>();
                    if (pageSetup == null)
                    {
                        pageSetup = new PageSetup();
                        worksheet.Append(pageSetup);
                    }
                    pageSetup.PapírTájolás(beállítás);
                    pageSetup.Papírméret(beállítás);

                    worksheet.OldaltörésBeállítása(beállítás);
                    worksheet.Papírkitöltés(beállítás);


                    // 2.
                    // Létező PageMargins eltávolítása, ha van
                    PageMargins pageMargins = worksheet.GetFirstChild<PageMargins>();
                    if (pageMargins == null)
                    {
                        // Ha nincs, létrehozzuk
                        pageMargins = new PageMargins();
                        worksheet.Append(pageMargins); // vagy InsertAfter<PageMargins>(...), ha fontos a sorrend
                    }
                    pageMargins.MargóBeállítás(beállítás);

                    // 3. Header/Footer
                    HeaderFooter headerFooter = worksheet.GetFirstChild<HeaderFooter>();
                    if (headerFooter == null)
                    {
                        headerFooter = new HeaderFooter();
                        worksheet.Append(headerFooter);
                    }
                    headerFooter.LáblécBeállítás(beállítás);

                    if (!string.IsNullOrEmpty(beállítás.Képútvonal) && File.Exists(beállítás.Képútvonal))
                    {
                        worksheetPart.KépBeállítás(beállítás);
                    }


                    //       4.PrintOptions
                    PrintOptions printOptions = worksheet.GetFirstChild<PrintOptions>();
                    if (printOptions == null)
                    {
                        printOptions = new PrintOptions();
                        worksheet.InsertBefore(printOptions, worksheet.GetFirstChild<PageMargins>());
                    }
                    printOptions.Headings = false;
                    printOptions.GridLines = false;
                    printOptions.VerticalCentered = beállítás.FüggKözép;
                    printOptions.HorizontalCentered = beállítás.VízKözép;

                    workbookPart.SorOszlopIsmétlődés(beállítás, lapNév, i);
                    workbookPart.NyomtatásiTerület(beállítás, lapNév, i);

                    worksheet.Save();

                }
            }
        }

        // ===== Segédfüggvények=====
        private static string NévEllenőr(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            // Ha tartalmaz szóközt, idézőjelet, vagy egyéb speciális karaktert, idézőjelek közé tesszük
            if (name.Contains(' ') || name.Contains('\'') || name.Contains('!') || name.Contains('#') ||
                name.Contains('%') || name.Contains('&') || name.Contains('(') || name.Contains(')') ||
                name.Contains('+') || name.Contains('-') || name.Contains(',') || name.Contains(';') ||
                name.Contains('<') || name.Contains('>') || name.Contains('=') || name.Contains('{') ||
                name.Contains('}') || name.Contains('[') || name.Contains(']') || name.Contains('^') ||
                name.Contains('~') || name.Contains('\\') || name.Contains('|'))
            {
                return "'" + name.Replace("'", "''") + "'";
            }

            return name;
        }

        private static void Papírkitöltés(this Worksheet worksheet, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                if (worksheet == null) throw new ArgumentNullException(nameof(worksheet));

                // 1. PageSetup beállítása (ez a nyomtatási méretezés részletei)
                var pageSetup = worksheet.GetFirstChild<PageSetup>();
                if (pageSetup == null)
                {
                    pageSetup = new PageSetup();
                    worksheet.Append(pageSetup);
                }
                pageSetup.FitToWidth = (UInt32Value)(uint)beállítás.LapSzéles;
                pageSetup.FitToHeight = (UInt32Value)(uint)beállítás.LapMagas;

                // 2. SheetProperties (<sheetPr>) lekérése – ha nem létezik, létrehozzuk
                var sheetPr = worksheet.GetFirstChild<SheetProperties>();
                if (sheetPr == null)
                {
                    sheetPr = new SheetProperties();
                    // A sheetPr MINDIG legyen az első gyermek elem
                    worksheet.InsertAt(sheetPr, 0);
                }

                // 3. PageSetupProperties (<pageSetUpPr>) lekérése vagy létrehozása a sheetPr-ben
                var pageSetupPr = sheetPr.GetFirstChild<PageSetupProperties>();
                if (pageSetupPr == null)
                {
                    pageSetupPr = new PageSetupProperties();
                    sheetPr.Append(pageSetupPr);
                }

                // 4. FitToPage bekapcsolása
                pageSetupPr.FitToPage = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Papírkitöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Papírméret(this PageSetup pageSetup, Beállítás_Nyomtatás beállítás)
        {
            try
            {   // Papírméret: A4 = 9
                if (beállítás.Papírméret.Trim() == "A3")
                    pageSetup.PaperSize = 8;
                else
                    pageSetup.PaperSize = 9;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Papírméret", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PapírTájolás(this PageSetup pageSetup, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                pageSetup.Orientation = beállítás.Álló ? OrientationValues.Portrait : OrientationValues.Landscape;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "PapírTájolás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void OldaltörésBeállítása(this Worksheet worksheet, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                // JAVÍTANDÓ:
                //// Meglévő RowBreaks keresése
                //RowBreaks rowBreaks = worksheet.Descendants<RowBreaks>().FirstOrDefault();

                //if (rowBreaks == null)
                //{
                //    rowBreaks = new RowBreaks();
                //    SheetData sheetData = worksheet.GetFirstChild<SheetData>();
                //    worksheet.InsertAfter(rowBreaks, sheetData);
                //}
                //rowBreaks.Append(new Break { Id = (uint)beállítás.Oldaltörés });
                //rowBreaks.Count = (uint)rowBreaks.ChildElements.Count;
                //rowBreaks.ManualBreakCount = (uint)rowBreaks.ChildElements.Count;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "PapírTájolás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void MargóBeállítás(this PageMargins pageMargins, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                pageMargins.Left = beállítás.BalMargó * MmToInch;
                pageMargins.Right = beállítás.JobbMargó * MmToInch;
                pageMargins.Top = beállítás.FelsőMargó * MmToInch;
                pageMargins.Bottom = beállítás.AlsóMargó * MmToInch;
                pageMargins.Header = beállítás.FejlécMéret * MmToInch;
                pageMargins.Footer = beállítás.LáblécMéret * MmToInch;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "MargóBeállítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void LáblécBeállítás(this HeaderFooter headerFooter, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                headerFooter.AlignWithMargins = true;
                headerFooter.DifferentFirst = false;
                headerFooter.DifferentOddEven = false;



                headerFooter.OddFooter = new OddFooter
                {
                    Text = $"&L{beállítás.LáblécBal}&C{beállítás.LáblécKözép}&R{beállítás.LáblécJobb}"
                };

                if (beállítás.Képútvonal.Trim() != "" || !beállítás.Képútvonal.Contains("&G"))
                {
                    beállítás.FejlécBal += "&G";
                }

                headerFooter.OddHeader = new OddHeader
                {
                    Text = $"&L{beállítás.FejlécBal}&C{beállítás.FejlécKözép}&R{beállítás.FejlécJobb}"
                };
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "LáblécBeállítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SorOszlopIsmétlődés(this WorkbookPart workbookPart, Beállítás_Nyomtatás beállítás, string lapNév, int i)
        {
            try
            {
                if (!string.IsNullOrEmpty(beállítás.IsmétlődőSorok) || !string.IsNullOrEmpty(beállítás.IsmétlődőOszlopok))
                {
                    Workbook workbook = workbookPart.Workbook;
                    DefinedNames definedNames = workbook.DefinedNames;
                    if (definedNames == null)
                    {
                        definedNames = new DefinedNames();
                        workbook.Append(definedNames);
                    }

                    // Eltávolítjuk a meglévő Print_Titles-t erre a munkalapra (index alapján)
                    DefinedName existingPrintTitle = definedNames.OfType<DefinedName>()
                        .FirstOrDefault(dn => dn.Name == "_xlnm.Print_Titles" && dn.LocalSheetId?.Value == (uint)i);
                    existingPrintTitle?.Remove();

                    // Referencia összeállítása
                    string escapedName = NévEllenőr(lapNév);
                    string reference = "";

                    if (!string.IsNullOrEmpty(beállítás.IsmétlődőSorok) && !string.IsNullOrEmpty(beállítás.IsmétlődőOszlopok))
                        reference = $"{escapedName}!{beállítás.IsmétlődőSorok},{escapedName}!{beállítás.IsmétlődőOszlopok}";
                    else if (!string.IsNullOrEmpty(beállítás.IsmétlődőSorok))
                        reference = $"{escapedName}!{beállítás.IsmétlődőSorok}";
                    else if (!string.IsNullOrEmpty(beállítás.IsmétlődőOszlopok))
                        reference = $"{escapedName}!{beállítás.IsmétlődőOszlopok}";

                    definedNames.Append(new DefinedName
                    {
                        Name = "_xlnm.Print_Titles",
                        LocalSheetId = (uint)i,
                        Text = reference
                    });
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "SorOszlopIsmétlődés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void NyomtatásiTerület(this WorkbookPart workbookPart, Beállítás_Nyomtatás beállítás, string lapNév, int i)
        {
            try
            {
                if (!string.IsNullOrEmpty(beállítás.NyomtatásiTerület))
                {
                    DefinedNames definedNames = workbookPart.Workbook.GetFirstChild<DefinedNames>();
                    if (definedNames == null)
                    {
                        definedNames = new DefinedNames();
                        workbookPart.Workbook.InsertAfter(definedNames, workbookPart.Workbook.GetFirstChild<Sheets>());
                    }

                    DefinedName printArea = definedNames.Elements<DefinedName>()
                        .FirstOrDefault(dn => dn.Name?.Value == "_xlnm.Print_Area" && dn.LocalSheetId?.Value == (uint)i);

                    printArea?.Remove();

                    definedNames.Append(new DefinedName
                    {
                        Name = "_xlnm.Print_Area",
                        LocalSheetId = (uint)i,
                        Text = $"{NévEllenőr(lapNév)}!{beállítás.NyomtatásiTerület}",
                        Hidden = true
                    });
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "NyomtatásiTerület", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void KépBeállítás(this WorksheetPart worksheetPart, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                string imagePath = beállítás.Képútvonal;
                if (!File.Exists(imagePath)) return;

                // 1. VmlDrawingPart létrehozása vagy lekérése
                VmlDrawingPart vmlDrawingPart = worksheetPart.VmlDrawingParts.FirstOrDefault();
                if (vmlDrawingPart == null)
                {
                    vmlDrawingPart = worksheetPart.AddNewPart<VmlDrawingPart>();
                }

                // 2. Kép hozzáadása a VmlDrawingPart-hoz
                string imageRelId;
                // Ellenőrizzük, hogy van-e már kép, hogy ne szemeteljük tele, ha többször hívódna meg
                if (vmlDrawingPart.ImageParts.Any())
                {
                    // Ha már van kép, újrahasznosítjuk az elsőt (egyszerűsítés)
                    imageRelId = vmlDrawingPart.GetIdOfPart(vmlDrawingPart.ImageParts.First());
                }
                else
                {
                    ImagePart imagePart = vmlDrawingPart.AddImagePart(ImagePartType.Png);
                    using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        imagePart.FeedData(stream);
                    }
                    imageRelId = vmlDrawingPart.GetIdOfPart(imagePart);
                }

                // 3. VML tartalom generálása és írása
                // Mivel a &G a FejlécBal-ban van, ezért az ID: "LH" (Left Header)
                // Ha változtatnál és máshova tennéd, akkor: CH (Center), RH (Right)
                string vmlContent = GetVmlXmlContent(imageRelId, "LH");

                using (StreamWriter writer = new StreamWriter(vmlDrawingPart.GetStream(FileMode.Create, FileAccess.Write)))
                {
                    writer.Write(vmlContent);
                }

                // 4. A LegacyDrawingHeaderFooter elem létrehozása és HELYES beillesztése
                string vmlPartId = worksheetPart.GetIdOfPart(vmlDrawingPart);

                var worksheet = worksheetPart.Worksheet;
                var legacyDrawingHF = worksheet.Elements<LegacyDrawingHeaderFooter>().FirstOrDefault();

                if (legacyDrawingHF == null)
                {
                    legacyDrawingHF = new LegacyDrawingHeaderFooter();

                    // AZ EXCEL SZIGORÚ SORRENDET KÖVETEL:
                    // Sorrend: PageMargins -> PageSetup -> HeaderFooter -> ... -> Drawing -> LegacyDrawing -> LegacyDrawingHeaderFooter

                    // Megpróbáljuk a HeaderFooter után beszúrni
                    var headerFooter = worksheet.Elements<HeaderFooter>().FirstOrDefault();
                    if (headerFooter != null)
                    {
                        worksheet.InsertAfter(legacyDrawingHF, headerFooter);
                    }
                    else
                    {
                        // Ha nincs HeaderFooter, akkor a PageSetup után
                        var pageSetup = worksheet.Elements<PageSetup>().FirstOrDefault();
                        if (pageSetup != null)
                            worksheet.InsertAfter(legacyDrawingHF, pageSetup);
                        else
                        {
                            // Vészmegoldás: a Worksheet végére, de ez néha kockázatos
                            worksheet.Append(legacyDrawingHF);
                        }
                    }
                }

                legacyDrawingHF.Id = vmlPartId;
                worksheet.Save(); // Biztosítjuk, hogy a változások mentésre kerüljenek
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KépBeállítás_Javított", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetVmlXmlContent(string imageRelId, string shapeId)
        {
            // shapeId: "LH" = Bal fejléc, "CH" = Közép, "RH" = Jobb
            // A méretek (width, height) pontban (pt) vannak megadva. Állítsd be a logódnak megfelelően!
            string width = "125pt";  // Kb 4.4 cm
            string height = "55pt";  // Kb 2 cm

            return
            "<xml xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\">" +
              "<o:shapelayout v:ext=\"edit\">" +
                "<o:idmap v:ext=\"edit\" data=\"1\"/>" +
              "</o:shapelayout>" +
              "<v:shapetype id=\"_x0000_t75\" coordsize=\"21600,21600\" o:spt=\"75\" o:preferrelative=\"t\" path=\"m@4@5l@4@11@9@11@9@5xe\" filled=\"f\" stroked=\"f\">" +
                "<v:stroke joinstyle=\"miter\"/>" +
                "<v:formulas>" +
                  "<v:f eqn=\"if lineDrawn pixelLineWidth 0\"/>" +
                  "<v:f eqn=\"sum @0 1 0\"/>" +
                  "<v:f eqn=\"sum 0 0 @1\"/>" +
                  "<v:f eqn=\"prod @2 1 2\"/>" +
                  "<v:f eqn=\"prod @3 21600 pixelWidth\"/>" +
                  "<v:f eqn=\"prod @3 21600 pixelHeight\"/>" +
                  "<v:f eqn=\"sum @0 0 1\"/>" +
                  "<v:f eqn=\"prod @6 1 2\"/>" +
                  "<v:f eqn=\"prod @7 21600 pixelWidth\"/>" +
                  "<v:f eqn=\"sum @8 21600 0\"/>" +
                  "<v:f eqn=\"prod @7 21600 pixelHeight\"/>" +
                  "<v:f eqn=\"sum @10 21600 0\"/>" +
                "</v:formulas>" +
                "<v:path o:extrusionok=\"f\" gradientshapeok=\"t\" o:connecttype=\"rect\"/>" +
                "<o:lock v:ext=\"edit\" aspectratio=\"t\"/>" +
              "</v:shapetype>" +
              // Az adott kép definíciója
              $"<v:shape id=\"{shapeId}\" o:spid=\"_x0000_s1025\" type=\"#_x0000_t75\" " +
              $"style=\"position:absolute;margin-left:0;margin-top:0;width:{width};height:{height};z-index:1\">" +
                $"<v:imagedata o:relid=\"{imageRelId}\" o:title=\"Logo\"/>" +
                // ClientData fontos lehet az Excelnek, hogy tudja, ez egy kép
                "<x:ClientData ObjectType=\"Pict\">" +
                    "<x:SizeWithCells/>" +
                // "<x:CF>Bitmap</x:CF>" + // Opcionális
                "</x:ClientData>" +
              "</v:shape>" +
            "</xml>";
        }

    }
}