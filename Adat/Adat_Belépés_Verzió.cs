namespace VédőEszköz
{
    public class Adat_Belépés_Verzió
    {
        public long Id { get; private set; }
        public double Verzió { get; private set; }

        public Adat_Belépés_Verzió(long id, double verzió)
        {
            Id = id;
            Verzió = verzió;
        }
    }
}
