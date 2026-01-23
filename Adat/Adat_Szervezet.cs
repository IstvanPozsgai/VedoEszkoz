namespace VédőEszköz
{
    public class Adat_Szervezet
    {
        public int Id { get; private set; }
        public string Szervezet { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Szervezet(int id, string szervezet, bool státus)
        {
            Id = id;
            Szervezet = szervezet;
            Státus = státus;
        }
    }
}
