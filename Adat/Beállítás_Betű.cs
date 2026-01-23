using System.Drawing;

namespace VédőEszköz
{
    public class Beállítás_Betű
    {
        public Color Szín { get; set; } = Color.Black;
        public int Méret { get; set; } = 12;
        public string Név { get; set; } = "Arial";
        public string Formátum { get; set; } = "";
        public bool Aláhúzott { get; set; } = false;
        public bool Dőlt { get; set; } = false;
        public bool Vastag { get; set; } = false;
    }
}
