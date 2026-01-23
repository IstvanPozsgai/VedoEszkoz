namespace VédőEszköz
{
    public class Adat_Jogosultságok
    {

        public int UserId { get; private set; }
        public int OldalId { get; private set; }
        public int GombokId { get; private set; }
        public int SzervezetId { get; private set; }
        public bool Törölt { get; private set; }

        public Adat_Jogosultságok(int userId, int oldalId, int gombokId, int szervezetId, bool törölt)
        {

            UserId = userId;
            OldalId = oldalId;
            GombokId = gombokId;
            SzervezetId = szervezetId;
            Törölt = törölt;
        }
    }
}
