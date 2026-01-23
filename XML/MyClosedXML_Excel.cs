using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        private static XLWorkbook xlWorkBook;
        private static IXLWorksheet xlWorkSheet;
        private static readonly Dictionary<string, int> FagyasztandóSorok = new Dictionary<string, int>();
        private static readonly Dictionary<string, Beállítás_Nyomtatás> NyomtatásiBeállítások = new Dictionary<string, Beállítás_Nyomtatás>();
        private static readonly List<Beállítás_Ferde> FerdeVonalak = new List<Beállítás_Ferde>();
        private static readonly List<Beállítás_CellaSzöveg> CellaBeállítás = new List<Beállítás_CellaSzöveg>();
        private static readonly List<Beállítás_Diagram> DiagramBeállítások = new List<Beállítás_Diagram>();

        public static int sor;
        public static int oszlop;


        private const double MmToInch = 1.0 / 25.4;

        /// <summary>
        /// Ha az Excel fájlt kell megnyitni, kell használni ezt a függvényt,
        /// hogy adatokat lehessen írni, vagy olvasni a fájlból
        /// Ha csak meg akarunk nyitni valamit, akkor használjuk a Process.Start()-ot amit a Függvényeknél találunk
        /// </summary>
        /// <param name="hely"></param>
        public static void ExcelMegnyitás(string hely)
        {
            try
            {
                xlWorkBook = new XLWorkbook(hely);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMegnyitás(hely: {hely}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(
                    $"Nem sikerült megnyitni az Excel-fájlt:\n{hely}\n\nHiba: {ex.Message}",
                    "Hiba az Excel megnyitásakor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                // Bezárás: most már csak a munkafüzetet kell felszabadítani
                ExcelBezárás();
            }
        }

        public static void ExcelMentésMásként(string fájlnév)
        {
            try
            {
                if (string.IsNullOrEmpty(fájlnév))
                {
                    throw new InvalidOperationException("A munkafüzet fájlútvonala nem lett beállítva. Használjon ExcelMentésMásként() vagy állítson be fájlnevet.");
                }
                xlWorkBook.SaveAs(fájlnév);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMentés \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void ExcelMentés(string fájlnév)
        {
            try
            {
                if (string.IsNullOrEmpty(fájlnév))
                {
                    throw new InvalidOperationException("A munkafüzet fájlútvonala nem lett beállítva. Használjon ExcelMentésMásként() vagy állítson be fájlnevet.");
                }

                xlWorkBook.SaveAs(fájlnév);
                xlWorkBook.Dispose();

                if (DiagramBeállítások.Count > 0)
                {
                    AlkalmazDiagramokat(fájlnév, DiagramBeállítások);
                    DiagramBeállítások.Clear();
                }

                // Utólagos OpenXml módosítás – CSAK ha van fagyasztás
                if (FagyasztandóSorok.Count > 0)
                {
                    AlkalmazFagyasztást(fájlnév, FagyasztandóSorok);
                    FagyasztandóSorok.Clear(); // nem kötelező, de tiszta állapot
                }

                // Utólaf OpenXml segítségével behúzzuk a ferde vonalat.
                if (FerdeVonalak.Count > 0)
                {
                    AlkalmazFerdeVonalak(fájlnév, FerdeVonalak);
                    FerdeVonalak.Clear();
                }

                // Ha van nyomtatási beállítás, alkalmazzuk OpenXml-mel
                if (NyomtatásiBeállítások.Count > 0)
                {
                    AlkalmazNyomtatásiBeállításokat(fájlnév, NyomtatásiBeállítások);
                    NyomtatásiBeállítások.Clear(); // Opcionális
                }

                if (CellaBeállítás.Count > 0)
                {
                    AlkalmazCellaFormázás(fájlnév, CellaBeállítás);
                    CellaBeállítás.Clear();
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMentés \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExcelBezárás()
        {
            try
            {
                // Munkafüzet eldobása – a ClosedXML nem igényel explicit "Close"-t,
                // de ha volt korábbi mentés, azt már el kellett végezni.
                xlWorkBook?.Dispose(); // Dispose felszabadítja a belső erőforrásokat (pl. stream-eket)
                xlWorkBook = null;
                xlWorkSheet = null;

                // Nincs szükség GC.Collect() vagy Marshal.ReleaseComObject() hívásokra
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelBezárás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExcelLétrehozás(string munkalap = "Munka1")
        {
            try
            {
                // Üres munkafüzet létrehozása memóriában
                xlWorkBook = new XLWorkbook();
                xlWorkSheet = xlWorkBook.Worksheets.Add(munkalap); // Alapértelmezett lapnév
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelLétrehozás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// DatagridView tartalmát elmenti Excel fájlba
        /// </summary>
        /// <param name="InitialDirectory">Fájl elérési út</param>
        /// <param name="Title">Ablak felirat</param>
        /// <param name="FileName">Fájlnév</param>
        /// <param name="Filter">Szűrő</param>
        /// <param name="Tábla">DatagridView táblanév</param>
        public static void Mentés(string InitialDirectory, string Title, string FileName, string Filter, DataGridView Tábla)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = InitialDirectory,
                    Title = Title,
                    FileName = FileName,
                    Filter = Filter
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                DataGridViewToXML(fájlexc, Tábla);
                MessageBox.Show($"Elkészült az Excel tábla:\n{fájlexc}", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Mentés {InitialDirectory},{Title},{FileName},{Filter}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
