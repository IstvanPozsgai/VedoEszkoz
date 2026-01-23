using DocumentFormat.OpenXml.Drawing;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using static System.IO.File;
using DT = System.Data;
using MyExcel = Microsoft.Office.Interop.Excel;


namespace VédőEszköz
{
    public static partial class Module_Excel
    {
        public static int sor;
        public static int oszlop;
        public static MyExcel.Application xlApp;
        public static MyExcel.Workbook xlWorkBook;
        public static MyExcel.Worksheet xlWorkSheet;
        public static MyExcel._Workbook _xlWorkBook;
        public static MyExcel._Worksheet _xlWorkSheet;

        readonly static string ExcelUzenet = "Egy Office frissítés miatt keletkező hiba miatt a háttérben megnyílt egy Excel ablak.\nA billentyűzet ALT+TAB gombjainak megnyomásával " +
            "válassza ki az Excel ablakot, majd kattintson a bezárás gombra és indítsa újra a folyamatot!";


        public static object misValue = System.Reflection.Missing.Value;



        /// <summary>
        /// Elindítjuk az Excel készítést egy üres munkafüzettel
        /// </summary>
        public static void ExcelLétrehozás(bool teszt = false)
        {
            try
            {
                //elindítjuk az alkalmazást. létrehozzuk a fájlt és a munkalapot.
                xlApp = new MyExcel.Application
                {
                    Visible = teszt
                };
                xlApp.DisplayAlerts = false;
                // xlApp.Interactive = false;  Nem szabad bekapcsolni mert akkor nem működik kivétel HRESULT-értéke: 0x800AC472 dob.
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (MyExcel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelLétrehozás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Excel táblát elmentjük a megadott néven
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="fájlnév"></param>
        public static void ExcelMentés(string fájlnév)
        {
            try
            {
                xlApp.DisplayAlerts = false;
                xlWorkBook.SaveAs(fájlnév, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMentés(fájlnév {fájlnév}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Excel mentés ugyan azon a néven.
        /// </summary>
        public static void ExcelMentés()
        {
            try
            {
                xlApp.DisplayAlerts = false;
                xlWorkBook.Save();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMentés \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Betűméretet lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="méret">egész</param>
        public static void Betű(string mit, int méret)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Font.Size = méret;
            Táblaterület.Font.Strikethrough = false;
            Táblaterület.Font.Superscript = false;
            Táblaterület.Font.Subscript = false;
            Táblaterület.Font.OutlineFont = false;
            Táblaterület.Font.Shadow = false;
            Táblaterület.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone;
        }

        public static void Betű(string mit, Color színe)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Font.Color = színe;
            //     Táblaterület.Font.ThemeColor = színe;
            //Táblaterület.Font.TintAndShade = 0;
        }


        /// <summary>
        /// Betű formátumát lehet állítani
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="aláhúzott"></param>
        /// <param name="dőlt"></param>
        /// <param name="vastag"></param>
        public static void Betű(string mit, bool aláhúzott, bool dőlt, bool vastag)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Font.Underline = aláhúzott;
            Táblaterület.Font.Italic = dőlt;
            Táblaterület.Font.Bold = vastag;
        }

        /// <summary>
        /// A Betű stílusából egyet lehet kiválasztani, annak a formátum maszkját kell elküldeni.
        /// üres string ha nem akarjuk kihasználni.
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="stílus"></param>
        /// <param name="formátum"></param>
        public static void Betű(string mit, string stílus = "", string formátum = "")
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            if (stílus.Trim() != "")
                Táblaterület.Style = stílus;
            if (formátum.Trim() != "")
                Táblaterület.NumberFormat = formátum;
        }
        public static void ExcelBezárás()
        {
            try
            {
                if (xlWorkBook != null)
                {
                    xlWorkBook.Close(SaveChanges: false); // vagy true, ha kell
                    Marshal.ReleaseComObject(xlWorkBook);
                    xlWorkBook = null;
                }

                if (xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                }

                // Ha van munkalap referenciád, azt is szabadítsd fel
                if (xlWorkSheet != null)
                {
                    Marshal.ReleaseComObject(xlWorkSheet);
                    xlWorkSheet = null;
                }

                // Kritikus: GC hívások
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelBezárás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        /// <summary>
        /// Kiírja a szöveget a megfelelő cellába
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="hova">szöveg</param>
        public static void Kiir(string mit, string hova)
        {
            try
            {
                MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[hova];
                Cella.Value = mit;

                Marshal.ReleaseComObject(Cella);
                Cella = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kiir(mit {mit}, hova {hova})\n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        ///  A szöveg helyzetét lehet meghatározni a cellában bal és jobb a kötött név minden egyéb középre kerül.
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">bal/jobb/közép</param>
        public static void Igazít_vízszintes(string mit, string irány)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                switch (irány)
                {
                    case "bal":
                        Táblaterület.HorizontalAlignment = Constants.xlLeft;
                        break;
                    case "jobb":
                        Táblaterület.HorizontalAlignment = Constants.xlRight;
                        break;
                    default:
                        Táblaterület.HorizontalAlignment = Constants.xlCenter;
                        break;
                }
                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Igazít_vízszintes(mit {mit}, irány {irány}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// szöveg függőleges helyzetét lehet megadni
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">felső/alsó/közép</param>
        public static void Igazít_függőleges(string mit, string irány)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                switch (irány)
                {
                    case "felső":
                        Táblaterület.VerticalAlignment = Constants.xlTop;
                        break;
                    case "alsó":
                        Táblaterület.VerticalAlignment = Constants.xlBottom;
                        break;
                    default:
                        Táblaterület.VerticalAlignment = Constants.xlCenter;
                        break;
                }
                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Igazít_függőleges(mit {mit}, irány {irány}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Egyesíti a kiválasztott területet
        /// </summary>
        /// <param name="mit">szöveg</param>
        public static void Egyesít(string munkalap, string mit)
        {
            try
            {
                MyExcel.Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Range Táblaterület = Munkalap.Range[mit];
                Táblaterület.Merge();
                Táblaterület.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                Táblaterület.VerticalAlignment = XlVAlign.xlVAlignCenter;
                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Egyesít(munkalap {munkalap}, mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Háttérszín beállítása
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="szín">int</param>
        public static void Háttérszín(string mit, int szín)
        {
            if (szín < 0 || szín > 16777215)
            {
                // Ha érvénytelen, akkor fehér színnel hívjuk meg a másikat
                Háttérszín(mit, Color.White);
            }
            else
            {
                // Konvertáljuk az int-et Color-á, majd hívjuk a másik túlterhelést
                Color color = Color.FromArgb(
                    (szín >> 16) & 0xFF, // R
                    (szín >> 8) & 0xFF,  // G
                    szín & 0xFF          // B
                );
                Háttérszín(mit, color);
            }
        }


        public static void Háttérszín(string mit, Color színe)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                Táblaterület.Interior.Color = ColorTranslator.ToOle(színe);
                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Háttérszín(mit {mit}, színe {színe}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static void CellaNincsHáttér(string mit)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

                // 1. Kitöltési minta eltávolítása
                Táblaterület.Interior.Pattern = Constants.xlNone;

                // 2. Szín visszaállítása alapértelmezettre („nincs szín”)
                // Az Excelben ez a -4142 (xlColorIndexNone) érték
                Táblaterület.Interior.ColorIndex = -4142;

                // A TintAndShade és PatternTintAndShade általában nem szükséges,
                // ha Pattern = xlNone, de meghagyhatod biztonságból.
                Táblaterület.Interior.TintAndShade = 0;
                Táblaterület.Interior.PatternTintAndShade = 0;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"CellaNincsHáttér(mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Sötét háttérhez világos betűt állít be
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="szín">dupla</param>
        public static void Háttérszíninverz(string mit, int szín)
        {
            if (szín < 0 || szín > 16777215)
            {
                // Ha érvénytelen, akkor fehér színnel hívjuk meg a másikat
                Háttérszín(mit, Color.White);
            }
            else
            {
                // Konvertáljuk az int-et Color-á, majd hívjuk a másik túlterhelést
                Color color = Color.FromArgb(
                    (szín >> 16) & 0xFF, // R
                    (szín >> 8) & 0xFF,  // G
                    szín & 0xFF          // B
                );
                Háttérszín(mit, color);
            }
        }


        public static void Háttérszíninverz(string mit, Color színe)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

                // Háttérszín beállítása (OLE_COLOR formátumban)
                Táblaterület.Interior.Color = System.Drawing.ColorTranslator.ToOle(színe);

                // Betűszín: FEHÉR (közvetlenül, nem ThemeColor!)
                Táblaterület.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);

                // Opcionális: eltávolítjuk a ThemeColor és TintAndShade hatásokat
                Táblaterület.Font.ThemeColor = 0; // vagy: nincs értelme, ha Color-t használunk
                Táblaterület.Font.TintAndShade = 0;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Háttérszíninverz(mit {mit}, színe {színe}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// A munkalapot, úgy mozgatja, hogy a kívánt cella rajta legyen a képernyőn.
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        public static void Aktív_Cella(string munkalap, string mit)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Module_Excel.xlWorkBook.Activate();
                Munkalap.Activate(); // Activate() stabilabb, mint Select()
                MyExcel.Range Táblaterület = Munkalap.get_Range(mit, mit);
                Táblaterület.Select();

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Aktív_Cella(munkalap {munkalap}, mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// A cellába beírt szöveg olvasási irányát lehet beállítani
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="mit">cella helyzete</param>
        /// <param name="mennyit">-90 bal- 0 vízszintes- 90 jobb</param>
        public static void SzövegIrány(string munkalap, string mit, double mennyit)
        {
            try
            {
                Worksheet Munkalap = (Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Munkalap.get_Range(mit, Type.Missing);
                Táblaterület.Orientation = mennyit;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"SzövegIrány(munkalap {munkalap}, mit {mit}, mennyit {mennyit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// A cella tartalmát sortöréssel több sorba írja
        /// </summary>
        /// <param name="mit"></param>
        /// 
        public static void Sortörésseltöbbsorba(string mit)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                // Vízszintes igazítás: Általános
                Táblaterület.HorizontalAlignment = MyExcel.XlHAlign.xlHAlignGeneral; // vagy -4133
                // Függőleges igazítás: Középre
                Táblaterület.VerticalAlignment = MyExcel.XlVAlign.xlVAlignCenter;    // vagy -4108
                // Sortörés BE
                Táblaterület.WrapText = true;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sortörésseltöbbsorba(mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static void Sortörésseltöbbsorba(string mit, bool egyesített)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                Táblaterület.HorizontalAlignment = MyExcel.XlHAlign.xlHAlignGeneral; // vagy -4133
                Táblaterület.VerticalAlignment = MyExcel.XlVAlign.xlVAlignCenter;    // vagy -4108

                Táblaterület.WrapText = true;
                Táblaterület.Orientation = 0;
                Táblaterület.AddIndent = false;
                Táblaterület.IndentLevel = 0;
                Táblaterület.ShrinkToFit = false;
                Táblaterület.MergeCells = egyesített;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sortörésseltöbbsorba(mit {mit}, egyesített {egyesített}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static void Sortörésseltöbbsorba_egyesített(string mit)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                Táblaterület.HorizontalAlignment = MyExcel.XlHAlign.xlHAlignGeneral; // vagy -4133
                Táblaterület.VerticalAlignment = MyExcel.XlVAlign.xlVAlignCenter;    // vagy -4108
                Táblaterület.WrapText = true;
                Táblaterület.Orientation = 0;
                Táblaterület.AddIndent = false;
                Táblaterület.IndentLevel = 0;
                Táblaterület.ShrinkToFit = false;
                Táblaterület.MergeCells = true;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sortörésseltöbbsorba(mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Megnyitja az excel, html lapot
        /// </summary>
        /// <param name="Fájlhelye"></param>
        public static void Megnyitás(string Fájlhelye)
        {
            try
            {
                if (!Exists(Fájlhelye)) return;
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = Fájlhelye,
                    UseShellExecute = true, // Fontos: ezzel a rendszer alapértelmezett alkalmazását használja
                    Verb = "open"           // Explicit "megnyitás" parancs
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Megnyitás(Fájlhelye {Fájlhelye}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static void ExcelMegnyitás(string hely, bool látszik = false)
        {
            try
            {
                xlApp = new MyExcel.Application();
                xlApp.Visible = látszik;
                xlApp.DisplayAlerts = false; // Fontos: letiltja a figyelmeztetéseket (pl. "fájl foglalt")

                xlWorkBook = xlApp.Workbooks.Open(
                    Filename: hely,
                    ReadOnly: false,           // Vagy true, ha csak olvasol
                    UpdateLinks: false,        // Ne frissítsen hivatkozásokat
                    Editable: true,
                    Notify: false              // Ne jelenjen meg üzenet, ha a fájl foglalt
                );
            }
            catch (Exception ex)
            {
                // Hibakezelés és takarítás
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMegnyitás(hely: {hely}, látszik: {látszik}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(
                    $"Nem sikerült megnyitni az Excel-fájlt:\n{hely}\n\nHiba: {ex.Message}",
                    "Hiba az Excel megnyitásakor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                ExcelBezárásÚJ();
            }
        }


        public static void ExcelBezárásÚJ()
        {
            try
            {
                if (xlWorkBook != null)
                {
                    xlWorkBook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                    xlWorkBook = null;
                }

                if (xlApp != null)
                {
                    xlApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelBezárás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                // Ne dobjon kivételt – csak logol
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        public static void Értékmásol(string munkalap, string honnan, string hova)
        {
            try
            {
                // JAVÍTANDÓ:
                //Eredeti
                //Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                //MyExcel.Range területhonnan = Munkalap.Range[honnan];
                //MyExcel.Range területhova = Munkalap.Range[hova];

                //területhonnan.Select();
                //területhonnan.Copy();
                //területhova.Select();
                //területhova.PasteSpecial(Paste: XlPasteType.xlPasteValues, Operation: XlPasteSpecialOperation.xlPasteSpecialOperationNone, SkipBlanks: false, Transpose: false);

                // Közvetlen érték másolása – NINCS vágólap, NINCS Select!
                Worksheet Munkalap = (Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range forrás = Munkalap.Range[honnan];
                MyExcel.Range cél = Munkalap.Range[hova];
                cél.Value = forrás.Value;

                Marshal.ReleaseComObject(forrás);
                forrás = null;
                Marshal.ReleaseComObject(cél);
                cél = null;

                //Kipróbálható
                //Worksheet Munkalap = (Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                //MyExcel.Range forrás = Munkalap.Range[honnan];
                //MyExcel.Range cél = Munkalap.Range[hova];
                //forrás.Copy(); // Nincs szükség kijelölésre
                //cél.PasteSpecial(
                //    Paste: MyExcel.XlPasteType.xlPasteValues,
                //    Operation: MyExcel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                //    SkipBlanks: false,
                //    Transpose: false
                //);
                //// Vágólap törlése (opcionális, de ajánlott)
                //Module_Excel.xlApp.CutCopyMode = false;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Értékmásol(munkalap {munkalap}, honnan {honnan}, hova {hova}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static void Képlet_másol(string munkalap, string honnan, string hova)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range területhonnan = Munkalap.Range[honnan];
                MyExcel.Range területhova = Munkalap.Range[hova];
                területhonnan.Copy();
                területhova.PasteSpecial(Paste: XlPasteType.xlPasteFormulas);

                Marshal.ReleaseComObject(területhonnan);
                területhonnan = null;
                Marshal.ReleaseComObject(területhova);
                területhova = null;

            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Képlet_másol(munkalap {munkalap}, honnan {honnan}, hova {hova}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static string Beolvas(string honnan)
        {
            string válasz = "_";
            try
            {
                Range Táblaterület = Module_Excel.xlApp.Application.Range[honnan];
                if (Táblaterület.Value != null) válasz = Táblaterület.Value.ToStrTrim();

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Beolvas(honnan {honnan}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return válasz;
        }


        public static void Kicsinyít(string mit)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                Táblaterület.ShrinkToFit = true;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kicsinyít(mit: {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // JAVÍTANDÓ:       
        public static DateTime BeolvasDátum(string honnan)
        {
            DateTime válasz = new DateTime(1900, 1, 1);
            try
            {
                //Eredeti
                //MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];
                //if (Cella.Value == null)
                //{
                //    válasz = new DateTime(1900, 1, 1);
                //}
                //else if (!int.TryParse(Cella.Value.ToString(), out int result))
                //{
                //    válasz = Convert.ToDateTime(Cella.Value);
                //}
                //else
                //{
                //    válasz = Convert.ToDateTime(Cella.Value);
                //}

                Range cella = Module_Excel.xlWorkSheet.Range[honnan];
                object érték = cella.Value;

                if (érték == null || érték == DBNull.Value) return válasz;

                if (érték is double szám) return DateTime.FromOADate(szám);

                return Convert.ToDateTime(érték);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"BeolvasDátum(honnan: {honnan}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return válasz;
        }

        // JAVÍTANDÓ:
        public static DateTime Beolvasidő(string honnan)
        {
            DateTime válasz = new DateTime(1900, 1, 1, 0, 0, 0);
            try
            {
                MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];

                if (Cella.Value == null)
                {
                    válasz = new DateTime(1900, 1, 1, 0, 0, 0);
                }
                else if (decimal.TryParse(Cella.Value.ToString(), out decimal ideig))
                {
                    int óra, perc, másodperc;
                    decimal órad, percd, másodpercd;

                    órad = ideig * 24;
                    óra = ((int)órad);
                    órad = órad - Convert.ToDecimal(óra);

                    percd = órad * 60;
                    perc = (int)percd;
                    percd = percd - Convert.ToDecimal(perc);

                    másodpercd = percd * 60;
                    másodperc = (int)másodpercd;

                    válasz = new DateTime(1900, 1, 1, óra, perc, másodperc);
                }
                else if (Cella.Value.ToString().Contains(":"))
                {
                    string[] darab = Cella.Value.ToString().Split(':');
                    int óra = int.Parse(darab[0]);
                    int perc = int.Parse(darab[1]);
                    int másodperc;
                    if (darab.Length > 2)
                        másodperc = int.Parse(darab[2]);
                    else
                        másodperc = 0;

                    válasz = new DateTime(1900, 1, 1, óra, perc, másodperc);
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Beolvasidő(honnan {honnan}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return válasz;
        }


        /// <summary>
        ///  Kép_beillesztés
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        /// <param name="hely"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Magas"></param>
        /// <param name="Széles"></param>
        public static void Kép_beillesztés(string munkalap, String mit, string hely, int X, int Y, int Magas, int Széles)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                xlWorkSheet.Shapes.AddPicture(hely, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, X, Y, Széles, Magas);

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kép_beillesztés(munkalap {munkalap}, mit {mit}, hely {hely}, X {X}, Y {Y}, Magas {Magas}, Széles {Széles}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        /// <param name="hova">cella hivatkozás</param>
        /// <param name="hivatkozottlap">munkalap neve amire mutat</param>
        public static void Link_beillesztés(String munkalap, string hova, string hivatkozottlap)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Munkalap.Range[hova];
                Táblaterület.Hyperlinks.Add(Anchor: Táblaterület, Address: "", SubAddress: "'" + hivatkozottlap + "'!A1", TextToDisplay: "'" + hivatkozottlap + "'");

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Link_beillesztés(munkalap {munkalap}, hova {hova}, hivatkozottlap {hivatkozottlap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // JAVÍTANDÓ:
        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap_adat">Az a munkalap melyen vannak az adatok</param>
        /// <param name="balfelső">A kimutatás alaptáblájának bal felső cellaneve</param>
        /// <param name="jobbalsó">>A kimutatás alaptáblájának jobb alsó cellaneve</param>
        /// <param name="kimutatás_Munkalap">Az a munkalap amelyikre tesszük a kimutatást</param>
        /// <param name="Kimutatás_cella">Kimutatás bal felső cellája</param>
        /// <param name="Kimutatás_név">Kimutatás neve</param>
        /// <param name="összesítNév">Azon adatok listája amit összesítünk</param>
        /// <param name="sorNév">Sorokban szereplő értékek listája</param>
        /// <param name="oszlopNév">Oszlopokban szereplő értékek listája</param>
        /// <param name="SzűrőNév">Szűrő Nevek listája</param>
        /// <param name="SzűrőÉrték">Szűrt értékek</param>
        public static void Kimutatás_Fő(string munkalap_adat, string balfelső, string jobbalsó, string kimutatás_Munkalap, string Kimutatás_cella, string Kimutatás_név
            , List<string> összesítNév, List<string> sorNév, List<string> oszlopNév, List<string> SzűrőNév)
        {
            try
            {
                MyExcel.Worksheet Adatok_lap = (Worksheet)xlWorkBook.Worksheets[munkalap_adat];
                MyExcel.Worksheet Kimutatás_lap = (Worksheet)xlWorkBook.Worksheets[kimutatás_Munkalap];

                MyExcel.Range AdatRange = Adatok_lap.Range[balfelső, jobbalsó];

                PivotCaches pivotCaches = xlWorkBook.PivotCaches();
                MyExcel.Range pivotData = Adatok_lap.Range[balfelső, jobbalsó];

                MyExcel.PivotCache pivotCache = pivotCaches.Create(XlPivotTableSourceType.xlDatabase, pivotData);
                MyExcel.PivotTable pivotTable = pivotCache.CreatePivotTable(Kimutatás_lap.Range[Kimutatás_cella], Kimutatás_név);

                //Táblázatban megjelenő érték
                if (összesítNév.Count > 0)
                {
                    for (int i = 0; i < összesítNév.Count; i++)
                    {

                        PivotField salesField = (PivotField)pivotTable.PivotFields(összesítNév[i]);
                        salesField.Orientation = XlPivotFieldOrientation.xlDataField;
                        salesField.Function = XlConsolidationFunction.xlSum;
                        salesField.Name = összesítNév[i] + " db";
                    }
                }
                //Sor adatok
                if (sorNév.Count > 0)
                {
                    for (int i = 0; i < sorNév.Count; i++)
                    {
                        PivotField colorsRowsField = (PivotField)pivotTable.PivotFields(sorNév[i]);
                        colorsRowsField.Orientation = XlPivotFieldOrientation.xlRowField;
                    }
                }

                //oszlopok 
                if (oszlopNév.Count > 0)
                {
                    for (int i = 0; i < oszlopNév.Count; i++)
                    {
                        PivotField regionField = (PivotField)pivotTable.PivotFields(oszlopNév[i]);
                        regionField.Orientation = XlPivotFieldOrientation.xlColumnField;
                    }
                }

                //Szűrő mezők
                if (SzűrőNév.Count > 0)
                {
                    for (int i = 0; i < SzűrőNév.Count; i++)
                    {
                        PivotField datefield = (PivotField)pivotTable.PivotFields(SzűrőNév[i]);
                        datefield.Orientation = XlPivotFieldOrientation.xlPageField;
                        datefield.EnableMultiplePageItems = true;
                    }
                }

                Marshal.ReleaseComObject(pivotData);
                pivotData = null;
                Marshal.ReleaseComObject(AdatRange);
                AdatRange = null;



            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, " \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        // JAVÍTANDÓ:
        public static void Kimutatás_Fő(string munkalap_adat, string balfelső, string jobbalsó, string kimutatás_Munkalap, string Kimutatás_cella, string Kimutatás_név,
            List<string> összesítNév, List<string> Összesítés_módja, List<string> sorNév, List<string> oszlopNév, List<string> SzűrőNév)
        {
            try
            {
                MyExcel.Worksheet Adatok_lap = (Worksheet)xlWorkBook.Worksheets[munkalap_adat];
                MyExcel.Worksheet Kimutatás_lap = (Worksheet)xlWorkBook.Worksheets[kimutatás_Munkalap];

                MyExcel.Range AdatRange = Adatok_lap.Range[balfelső, jobbalsó];

                PivotCaches pivotCaches = xlWorkBook.PivotCaches();
                MyExcel.Range pivotData = Adatok_lap.Range[balfelső, jobbalsó];

                MyExcel.PivotCache pivotCache = pivotCaches.Create(XlPivotTableSourceType.xlDatabase, pivotData);
                MyExcel.PivotTable pivotTable = pivotCache.CreatePivotTable(Kimutatás_lap.Range[Kimutatás_cella], Kimutatás_név);

                //Táblázatban megjelenő érték
                if (összesítNév.Count > 0)
                {
                    for (int i = 0; i < összesítNév.Count; i++)
                    {

                        PivotField salesField = (PivotField)pivotTable.PivotFields(összesítNév[i]);
                        salesField.Orientation = XlPivotFieldOrientation.xlDataField;
                        switch (Összesítés_módja[i])
                        {

                            case "xlSum":
                                salesField.Function = XlConsolidationFunction.xlSum;
                                salesField.Name = összesítNév[i] + " db";
                                break;

                            case "xlCount":
                                salesField.Function = XlConsolidationFunction.xlCount;
                                salesField.Name = összesítNév[i] + " Összeg";
                                break;

                            default:
                                break;
                        }


                    }
                }
                //oszlopok 
                if (oszlopNév.Count > 0)
                {
                    for (int i = 0; i < oszlopNév.Count; i++)
                    {
                        PivotField regionField = (PivotField)pivotTable.PivotFields(oszlopNév[i]);
                        regionField.Orientation = XlPivotFieldOrientation.xlColumnField;
                        regionField.Position = i + 1;
                    }
                }

                //Sor adatok
                if (sorNév.Count > 0)
                {
                    for (int i = 0; i < sorNév.Count; i++)
                    {
                        PivotField colorsRowsField = (PivotField)pivotTable.PivotFields(sorNév[i]);
                        colorsRowsField.Orientation = XlPivotFieldOrientation.xlRowField;
                    }
                }

                //Szűrő mezők
                if (SzűrőNév.Count > 0)
                {
                    for (int i = 0; i < SzűrőNév.Count; i++)
                    {
                        PivotField datefield = (PivotField)pivotTable.PivotFields(SzűrőNév[i]);
                        datefield.Orientation = XlPivotFieldOrientation.xlPageField;
                        datefield.EnableMultiplePageItems = true;
                    }
                }

                Marshal.ReleaseComObject(AdatRange);
                AdatRange = null;
                Marshal.ReleaseComObject(pivotData);
                pivotData = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, " \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ExcelUzenet, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static void Diagram(string munkalap, int felsőx, int felsőy, int alsóx, int alsóy, string táblafelső, string táblaalsó)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

            MyExcel.Range crange;
            MyExcel.ChartObjects cb = (MyExcel.ChartObjects)Munkalap.ChartObjects(Type.Missing);
            MyExcel.ChartObject cbc = (MyExcel.ChartObject)cb.Add(felsőx, felsőy, alsóx, alsóy);
            MyExcel.Chart cp = cbc.Chart;

            crange = Munkalap.get_Range(táblafelső, táblaalsó);
            cp.SetSourceData(crange, misValue);
            cp.ChartType = MyExcel.XlChartType.xlPie;
            cp.ApplyLayout(1);

            Marshal.ReleaseComObject(crange);
            crange = null;
        }

        //Elkopó
        public static long Munkalap(DT.DataTable Tábla, int sor, string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            //Fejléc
            for (int j = 0; j < Tábla.Columns.Count; j++)
            {
                Munkalap.Cells[sor, j + 1] = Tábla.Columns[j].ColumnName.ToString();
            }


            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                for (int j = 0; j < Tábla.Columns.Count; j++)
                {
                    Munkalap.Cells[i + sor + 1, j + 1] = Tábla.Rows[i].ItemArray[j];
                }
            }

            long utolsó_sor = Tábla.Rows.Count;
            return utolsó_sor;
        }

        public static int Tábla_Író(string hely, string jelszó, string szöveg, int sor, string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            DataGridView dataGridView1 = new DataGridView();
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(szöveg, Kapcsolat);

                DataSet Tábla = new DataSet();

                Adapter.Fill(Tábla);
                //Fejléc
                for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                {
                    Munkalap.Cells[sor, j + 1] = Tábla.Tables[0].Columns[j].ColumnName.ToString();
                }


                for (int i = 0; i < Tábla.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                    {
                        Munkalap.Cells[i + sor + 1, j + 1] = Tábla.Tables[0].Rows[i].ItemArray[j];
                    }
                }

                int utolsó_sor = Tábla.Tables[0].Rows.Count;
                return utolsó_sor;
            }

        }
        /// <summary>
        /// A táblázatot rögzíti a beállított sornak megfelelően
        /// </summary>
        /// <param name="sor">sor</param>
        public static void Tábla_Rögzítés(int sor)
        {
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = sor;
            xlApp.ActiveWindow.FreezePanes = true;
        }
        public static void Szűrés(string munkalap, int oszloptól, int oszlopig, int sor)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range myRange = Munkalap.Range[Oszlopnév(oszloptól) + ":" + Oszlopnév(oszlopig)];
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            object result = myRange.AutoFilter(sor);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }


        public static void Szűrés(string munkalap, string oszloptól, string oszlopig, int sor)
        {

            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range myRange = Munkalap.Range[oszloptól + ":" + oszlopig];
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            object result = myRange.AutoFilter(sor);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }

        public static void Szűrés(string munkalap, string mit, int sor)
        {

            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range myRange = Munkalap.Range[mit];
            object result = myRange.AutoFilter(sor);
        }
        /// <summary>
        /// Kijelölt területet rácsoz
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="Kijelöltterület">szöveg kijelölt terület A1:V4 formában</param>
        public static void Rácsoz(string Kijelöltterület)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[Kijelöltterület];
            Táblaterület.Borders.LineStyle = XlLineStyle.xlContinuous;
            Táblaterület.Borders.Weight = MyExcel.XlBorderWeight.xlThin;
            Táblaterület.BorderAround(MyExcel.XlLineStyle.xlContinuous,
                                        MyExcel.XlBorderWeight.xlMedium,
                                        MyExcel.XlColorIndex.xlColorIndexAutomatic,
                                        MyExcel.XlColorIndex.xlColorIndexAutomatic);
        }
    }
}
