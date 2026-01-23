using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace VédőEszköz
{
    public static partial class Függvénygyűjtemény
    {

        readonly static Kezelő_Excel_Beolvasás Kéz_Beolvasás = new Kezelő_Excel_Beolvasás();

        /// <summary>
        /// Ez lesz a végleges változat
        /// Az Excel táblát kapja és a fejlécét hasonlítja össze a táblázatban letárolt értékekkel.
        /// Csak akkor ad vissza igaz értéket, ha a beolvasni kívánt változókat tartalmazza az excel tábla
        /// Tehát ha a sorrend nem jó, de a változók benne vannak akkor is igazat ad vissza.
        /// </summary>
        /// <param name="Melyik"></param>
        /// <param name="Fejlécsor"></param>
        /// <returns></returns>
        public static bool Betöltéshelyes(string Melyik, DataTable Tábla)
        {
            bool válasz = true;
            try
            {
                List<Adat_Excel_Beolvasás> Adatok = Kéz_Beolvasás.Lista_Adatok();
                //csak azokat az adatokat nézzük amit be kell tölteni.
                Adatok = (from a in Adatok
                          where a.Csoport == Melyik.Trim()
                          && a.Státusz == false
                          && a.Változónév.Trim() != "0"
                          orderby a.Oszlop
                          select a).ToList();
                //             Végignézzük a változók listáját és ha van benne olyan ami nincs a táblázatban átállítjuk a státusszát
                foreach (Adat_Excel_Beolvasás rekord in Adatok)
                {
                    bool volt = false;
                    int i = 0;
                    while (volt == false && i < Tábla.Columns.Count)
                    {
                        if (rekord.Fejléc.Trim() == Tábla.Columns[i].ColumnName.Trim()) volt = true;
                        i++;
                    }
                    if (!volt)
                    {
                        válasz = false;
                        break;
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Függvénygyűjtemény - Betöltéshelyes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }





        #region Jelszó kódolások

        public static string Rövidkód(string jelszó)
        {
            int hossz = jelszó.Length;
            string újszó;
            újszó = ((char)(122 - hossz)).ToString();
            for (int i = 1, loopTo = hossz; i <= loopTo; i++)
                újszó += ((char)(65 + Convert.ToInt32(jelszó.Substring(i - 1, 1)) - i)).ToString();
            string ford = "";
            for (int i = újszó.Length; i >= 1; i -= 1)
                ford += újszó.Substring(i - 1, 1);
            return ford;
        }

        public static string Kódol(string jelszó)
        {
            Random rnd = new Random(100);
            int hossz = jelszó.Length;
            string újszó = "";
            string eredmény;
            int érték;
            újszó += ((char)(Convert.ToInt32(rnd.Next(1, 11)) + 97)).ToString();
            újszó += ((char)(Convert.ToInt32(rnd.Next(1, 11)) + 97)).ToString();
            újszó += ((char)(122 - hossz)).ToString();
            for (int i = 1; i <= hossz; i++)
            {
                eredmény = jelszó.Substring(i - 1, 1);
                érték = Convert.ToInt32(eredmény);
                újszó += ((char)(65 + érték - i)).ToString();
            }
            // kitölti 20 karakterig
            for (int i = újszó.Length; i <= 19; i++)
                újszó += ((char)(Convert.ToInt32(rnd.Next(1, 11)) + 97)).ToString();
            string ford = "";
            for (int i = 20; i >= 1; i -= 1)
                ford += újszó.Substring(i - 1, 1);
            return ford;
        }

        public static string Dekódolja(string áljelszó)
        {
            string ford = "";
            int hossz;
            string újjelszó = "";
            if (áljelszó.Length > 0)
            {
                for (int i = 20; i >= 1; i -= 1)
                    ford += áljelszó.Substring(i - 1, 1);

                hossz = 122 - ASC(ford.Substring(2, 1));

                for (int i = 1; i <= hossz; i++)
                {
                    int ideig = ASC(ford.Substring(i + 2, 1)) + i - 65;

                    újjelszó += ideig.ToString();

                }
            }
            return újjelszó.Replace(@"\u000", "");
        }

        public static int ASC(string Betű)
        {
            //Visszaadjuk a betű ascii kódját miután karakterré alakítottuk
            char valami = Convert.ToChar(Betű);
            return (int)valami;
        }


        public static string MÁSKódol(string jelszó)
        {
            Random rnd = new Random();
            int hossz = jelszó.Length;
            jelszó = jelszó.ToUpper();
            string újszó = "";
            újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 47)).ToString(); //kamu
            újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 97)).ToString(); //kamu
            újszó += ((char)(122 - hossz)).ToString();
            for (int i = 1; i <= hossz; i++)
            {
                string betű = jelszó.Substring(i - 1, 1);
                int ss = char.Parse(betű);
                string újbetű = ((char)(ss + i - 15)).ToString();
                int újss = char.Parse(újbetű);
                újszó += újbetű;
            }
            // kitölti 25 karakterig
            for (int i = újszó.Length; i <= 24; i++)
                újszó += ((char)(int)Convert.ToInt32(rnd.Next(100) * 10) + 97).ToString();
            string ford = "";
            for (int i = 25; i >= 1; i -= 1)
                ford += újszó.Substring(i - 1, 1);
            return ford;
        }

        public static string MÁSDekódolja(string áljelszó)
        {

            string ford = "";
            int hossz;
            string újjelszó = "";
            string betű;

            if (áljelszó.Length > 0)
            {
                for (int i = 25; i >= 1; i -= 1)
                    ford += áljelszó.Substring(i - 1, 1);
                hossz = 122 - Convert.ToChar(ford.Substring(2, 1));

                for (int i = 1; i <= hossz; i++)
                {
                    betű = ford.Substring(i + 2, 1);
                    char Betűkar = char.Parse(betű);
                    újjelszó += ((char)(char.Parse(betű) - i + 15)).ToString();
                }
            }

            return újjelszó;
        }

        public static string MÁSRövidkód(string jelszó)
        {
            Random rnd = new Random();
            int hossz = jelszó.Length;
            jelszó = jelszó.ToUpper();
            string újszó = "";

            újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 97)).ToString();
            újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 97)).ToString();
            újszó += ((char)(122 - hossz)).ToString();

            for (int i = 1, loopTo = hossz; i <= loopTo; i++)
            {
                char egybetű = Convert.ToChar(jelszó.Substring(i - 1, 1));
                int Betű = Convert.ToInt32(egybetű);
                újszó += ((char)(Betű + i - 15)).ToString();
            }
            string ford = "";
            for (int i = újszó.Length; i >= 1; i -= 1)
            {
                ford += újszó.Substring(i - 1, 1);
            }
            return ford;
        }

        #endregion


        public static bool Vanjoga(int melyikelem, int csoport)
        {
            bool válasz;
            switch (csoport)
            {
                case 1: //1 -es csoport
                    {
                        if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "3" ||
                            Program.PostásJogkör.Substring(melyikelem - 1, 1) == "7" ||
                            Program.PostásJogkör.Substring(melyikelem - 1, 1) == "b" ||
                            Program.PostásJogkör.Substring(melyikelem - 1, 1) == "f")
                        { válasz = true; }
                        else
                        { válasz = false; }
                        break;
                    }
                case 2: // 2-es csoport
                    {
                        if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "5" ||
                                  Program.PostásJogkör.Substring(melyikelem - 1, 1) == "7" ||
                                  Program.PostásJogkör.Substring(melyikelem - 1, 1) == "d" ||
                                  Program.PostásJogkör.Substring(melyikelem - 1, 1) == "f")
                        { válasz = true; }
                        else
                        { válasz = false; }
                        break;
                    }
                default: //3-as csoport
                    {
                        if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "9" ||
                              Program.PostásJogkör.Substring(melyikelem - 1, 1) == "b" ||
                              Program.PostásJogkör.Substring(melyikelem - 1, 1) == "d" ||
                              Program.PostásJogkör.Substring(melyikelem - 1, 1) == "f")

                        { válasz = true; }
                        else
                        { válasz = false; }
                        break;
                    }
            }
            return válasz;

        }

    }



}