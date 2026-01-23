using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MyExcel = Microsoft.Office.Interop.Excel;


namespace VédőEszköz
{
    public static partial class Module_Excel
    {
        /// <summary>
        /// Munkalapon a jelölt sor elé beszúr meghatározott sorokat.
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="sor">a sorszám ahova kell beszúrni</param>
        /// <param name="beszúrás">beszúrandó sorok száma</param>
        public static void SorBeszúrás(string munkalap, int sor, int beszúrás)
        {
            try
            {
                MyExcel.Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

                // Ellenőrizzük, hogy a munkalap nem null-e
                if (Munkalap == null) throw new ArgumentNullException(nameof(Munkalap), "A munkalap nem lehet null.");

                // Ellenőrizzük, hogy a sorindex érvényes-e
                if (sor <= 0) throw new ArgumentOutOfRangeException(nameof(sor), "A sorindexnek 1-nél nagyobbnak kell lennie.");

                // Beszúrunk egy sort az adott pozícióba
                for (int i = 0; i < beszúrás; i++)
                {
                    MyExcel.Range Táblaterület = (MyExcel.Range)Munkalap.Rows[sor];
                    Táblaterület.Insert(MyExcel.XlInsertShiftDirection.xlShiftDown);

                    Marshal.ReleaseComObject(Táblaterület);
                    Táblaterület = null;
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"SorBeszúrás(munkalap: {munkalap}, munkalap: {sor}, munkalap: {beszúrás}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Sormagasságot lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="mekkora">egész, ha -1 akkor automatikus sormagasságot akarunk beállítani</param>
        /// 
        public static void Sormagasság(string mit, int mekkora)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(mit);

                if (mekkora > 0)
                    Táblaterület.RowHeight = mekkora;
                else
                    Táblaterület.EntireRow.AutoFit();

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
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
        /// Megadott oszlop szélesség beállítása az oszlopnál
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="oszlop">string oszlopnév</param>
        /// <param name="szélesség">double szélesség, ha nincs megadva akkor automatikus</param>
        public static void Oszlopszélesség(string munkalap, string oszlop, double szélesség = -1)
        {
            try
            {
                //Oszlop szélesség beállítás
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Munkalap.Range[oszlop];
                if (szélesség > 0)
                    Táblaterület.Columns.ColumnWidth = szélesség;
                else
                    Táblaterület.Columns.EntireColumn.AutoFit();

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Oszlopszélesség(munkalap: {munkalap}, oszlop: {oszlop}, szélesség: {szélesség} \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Törli az oszlopot
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="oszlop">formában kell megadni "A:A" </param>
        public static void OszlopTörlés(string oszlop)
        {
            try
            {
                MyExcel.Range Táblaterület = xlWorkSheet.Range[oszlop];
                Táblaterület.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlToLeft);

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"OszlopTörlés(oszlop {oszlop}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Elrejti az oszlopot
        /// </summary>
        /// <param name="oszlop"></param>
        public static void OszlopRejtés(string munkalap, string oszlop)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Munkalap.Range[oszlop];
                Táblaterület.EntireColumn.Hidden = true;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"OszlopRejtés(munkalap {munkalap}, oszlop {oszlop}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Oszlop sorszámát átalakítja az oszlop jelölő betűvé 
        /// </summary>
        /// <param name="sorszám">Int adunnk át</param>
        /// <returns></returns>
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


        public static int Utolsósor(string munkalap)
        {
            int maxRow = 0;
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Munkalap.UsedRange;
                maxRow = Táblaterület.Rows.Count;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Utolsósor(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maxRow;
        }


        public static int Utolsóoszlop(string munkalap)
        {
            int maxColumn = 0;
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Munkalap.UsedRange;
                maxColumn = Táblaterület.Columns.Count;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Utolsósor(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maxColumn;
        }



    }
}
