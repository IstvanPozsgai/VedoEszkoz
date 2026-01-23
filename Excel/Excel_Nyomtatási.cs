using Microsoft.Office.Interop.Excel;
using System;
using System.Windows.Forms;
using MyExcel = Microsoft.Office.Interop.Excel;


namespace VédőEszköz
{
    public static partial class Module_Excel
    {


        /// <summary>
        /// Nyomtatási területet 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"> "" vagy "$1:$1"</param>
        /// /// <param name="oszlopisnétlődés">"" vagy "$A:$B"</param>
        /// <param name="álló">Álló esetén true, fekvó esetén false</param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés, bool álló)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = "";
                    Táblaterület.CenterHeader = Program.PostásNév.Trim();
                    Táblaterület.RightHeader = DateTime.Now.ToString("yyyy.MM.dd hh:mm");
                    Táblaterület.LeftFooter = "";
                    Táblaterület.CenterFooter = @"&P/&N";
                    Táblaterület.RightFooter = "";
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(0.590551181102362d);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(0.590551181102362d);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(0.78740157480315d);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(0.78740157480315d);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(0.511811023622047d);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(0.511811023622047d);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = false;
                    Táblaterület.CenterVertically = false;
                    if (álló)
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    else
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlLandscape;

                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    Táblaterület.FitToPagesWide = 1;
                    Táblaterület.FitToPagesTall = false;
                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
            string fejbal, string fejközép, string fejjobb, string fénykép)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.LeftHeaderPicture.Filename = fénykép;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = fejbal;
                    Táblaterület.CenterHeader = fejközép;
                    Táblaterület.RightHeader = fejjobb;
                    Táblaterület.LeftFooter = "";
                    Táblaterület.CenterFooter = @"&P/&N";
                    Táblaterület.RightFooter = "";
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(0.590551181102362d);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(0.590551181102362d);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(0.78740157480315d);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(0.78740157480315d);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(0.511811023622047d);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(0.511811023622047d);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = false;
                    Táblaterület.CenterVertically = false;
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    Táblaterület.FitToPagesWide = 1;
                    Táblaterület.FitToPagesTall = false;
                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }




            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="oszlopisnétlődés"></param>
        /// <param name="fejbal"></param>
        /// <param name="fejközép"></param>
        /// <param name="fejjobb"></param>
        /// <param name="lábbal"></param>
        /// <param name="lábközép"></param>
        /// <param name="lábjobb"></param>
        /// <param name="fénykép"></param>
        /// <param name="balMargó"></param>
        /// <param name="jobbMargó"></param>
        /// <param name="alsóMargó"></param>
        /// <param name="felsőMargó"></param>
        /// <param name="fejlécMéret"></param>
        /// <param name="LáblécMéret"></param>
        /// <param name="vízszintes"></param>
        /// <param name="függőleges"></param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                        string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, double balMargó, double jobbMargó,
                        double alsóMargó, double felsőMargó, double fejlécMéret, double LáblécMéret, bool vízszintes, bool függőleges)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.LeftHeaderPicture.Filename = fénykép;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = fejbal;
                    Táblaterület.CenterHeader = fejközép;
                    Táblaterület.RightHeader = fejjobb;
                    Táblaterület.LeftFooter = lábbal;
                    Táblaterület.CenterFooter = lábközép;
                    Táblaterület.RightFooter = lábjobb;
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = vízszintes;
                    Táblaterület.CenterVertically = függőleges;
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    Táblaterület.FitToPagesWide = 1;
                    Táblaterület.FitToPagesTall = false;
                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap">Munkalap neve</param>
        /// <param name="terület"> "" vagy "$1:$1"</param>
        /// <param name="balMargó">LeftMargin 0.708661417322835d formátumban</param>
        /// <param name="jobbMargó">RightMargin 0.708661417322835d formátumban</param>
        /// <param name="alsóMargó">BottomMargin 0.708661417322835d formátumban</param>
        /// <param name="felsőMargó">TopMargin 0.708661417322835d formátumban</param>
        /// <param name="fejlécMéret">HeaderMargin 0.708661417322835d formátumban</param>
        /// <param name="LáblécMéret">FooterMargin 0.708661417322835d formátumban</param>
        /// <param name="oldalszéles">"szám" vagy false</param>
        /// <param name="oldalmagas">"szám" vagy false</param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
            double balMargó = 0.708661417322835d,
            double jobbMargó = 0.708661417322835d,
            double alsóMargó = 0.590551181102362d,
            double felsőMargó = 0.748031496062992d,
            double fejlécMéret = 0.31496062992126d,
            double LáblécMéret = 0.31496062992126d,
            string oldalszéles = "1",
            string oldalmagas = "1")
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    if (int.TryParse(oldalszéles, out int széles))
                        Táblaterület.FitToPagesWide = széles;
                    else
                        Táblaterület.FitToPagesWide = false;
                    if (int.TryParse(oldalmagas, out int magas))
                        Táblaterület.FitToPagesTall = magas;
                    else
                        Táblaterület.FitToPagesTall = false;

                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }




            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                    string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, double balMargó, double jobbMargó,
                    double alsóMargó, double felsőMargó, double fejlécMéret, double LáblécMéret, bool vízszintes, bool függőleges, string Elrendezés)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.LeftHeaderPicture.Filename = fénykép;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = fejbal;
                    Táblaterület.CenterHeader = fejközép;
                    Táblaterület.RightHeader = fejjobb;
                    Táblaterület.LeftFooter = lábbal;
                    Táblaterület.CenterFooter = lábközép;
                    Táblaterület.RightFooter = lábjobb;
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = vízszintes;
                    Táblaterület.CenterVertically = függőleges;
                    if (Elrendezés != "Álló")
                        Táblaterület.Orientation = XlPageOrientation.xlLandscape;
                    else
                        Táblaterület.Orientation = XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    Táblaterület.FitToPagesWide = 1;
                    Táblaterület.FitToPagesTall = false;
                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap   ">munkalap   </param>
        /// <param name="terület    ">terület    </param>
        /// <param name="balMargó   ">balMargó   </param>
        /// <param name="jobbMargó  ">jobbMargó  </param>
        /// <param name="alsóMargó  ">alsóMargó  </param>
        /// <param name="felsőMargó ">felsőMargó </param>
        /// <param name="fejlécMéret">fejlécMéret</param>
        /// <param name="LáblécMéret">LáblécMéret</param>
        /// <param name="oldalszéles">oldalszéles</param>
        /// <param name="oldalmagas ">oldalmagas </param>
        /// <param name="álló       ">álló       </param>
        /// <param name="papírméret ">papírméret </param>
        /// <param name="víz_közép  ">víz_közép  </param>
        /// <param name="Függ_közép ">Függ_közép </param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
            double balMargó, double jobbMargó,
            double alsóMargó, double felsőMargó,
            double fejlécMéret, double LáblécMéret,
            string oldalszéles = "1", string oldalmagas = "1",
            bool álló = true, string papírméret = "A4",
            bool víz_közép = true, bool Függ_közép = true)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;

                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    if (álló)
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    else
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlLandscape;

                    Táblaterület.Draft = false;

                    Táblaterület.CenterHorizontally = víz_közép;
                    Táblaterület.CenterVertically = Függ_közép;

                    if (papírméret == "A4")
                        Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    else
                        Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA3;

                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    if (int.TryParse(oldalszéles, out int széles))
                        Táblaterület.FitToPagesWide = széles;
                    else
                        Táblaterület.FitToPagesWide = false;
                    if (int.TryParse(oldalmagas, out int magas))
                        Táblaterület.FitToPagesTall = magas;
                    else
                        Táblaterület.FitToPagesTall = false;

                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }




            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Teljes változat
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="oszlopisnétlődés"></param>
        /// <param name="fejbal"></param>
        /// <param name="fejközép"></param>
        /// <param name="fejjobb"></param>
        /// <param name="lábbal"></param>
        /// <param name="lábközép"></param>
        /// <param name="lábjobb"></param>
        /// <param name="fénykép"></param>
        /// <param name="balMargó"></param>
        /// <param name="jobbMargó"></param>
        /// <param name="alsóMargó"></param>
        /// <param name="felsőMargó"></param>
        /// <param name="fejlécMéret"></param>
        /// <param name="LáblécMéret"></param>
        /// <param name="vízszintes"></param>
        /// <param name="függőleges"></param>
        /// <param name="oldalszéles"></param>
        /// <param name="oldalmagas"></param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, double balMargó, double jobbMargó,
                double alsóMargó, double felsőMargó, double fejlécMéret, double LáblécMéret, bool vízszintes, bool függőleges, string oldalszéles, string oldalmagas,
                bool álló = true, string papírméret = "A4")
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.LeftHeaderPicture.Filename = fénykép;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = fejbal;
                    Táblaterület.CenterHeader = fejközép;
                    Táblaterület.RightHeader = fejjobb;
                    Táblaterület.LeftFooter = lábbal;
                    Táblaterület.CenterFooter = lábközép;
                    Táblaterület.RightFooter = lábjobb;
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = vízszintes;
                    Táblaterület.CenterVertically = függőleges;
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    if (int.TryParse(oldalszéles, out int széles))
                        Táblaterület.FitToPagesWide = széles;
                    else
                        Táblaterület.FitToPagesWide = false;

                    if (int.TryParse(oldalmagas, out int magas))
                        Táblaterület.FitToPagesTall = magas;
                    else
                        Táblaterület.FitToPagesTall = false;

                    if (álló)
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    else
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlLandscape;

                    if (papírméret == "A4")
                        Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    else
                        Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA3;
                }

                Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
