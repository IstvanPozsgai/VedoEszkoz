using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VédőEszköz
{
    public class Kezelő_Belépés_MindenMásol
    {
        private readonly Kezelő_Belépés_Jogosultságtábla KézRégi = new Kezelő_Belépés_Jogosultságtábla();
        private readonly Kezelő_Users KézUsers = new Kezelő_Users();
        private readonly Kezelő_Kiegészítő_Könyvtár KézSzervezetek = new Kezelő_Kiegészítő_Könyvtár();
        private readonly Kezelő_Jogosultságok KézJog = new Kezelő_Jogosultságok();

        public void Másolás(string telephely, string felhasznaloNev)
        {
            try
            {
                //string fájlexc = "";

                //OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                //{
                //    InitialDirectory = "MyDocuments",
                //    Title = "SAP-s Adatok betöltése",
                //    FileName = "",
                //    Filter = "Excel |*.xlsx"
                //};

                //// bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                //if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                //    fájlexc = OpenFileDialog1.FileName;
                //else
                //    return;
                //DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexc);
                //List<IdeigJogosultságok> ExcelLista = new List<IdeigJogosultságok>();
                //for (int i = 0; i < Tábla.Rows.Count; i++)
                //{
                //    IdeigJogosultságok EgyJog = new IdeigJogosultságok(
                //      Tábla.Rows[i]["Sorszám"].ToÉrt_Int(),
                //      Tábla.Rows[i]["Mező"].ToÉrt_Int(),
                //      Tábla.Rows[i]["Ablak ID"].ToÉrt_Int(),
                //      Tábla.Rows[i]["Gomb ID"].ToÉrt_Int());
                //    ExcelLista.Add(EgyJog);
                //}


                //List<Adat_Belépés_Jogosultságtábla> AdatokJogRégi = KézRégi.Lista_Adatok(telephely);
                //Adat_Belépés_Jogosultságtábla AdatFelhasználó = AdatokJogRégi.Where(a => a.Név.ToUpper() == felhasznaloNev.ToUpper()).FirstOrDefault();

                //List<Adat_Users> AdatokUser = KézUsers.Lista_Adatok();
                //Adat_Users Adat_Users = AdatokUser.Where(a => a.UserName == felhasznaloNev).FirstOrDefault();

                //List<Adat_Kiegészítő_Könyvtár> Szervezetek = KézSzervezetek.Lista_Adatok();
                //Adat_Kiegészítő_Könyvtár Telep = Szervezetek.Where(a => a.Név == telephely).FirstOrDefault();

                //List<Adat_Jogosultságok> AdatokGy = new List<Adat_Jogosultságok>();
                //if (AdatFelhasználó == null || Adat_Users == null || Telep == null) return;

                //for (int i = 0; i < AdatFelhasználó.Jogkörúj1.Length; i++)
                //{
                //    string betű = AdatFelhasználó.Jogkörúj1.Substring(i, 1);
                //    if (betű != "0")
                //    {
                //        List<int> Mezők = Mező(betű);
                //        foreach (int mező in Mezők)
                //        {
                //            List<IdeigJogosultságok> GombokId = (from a in ExcelLista
                //                                                 where a.Sorszám == i + 1
                //                                                 && a.Mező == mező
                //                                                 select a).ToList();
                //            foreach (IdeigJogosultságok gomb in GombokId)
                //            {
                //                Adat_Jogosultságok ADAT = new Adat_Jogosultságok(
                //                    Adat_Users.UserId,
                //                    gomb.Ablakid,
                //                    gomb.Gombid,
                //                    Telep.ID,
                //                    false);
                //                AdatokGy.Add(ADAT);
                //            }
                //        }
                //    }
                //}
                //KézJog.Rögzítés(AdatokGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<int> Mező(string Betű)
        {
            List<int> válasz = new List<int>();
            if (Betű == "3" || Betű == "7" || Betű == "b" || Betű == "f") válasz.Add(1); //1-es csoport
            if (Betű == "5" || Betű == "7" || Betű == "d" || Betű == "f") válasz.Add(2); //2-es csoport
            if (Betű == "9" || Betű == "b" || Betű == "d" || Betű == "f") válasz.Add(3); //3-es csoport
            if (Betű == "1") válasz.Add(4); //Megjelenítés csoport
            return válasz;
        }


    }

    public class IdeigJogosultságok
    {
        public int Sorszám { get; set; }
        public int Mező { get; set; }
        public int Ablakid { get; set; }
        public int Gombid { get; set; }

        public IdeigJogosultságok(int sorszám, int mező, int ablakid, int gombid)
        {
            Sorszám = sorszám;
            Mező = mező;
            Ablakid = ablakid;
            Gombid = gombid;
        }
    }

}