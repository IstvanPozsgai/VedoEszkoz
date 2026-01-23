using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        /// <summary>
        /// Megadott oszlop szélesség beállítása az oszlopnál
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="oszlop">string oszlopnév</param>
        /// <param name="szélesség">double szélesség, ha nincs megadva akkor automatikus</param>

        public static void Oszlopszélesség(string munkalapNév, string oszlopTartomány, double szélesség = -1)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapNév);

                // Ellenőrizzük, hogy tartomány-e (tartalmaz-e ":")
                if (oszlopTartomány.Contains(':'))
                {
                    // Példa: "A:K" → szétválasztjuk
                    string[] részek = oszlopTartomány.Split(':');
                    if (részek.Length != 2) throw new ArgumentException($"Érvénytelen oszloptartomány: {oszlopTartomány}");

                    string kezdőOszlop = részek[0].Trim();
                    string végOszlop = részek[1].Trim();

                    // Konvertáljuk oszlopszámokká
                    int kezdőIndex = XLHelper.GetColumnNumberFromLetter(kezdőOszlop);
                    int végIndex = XLHelper.GetColumnNumberFromLetter(végOszlop);

                    if (kezdőIndex <= 0 || végIndex <= 0 || kezdőIndex > végIndex) throw new ArgumentException($"Érvénytelen oszloptartomány: {oszlopTartomány}");

                    // Végigmegyünk az oszlopokon
                    for (int i = kezdőIndex; i <= végIndex; i++)
                    {
                        OszlopotÁllít(i, munkalap, szélesség);
                    }
                }
                else
                {
                    // Egyetlen oszlop
                    int kezdőIndex = XLHelper.GetColumnNumberFromLetter(oszlopTartomány);
                    OszlopotÁllít(kezdőIndex, munkalap, szélesség);
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Oszlopszélesség(munkalap: {munkalapNév}, oszlop: {oszlopTartomány}, szélesség: {szélesség}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void OszlopotÁllít(int i, IXLWorksheet munkalap, double szélesség = -1)
        {
            if (szélesség > 0)
                munkalap.Column(i).Width = szélesség;
            else
            {
                IXLColumn oszlop = munkalap.Column(i);
                oszlop.AdjustToContents();
                if (oszlop.Width > 80) oszlop.Width = 80;
            }

        }

        /// <summary>
        /// Elrejti az oszlopot
        /// </summary>
        /// <param name="oszlop"></param>
        public static void OszlopRejtés(string munkalapNév, string oszlop)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapNév);
                if (oszlop.Contains(':')) oszlop = oszlop.Split(':')[0].Trim();
                if (munkalap == null) throw new ArgumentException($"A(z) '{munkalapNév}' nevű munkalap nem található.");
                munkalap.Column(oszlop).Hide();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"OszlopRejtés(munkalap {munkalapNév}, oszlop {oszlop}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Háttérszín(string munkalapnév, string mit, System.Drawing.Color színe)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange tartomány = munkalap.Range(mit);
                tartomány.Style.Fill.BackgroundColor = XLColor.FromColor(színe);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Háttérszín(mit {mit}, színe {színe}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Sortörésseltöbbsorba(string munkalapnév, string mit, bool egyesített = false)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange range = munkalap.Range(mit);
                if (egyesített) range.Merge();
                range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General;
                range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                range.Style.Alignment.WrapText = true;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sortörésseltöbbsorba(mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ex.Message, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalapnév"></param>
        /// <param name="mit"></param>
        /// <param name="mekkora">0 esetén automatikus, -1 esetén a program számolja ki</param>
        public static void Sormagasság(string munkalapnév, string mit, int mekkora, string BetűNév = "Arial", float BetűMéret = 12)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);

                // Tartomány lekérése – pl. "A5" vagy "B2:D10"
                IXLRange tartomány = munkalap.Range(mit);

                // Az érintett sorok számai
                var érintettSorok = Enumerable.Range(tartomány.FirstRow().RowNumber(),
                                                       tartomány.RowCount()).ToList();
                if (mekkora > 0)
                {
                    // Fix sor magasság beállítása (pontban – Excel-ben 1 egység = 1 pont)
                    foreach (int sor in érintettSorok)
                    {
                        munkalap.Row(sor).Height = mekkora;
                    }
                }
                else if (mekkora == -1)
                {
                    // Mepróbáljuk kiszámolni
                    foreach (int sor in érintettSorok)
                    {
                        munkalap.Row(sor).Height = SormagasságSzámítás(munkalap, sor, BetűNév, BetűMéret);
                    }
                }
                else
                {
                    // ⚠️ ClosedXML NEM támogatja natívan az AutoFit sor magasságot!
                    // De van mód arra, hogy "jó közelítéssel" automatikusan méretezzünk:
                    // 1. Távolítsuk el a fix magasságot (állítsuk "alapértelmezett" értékre)
                    // 2. Bekapcsoljuk a WrapText-et (ha többsoros szöveg van)
                    // 3. Megbízunk abban, hogy az Excel majd AutoFit-tel nyitja meg
                    foreach (int sor in érintettSorok)
                    {
                        // Alapértelmezett sor magasság visszaállítása (~15 pont)
                        munkalap.Row(sor).Height = 15.0;
                    }
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sormagasság(mit: {mit}, mekkora: {mekkora}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Kiszámolja a megadott sorhoz szükséges magasságot a cellák tartalma, betűtípusa, betűmérete és oszlopszélessége alapján.
        /// Nem módosítja a munkafüzetet – csak visszaadja a javasolt sor magasságot Excel egységben.
        /// </summary>
        /// <param name="worksheet">A munkalap, amely tartalmazza a sort</param>
        /// <param name="Sor">A sor indexe (1-től indul)</param>
        /// <param name="BetűNév">Alapértelmezett betűcsalád, ha a cellában nincs megadva</param>
        /// <param name="BetűMéret">Alapértelmezett betűméret (pontban), ha a cellában nincs megadva</param>
        /// <returns>Javasolt sor magasság Excel egységben (pontban)</returns>
        public static double SormagasságSzámítás(IXLWorksheet worksheet, int Sor, string BetűNév, float BetűMéret)
        {
            const double AlapMagasság = 15.0;
            if (Sor < 1) throw new ArgumentOutOfRangeException(nameof(Sor), "A sor indexe 1-től indul.");

            IXLRow row = worksheet.Row(Sor);
            var cellsUsed = row.CellsUsed();

            if (!cellsUsed.Any()) return AlapMagasság;

            int maxSorok = 1;

            // Opcionális: gyorsító szótár merged cellákhoz (nem feltétlenül szükséges kis munkafüzeteknél)
            var mergedRangeLookup = new Dictionary<(int Row, int Col), IXLRange>();
            foreach (var range in worksheet.MergedRanges)
            {
                for (int r = range.RangeAddress.FirstAddress.RowNumber; r <= range.RangeAddress.LastAddress.RowNumber; r++)
                {
                    for (int c = range.RangeAddress.FirstAddress.ColumnNumber; c <= range.RangeAddress.LastAddress.ColumnNumber; c++)
                    {
                        mergedRangeLookup[(r, c)] = range;
                    }
                }
            }

            using (var dummyBitmap = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(dummyBitmap))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                foreach (IXLCell cell in cellsUsed)
                {
                    string text = "";
                    if (cell != null) text = cell.Value.ToStrTrim();
                    if (string.IsNullOrEmpty(text)) continue;

                    // === Egyesített cellák kezelése – tényleges szélesség meghatározása ===
                    double effectiveColumnWidth = 0.0;

                    if (cell.IsMerged() && mergedRangeLookup.TryGetValue((cell.Address.RowNumber, cell.Address.ColumnNumber), out var mergedRange))
                    {
                        // Összegezzük az összes oszlop szélességét a merged tartományban
                        for (int col = mergedRange.RangeAddress.FirstAddress.ColumnNumber;
                                 col <= mergedRange.RangeAddress.LastAddress.ColumnNumber;
                                 col++)
                        {
                            double colWidth = worksheet.Column(col).Width;
                            if (colWidth > 0) // elrejtett oszlopokat kihagyjuk
                                effectiveColumnWidth += colWidth;
                        }
                    }
                    else
                    {
                        // Nem merged, vagy nem találtuk meg – csak saját oszlop
                        effectiveColumnWidth = worksheet.Column(cell.Address.ColumnNumber).Width;
                    }

                    if (effectiveColumnWidth <= 0) continue; // elrejtett oszlop

                    float maxWidthPx = (float)(effectiveColumnWidth * 7.0); // karakter-egység → pixel

                    // Betűtípus és méret lekérése
                    string fontFamily = !string.IsNullOrEmpty(cell.Style.Font.FontName)
                        ? cell.Style.Font.FontName
                        : BetűNév;

                    float fontSizePt = cell.Style.Font.FontSize > 0
                        ? (float)cell.Style.Font.FontSize
                        : BetűMéret;

                    // Biztonsági ellenőrzés: érvényes font?
                    if (fontSizePt <= 0) fontSizePt = BetűMéret;

                    try
                    {
                        using (Font font = new Font(fontFamily, fontSizePt, FontStyle.Regular, GraphicsUnit.Point))
                        {
                            string[] manualLines = text.Replace("\r", "").Split('\n');
                            int sorokSzama = 0;

                            foreach (string manualLine in manualLines)
                            {
                                if (string.IsNullOrEmpty(manualLine))
                                {
                                    sorokSzama++;
                                    continue;
                                }

                                // Mérés korlátozott szélességgel
                                SizeF layoutSize = new SizeF(maxWidthPx, float.MaxValue);
                                SizeF measured = g.MeasureString(manualLine, font, layoutSize, StringFormat.GenericTypographic);

                                // Egy sor magassága (font-magasság alapján)
                                float lineHeight = g.MeasureString("X", font, new SizeF(float.MaxValue, float.MaxValue), StringFormat.GenericTypographic).Height;

                                int linesForThis = (int)Math.Ceiling(measured.Height / lineHeight);
                                sorokSzama += Math.Max(1, linesForThis);
                            }

                            if (sorokSzama > maxSorok) maxSorok = sorokSzama;
                        }
                    }
                    catch (ArgumentException)
                    {
                        // Érvénytelen betűtípus – alapértelmezett használata
                        // (pl. ha a font nincs telepítve)
                        // Opcionális: logolás, vagy figyelmen kívül hagyás
                        continue;
                    }
                }
            }
            return maxSorok * AlapMagasság;
        }


        /// <summary>
        /// A cellába beírt szöveg olvasási irányát lehet beállítani
        /// A ClosedXML csak 0 - 180-at kezel értelmesen
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="mit">cella helyzete</param>
        /// <param name="mennyit">-90 bal- 0 vízszintes- 90 jobb</param>
        public static void SzövegIrány(string munkalapNév, string mit, int fok)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapNév);

                IXLRange range = munkalap.Range(mit); // pl. "A1" vagy "A1:B5"

                // Beállítás közvetlenül számként
                range.Style.Alignment.TextRotation = (ushort)fok;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"SzövegIrány(munkalap {munkalapNév}, mit: {mit}, fok: {fok}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public static int Utolsósor(string munkalapnév)
        {
            int maxRow = 0;
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                var utolsóSor = munkalap.LastRowUsed();
                maxRow = utolsóSor?.RowNumber() ?? 0;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Utolsósor(munkalap {munkalapnév}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maxRow;
        }

        public static int Utolsóoszlop(string munkalapnév)
        {
            int maxColumn = 0;
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);

                // Opció 1: legnagyobb oszlopszám (ajánlott legtöbb esetben)
                maxColumn = munkalap.LastColumnUsed()?.ColumnNumber() ?? 0;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Utolsósor(munkalap {munkalapnév}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maxColumn;
        }

        public static void OszlopTörlés(string munkalapnév, string oszlopBetű)
        {
            try
            {
                // 1. Oszlop tartalmának teljes törlése (érték + formázás)
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLColumn oszlop = munkalap.Column(oszlopBetű);
                oszlop.Clear(XLClearOptions.All); // ez törli: érték, formázás, szélesség, stb.

                // 2. Oszlopszélesség visszaállítása automatikusra (azaz nincs fix szélesség)
                // A Clear() már ezt is megteszi, de biztos ami biztos:
                oszlop.Width = 0; // 0 = automatikus szélesség (ClosedXML-ben ez a standard)
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"SzövegIrány(munkalap {munkalapnév}, oszlopBetű: {oszlopBetű}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        /// <summary>
        /// Munkalapon a jelölt sor elé beszúr meghatározott számú üres sort.
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="sor">a sorszám ahova kell beszúrni (ez a sor lejjebb csúszik)</param>
        /// <param name="beszúrás">beszúrandó sorok száma</param>
        public static void SorBeszúrás(string munkalap, int sor, int beszúrás)
        {
            try
            {
                IXLWorksheet worksheet = xlWorkBook.Worksheet(munkalap);

                if (sor <= 0) throw new ArgumentOutOfRangeException(nameof(sor), "A sorindexnek 1-nél nagyobbnak kell lennie.");
                if (beszúrás <= 0) return;

                worksheet.Row(sor).InsertRowsAbove(beszúrás);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"SorBeszúrás(munkalap: {munkalap}, sor: {sor}, db: {beszúrás}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
