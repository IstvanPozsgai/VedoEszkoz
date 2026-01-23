using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using MyF = Függvénygyűjtemény;

namespace VédőEszköz
{
    public static partial class MyClosedXML_Excel
    {
        //Táblaműveleteket gyűjti


        /// <summary>
        /// Ez a változat közvetlenül Adattáblából írja ki az adatokat
        /// és az ArrayToExcel könyvtárat használja
        /// </summary>
        /// <param name="Tábla"></param>
        /// <param name="fájl"></param>
        /// <param name="Elemek"></param>
        /// 

        public static void DataTableToXML(string fájl, DataTable Tábla, string munkalapnév = "Munka1", DataGridView TáblaGrid = null)
        {
            try
            {
                byte[] excel = Tábla.ToExcel(a => a.SheetName(munkalapnév));
                File.WriteAllBytes(fájl, excel);

                ///Megnyitjuk az Excel Formázásra
                ExcelMegnyitás(fájl);

                ////Utolsó oszlop és sor adatok
                oszlop = Tábla.Columns.Count;
                sor = Tábla.Rows.Count;

                Háttérszín(munkalapnév, $"A1:{MyF.Oszlopnév(oszlop)}1", Color.Yellow); //Sárga háttér
                Beállítás_Betű BeBetű = new Beállítás_Betű
                {
                    Vastag = true
                };
                Betű(munkalapnév, $"A1:{MyF.Oszlopnév(oszlop)}1", BeBetű);


                Rácsoz(munkalapnév, $"A1:{MyF.Oszlopnév(oszlop)}{sor + 1}"); // rácsozás
                Oszlopszélesség(munkalapnév, $"A:{MyF.Oszlopnév(oszlop)}");     //Automata Oszlop szélesség beállítás

                if (TáblaGrid != null) Színezés(munkalapnév, TáblaGrid);

                Tábla_Rögzítés(munkalapnév, 1);  //Rögzítjük a fejlécet
                Szűrés(munkalapnév, "A", MyF.Oszlopnév(oszlop), sor + 1);    //szűrést felteszük

                //Nyomtatási terület kijelülése
                Beállítás_Nyomtatás NyBeállítás = new Beállítás_Nyomtatás
                {
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(oszlop)}{sor + 1}",
                    Munkalap = munkalapnév,
                    IsmétlődőSorok = "$1:$1",
                    Álló = false,
                    LapSzéles = 1,
                    LáblécKözép = "&P/&N",
                    FejlécKözép = Program.PostásNév?.Trim() ?? "",
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm")
                };
                NyomtatásiTerület_részletes(munkalapnév, NyBeállítás);

                ExcelMentés(fájl);
                ExcelBezárás();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"DataTableToExcel(fájl: {fájl})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// DataGridView adatai alapján Excel táblát készít
        /// </summary>
        /// <param name="fájl">Mentési név</param>
        /// <param name="Tábla">DataGridView tábla</param>
        /// <param name="színes">DataGridView tábla színezését át akarjuk vinni az Excel táblába</param>
        public static void DataGridViewToXML(string fájl, DataGridView Tábla, string munkalapnév = "Munka1", bool színes = false)
        {
            try
            {
                DataTable ÚjTábla = new DataTable();
                foreach (DataGridViewColumn oszlop in Tábla.Columns)
                {
                    if (oszlop.Visible)
                    {
                        ÚjTábla.Columns.Add(oszlop.HeaderText);
                    }
                }
                foreach (DataGridViewRow sor in Tábla.Rows)
                {
                    DataRow ÚjSor = ÚjTábla.NewRow();
                    for (int i = 0; i < Tábla.Columns.Count; i++)
                    {
                        if (Tábla.Columns[i].Visible)
                        {
                            ÚjSor[i] = sor.Cells[i].Value;
                        }
                    }
                    ÚjTábla.Rows.Add(ÚjSor);
                }
                if (színes)
                    DataTableToXML(fájl, ÚjTábla, munkalapnév, Tábla);
                else
                    DataTableToXML(fájl, ÚjTábla, munkalapnév);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "DataGridViewToExcel", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Színezés(string munkalapnév, DataGridView táblaDat)
        {
            try
            {
                int sor = 2;        //Első sor sárga 
                int oszlop = 1;
                for (int i = 0; i < táblaDat.Rows.Count; i++)
                {
                    for (int j = 0; j < táblaDat.Columns.Count; j++)
                    {
                        DataGridViewCell cella = táblaDat.Rows[i].Cells[j];
                        Color háttér = cella.Style.BackColor;

                        // Ha a háttérszín nem definiált (pl. "0" névvel), akkor fehérnek vesszük
                        if (háttér.Name == "0" || háttér.IsEmpty)
                            háttér = Color.White;

                        // Csak akkor alkalmazzuk a színt, ha nem fehér
                        if (háttér != Color.White)
                        {
                            string mit = $"{MyF.Oszlopnév(oszlop + j)}{sor + i}";
                            Háttérszín(munkalapnév, mit, háttér);
                        }
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Színezés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
