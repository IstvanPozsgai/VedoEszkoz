using InputForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VédőEszköz
{
    public partial class Ablak_Szervezet_Kezelo : Form
    {
        InputForm form;
        readonly Kezelő_Szervezet KézSzervezet = new Kezelő_Szervezet();
        public Action FrissítésCallBack;

        public void Kezelés(Adat_Szervezet adat = null)
        {
            List<Adat_Szervezet> adatok = KézSzervezet.Lista_Adatok();
            bool UjAdat = adat == null;

            int ujId = adatok.Any() ? adatok.Max(a => a.Id) + 1 : 1;
            int id = UjAdat ? ujId : adat.Id;

            string[] szervezetek = adatok
                .Select(a => a.Szervezet)
                .Distinct()
                .ToArray();

            form = new InputForm(this);
            form.Add("Sorszám", (new InputTextbox("Sorszám:", id.ToStrTrim())).AddRule(null))
                .Add("Szervezet", (new InputSelect("Szervezet neve:", szervezetek)))
                  .Add("Státus", (new InputTextbox("Státusz:", UjAdat ? "Aktív" : (adat.Státus ? "Aktív" : "Inaktív"))).AddRule(null))
                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Mentés")
                .OnSubmit(() =>
                {
                    int idInt = int.Parse(form["Sorszám"]);
                    string szervezetNev = form["Szervezet"];
                    bool statusz = form["Státus"] == "Aktív";
                    Adat_Szervezet ADAT = new Adat_Szervezet(
                        idInt,
                        szervezetNev,
                        statusz
                    );
                        KézSzervezet.Döntés(ADAT);

                    FrissítésCallBack?.Invoke();
                    this.Close();
                });
        }
    }
}