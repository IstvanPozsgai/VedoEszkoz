namespace VédőEszköz
{
    public class Adat_Gombok
    {
        public int GombokId { get; private set; }
        public string FromName { get; private set; }
        public string GombName { get; private set; }
        public string GombFelirat { get; private set; }
        public string Szervezet { get; private set; }

        public bool Látható { get; private set; }
        public bool Törölt { get; private set; }

        public Adat_Gombok(int gombokId, string fromName, string gombName, string gombFelirat, string szervezet, bool látható, bool törölt)
        {
            GombokId = gombokId;
            FromName = fromName;
            GombName = gombName;
            GombFelirat = gombFelirat;
            Szervezet = szervezet;
            Látható = látható;
            Törölt = törölt;
        }
    }
}
