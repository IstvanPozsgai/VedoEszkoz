using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace VédőEszköz
{
    public static partial class MyClosedXML_Excel
    {

        public static string Beolvas(string munkalapnév, string honnan)
        {
            string válasz = "_";
            //try
            //{
            //    IXLWorksheet lap =  Workbook.Worksheet(munkalapnév);
            //    válasz = lap.Cell(honnan).Value.ToStrTrim();
            //    if (válasz.Trim() == "") válasz = "_";
            //}
            //catch (Exception ex)
            //{
            //    StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
            //    string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
            //    HibaNapló.Log(ex.Message, $"Beolvas(honnan {honnan}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
            //    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            return válasz;
        }

        /// <summary>
        /// CSak adott munkalapon belül másol
        /// </summary>
        /// <param name="munkalapnév"></param>
        /// <param name="honnan">kijelölt táblázat</param>
        /// <param name="hova">a bal felső cella ahova akarunk másolni</param>
        public static void Értékmásol(string munkalapnév, string honnan, string hova)
        {
            try
            {
                IXLWorksheet lap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange forrás = lap.Range(honnan);
                IXLCell célKezdo = lap.Cell(hova); // Működik akár "D1", akár "D1:E2" – de csak az első cellát használjuk

                int startRow = célKezdo.Address.RowNumber;
                int startCol = célKezdo.Address.ColumnNumber;

                int forrásElsoSor = forrás.FirstRow().RowNumber();
                int forrásElsoOszlop = forrás.FirstColumn().ColumnNumber();

                foreach (var forrásCella in forrás.Cells())
                {
                    int sorEltolás = forrásCella.Address.RowNumber - forrásElsoSor;
                    int oszlopEltolás = forrásCella.Address.ColumnNumber - forrásElsoOszlop;

                    lap.Cell(startRow + sorEltolás, startCol + oszlopEltolás)
                        .Value = forrásCella.Value; // Csak érték – képlet nem másolódik
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Értékmásol(munkalap {munkalapnév}, honnan {honnan}, hova {hova}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DateTime Beolvasidő(string munkalapnév, string honnan)
        {
            DateTime válasz = new DateTime(1900, 1, 1, 0, 0, 0);
            try
            {

                IXLWorksheet lap = xlWorkBook.Worksheet(munkalapnév);
                string Cella = lap.Cell(honnan).Value.ToStrTrim();


                if (Cella == "")
                {
                    válasz = new DateTime(1900, 1, 1, 0, 0, 0);
                }
                else if (decimal.TryParse(Cella, out decimal ideig))
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
                else if (Cella.Contains(":"))
                {
                    string[] darab = Cella.Split(':');
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
                    MessageBox.Show(ex.Message, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return válasz;
        }


        public static DateTime BeolvasDátum(string munkalapnév, string honnan)
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
                IXLWorksheet lap = xlWorkBook.Worksheet(munkalapnév);
                string Cella = lap.Cell(honnan).Value.ToStrTrim();


                if (Cella == null) return válasz;

                //        if (Cella is double szám) return DateTime.FromOADate(szám);

                return Convert.ToDateTime(Cella);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"BeolvasDátum(honnan: {honnan}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ex.Message, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return válasz;
        }

    }
}
