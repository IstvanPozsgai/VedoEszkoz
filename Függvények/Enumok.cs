namespace VédőEszköz
{
    public class Enumok
    {
        public enum Státus
        {
            Aktív = 0,
            Törölt = 1
        };

        public enum Mozgás
        {
            Beérkezés = 0,
            Átadás = 3,
            VisszaVétel = 5,
            Selejtezés = 6,
            Storno = 9
        };
        public enum KeretVastagsag
        {
            Alap,
            Nincs,
            Vékony,
            Közepes,
            Vastag
        }
    }
}
