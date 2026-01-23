using System;

namespace VédőEszköz
{
    public class Adat_Users
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string WinUserName { get; private set; }
        public string Dolgozószám { get; private set; }
        public string Password { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Frissít { get; private set; }
        public bool Törölt { get; private set; }
        public string Szervezetek { get; private set; }
        public string Szervezet { get; private set; }
        public bool GlobalAdmin { get; private set; }
        public bool TelepAdmin { get; private set; }

        public Adat_Users(
            int userId,
            string userName,
            string winUserName,
            string dolgozószám,
            string password,
            DateTime dátum,
            bool frissít,
            bool törölt,
            string szervezetek,
            string szervezet,
            bool globalAdmin,
            bool telepAdmin)
        {
            UserId = userId;
            UserName = userName;
            WinUserName = winUserName;
            Dolgozószám = dolgozószám;
            Password = password;
            Dátum = dátum;
            Frissít = frissít;
            Törölt = törölt;
            Szervezetek = szervezetek;
            Szervezet = szervezet;
            GlobalAdmin = globalAdmin;
            TelepAdmin = telepAdmin;
        }

        public Adat_Users(int userId, string password, bool frissít)
        {
            UserId = userId;
            Password = password;
            Frissít = frissít;
        }
    }
}
