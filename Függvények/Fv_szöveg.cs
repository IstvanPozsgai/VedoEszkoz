namespace VédőEszköz
{

    public static partial class Függvénygyűjtemény
    {
        /// <summary>
        /// Szöveg elejéről a felesleges karaktereket elhagyja.
        /// pl. 00123456 --> 123456 csinál, ha 0- a string
        /// </summary>
        /// <param name="szöveg"></param>
        /// <param name="betű"></param>
        /// <returns></returns>
        public static string Eleje_kihagy(string szöveg, string betű)
        {
            string válasz = "";
            for (int i = 0; i < szöveg.Length; i++)
            {
                //megkeressük az első olyan betűt amit nem kell eldobni
                if (szöveg.Substring(i, 1) != betű)
                    return Szöveg_Tisztítás(szöveg, i, szöveg.Length - i);
            }
            return válasz;
        }

        /// <summary>
        /// Combo listában szereplő név=dolgozószám visszaadjuk a nevet és dolgozószámot
        /// </summary>
        /// <param name="szöveg"></param>
        /// <param name="DolgozóNév"></param>
        /// <param name="DolgozóSzám"></param>
        public static void Dolgozó_Darabol(string szöveg, out string DolgozóNév, out string DolgozóSzám)
        {
            string[] darabol = szöveg.Split('=');

            DolgozóNév = darabol[0].Trim();
            DolgozóSzám = darabol[1].Trim();
        }

        /// <summary>
        /// Adott szövegből úgy vágja ki a megfelelő darabot, hogy előtte megvizsgálja azt.
        /// Ezen kívül azon karaktereket is átalakítja ami problémát okoz.
        /// </summary>
        /// <param name="szöveg">Tisztítandó szöveg</param>
        /// <param name="kezdő">Kezdő pozíció</param>
        /// <param name="hossz">Szöveg hossza, ha -1 akkor nem veszi figyelembe</param>
        /// <returns></returns>
        public static string Szöveg_Tisztítás(string szöveg, int kezdő, int hossz = -1)
        {
            string válasz = szöveg.Trim() == "" ? "_" : szöveg.Trim();
            if (hossz != -1)
            {
                if (szöveg.Length > hossz)
                    válasz = szöveg.Substring(kezdő, hossz);
            }
            else
                válasz = szöveg.Substring(kezdő);

            válasz = válasz.Replace('\"', '`');
            válasz = válasz.Replace("'", "`");

            return válasz;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="szöveg">Tisztítandó szöveg</param>
        /// <param name="kezdő">Kezdő pozíció</param>
        /// <param name="hossz">Szöveg hossza, ha -1 akkor nem veszi figyelembe</param>
        /// <param name="sortörés">Sortörés és sorkocsi vissza jeleket is kicsereli</param>
        /// <returns></returns>
        public static string Szöveg_Tisztítás(string szöveg, int kezdő, int hossz, bool sortörés)
        {
            string válasz = szöveg.Trim() == "" ? "_" : szöveg.Trim();
            if (hossz != -1)
            {
                if (szöveg.Length > hossz)
                    válasz = válasz.Substring(kezdő, hossz);
            }
            else
                válasz = szöveg.Substring(kezdő);

            válasz = válasz.Replace("'", "`");
            válasz = válasz.Replace("\"", "`");

            if (sortörés)
            {
                válasz = válasz.Replace("\n", "");
                válasz = válasz.Replace("\r", "");
            }
            return válasz;
        }

        /// <summary>
        /// Megtisztítja a szöveget az olyan karakterektől amik nem láthatóak, vagy a rögzítés során problémat okozak.
        /// Pl. ', ",
        /// </summary>
        /// <param name="szöveg"></param>
        /// <returns></returns>
        public static string Szöveg_Tisztítás(string szöveg, bool sortörés = false)
        {
            string válasz = szöveg.Replace("'", "`");    // ' cseréli ki ''
            válasz = válasz.Replace("\"", "``");  // " cseréli ki üres mezőre
            válasz = válasz.Replace("/", "");
            válasz = válasz.Replace(",", "");
            válasz = válasz.Replace(@"\", "");
            if (sortörés)
            {
                válasz = válasz.Replace("\n", " ");
                válasz = válasz.Replace("\r", " ");
            }
            return válasz.Trim();
        }
    }

}