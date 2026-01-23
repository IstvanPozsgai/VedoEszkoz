namespace VédőEszköz
{
    public class Adat_Oldalak
    {
        public int OldalId { get; private set; }
        public string FromName { get; private set; }
        public string MenuName { get; private set; }
        public string MenuFelirat { get; private set; }
        public bool Látható { get; private set; }
        public bool Törölt { get; private set; }

        public Adat_Oldalak(int oldalId, string fromName, string menuName, string menuFelirat, bool látható, bool törölt)
        {
            OldalId = oldalId;
            FromName = fromName;
            MenuName = menuName;
            MenuFelirat = menuFelirat;
            Látható = látható;
            Törölt = törölt;
        }
    }
}
