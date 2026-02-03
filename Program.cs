using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Zuby.ADGV;

namespace VédőEszköz
{
    public static class Program
    {
        /// <summary>
        /// Az a telephely ahol működik a program
        /// </summary>
        public static string PostásTelephely = "";
        /// <summary>
        /// Aki bejelentkezett
        /// </summary>
        public static string PostásNév = "";
        /// <summary>
        /// Bejelentkező jogosultsági szövege
        /// </summary>
        public static string PostásJogkör = "";
        /// <summary>
        /// A telephely szakszolgálat-e, van-e alá besorolva más üzem
        /// </summary>
        public static bool Postás_Vezér = false;
        /// <summary>
        /// Melyik csoportban van
        /// </summary>
        public static int Postás_csoport = 0;
        /// <summary>
        /// 
        /// </summary>
        public static bool Postás_telephely = false;
        public static List<ToolStripMenuItem> PostásMenü = new List<ToolStripMenuItem>();
        public static int PostásNévId = 1;  //Vendég bejelentkezés
        public static List<Adat_Jogosultságok> PostásJogosultságok = new List<Adat_Jogosultságok>();
        public static List<Adat_Oldalak> PostásOldalak = new List<Adat_Oldalak>();
        public static List<Adat_Gombok> PostásGombok = new List<Adat_Gombok>();
        public static List<Adat_Kiegészítő_Könyvtár> PostásKönyvtár = new List<Adat_Kiegészítő_Könyvtár>();
        public static Adat_Users Postás_Felhasználó = null;
        public static List<string> Postás_Telephelyek = new List<string>();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //Magyarosítás
            AdvancedDataGridView.SetTranslations(AdvancedDataGridView.LoadTranslationsFromFile("lang_hu-HU.json"));
            AdvancedDataGridViewSearchToolBar.SetTranslations(AdvancedDataGridViewSearchToolBar.LoadTranslationsFromFile("lang_hu-HU.json"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AblakBejelentkezés());
        }

    }
}
