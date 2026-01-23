using System;
using System.Globalization;

namespace VédőEszköz
{
    public static partial class Függvénygyűjtemény
    {
        public static DateTime Év_elsőnapja(DateTime dateTime)
        {
            DateTime datum = new DateTime(dateTime.Year, 1, 1);
            return datum;
        }

        public static DateTime Év_elsőnapja(int Év)
        {
            DateTime datum = new DateTime(Év, 1, 1);
            return datum;
        }

        public static DateTime Félév_utolsónapja(int Év)
        {
            DateTime datum = new DateTime(Év, 6, 30);
            return datum;
        }

        public static DateTime Félév_elsőnapja(int Év)
        {
            DateTime datum = new DateTime(Év, 7, 1);
            return datum;
        }

        public static DateTime Év_utolsónapja(DateTime dateTime)
        {
            DateTime datum = new DateTime(dateTime.Year, 12, 31);
            return datum;
        }

        public static DateTime Év_utolsónapja(int Év)
        {
            DateTime datum = new DateTime(Év, 12, 31);
            return datum;
        }

        public static DateTime Hónap_utolsónapja(DateTime dateTime)
        {
            DateTime datum = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
            return datum;
        }


        /// <summary>
        /// Hónap utolsó napjának dátuma
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime HóUNnap(this DateTime dateTime)
        {
            DateTime datum = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
            return datum;
        }


        public static DateTime Hónap_elsőnapja(DateTime dateTime)
        {
            DateTime datum = new DateTime(dateTime.Year, dateTime.Month, 1);
            return datum;
        }


        /// <summary>
        /// Hónap első napjának dátuma
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime HóENap(this DateTime dateTime)
        {
            DateTime datum = new DateTime(dateTime.Year, dateTime.Month, 1);
            return datum;
        }


        public static int Hónap_hossza(DateTime dateTime)
        {
            int datum = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            return datum;
        }


        /// <summary>
        /// Hét első napjának dátuma
        /// </summary>
        /// <param name="Dátum"></param>
        /// <returns></returns>
        public static DateTime HétENap(this DateTime Dátum)
        {
            DateTime válasz;

            int hétnapszáma = Dátum.DayOfWeek == DayOfWeek.Monday
                            ? 1
                            : (int)Dátum.DayOfWeek;
            switch (hétnapszáma)
            {
                case 0: //Vasárnap
                    {
                        válasz = Dátum.AddDays(-6);
                        break;
                    }
                case 1:
                    {
                        válasz = Dátum;
                        break;
                    }
                default:
                    {
                        válasz = Dátum.AddDays(-1 * (hétnapszáma - 1));
                        break;
                    }
            }

            return válasz;
        }


        public static DateTime Hét_elsőnapja(DateTime Dátum)
        {
            DateTime válasz;

            int hétnapszáma = Dátum.DayOfWeek == DayOfWeek.Monday
                            ? 1
                            : (int)Dátum.DayOfWeek;
            switch (hétnapszáma)
            {
                case 0: //Vasárnap
                    {
                        válasz = Dátum.AddDays(-6);
                        break;
                    }
                case 1:
                    {
                        válasz = Dátum;
                        break;
                    }
                default:
                    {
                        válasz = Dátum.AddDays(-1 * (hétnapszáma - 1));
                        break;
                    }
            }

            return válasz;
        }


        /// <summary>
        /// Hét utolsó napjának a dátuma
        /// </summary>
        /// <param name="Dátum"></param>
        /// <returns></returns>
        public static DateTime HétUNap(this DateTime Dátum)
        {
            DateTime válasz;

            int hétnapszáma = Dátum.DayOfWeek == DayOfWeek.Monday
                            ? 1
                            : (int)Dátum.DayOfWeek;
            switch (hétnapszáma)
            {
                case 0: //Vasárnap
                    {
                        válasz = Dátum;
                        break;
                    }
                default:
                    {
                        válasz = Dátum.AddDays(7 - hétnapszáma);
                        break;
                    }
            }
            return válasz;
        }


        public static DateTime Hét_Utolsónapja(DateTime Dátum)
        {
            DateTime válasz;

            int hétnapszáma = Dátum.DayOfWeek == DayOfWeek.Monday
                            ? 1
                            : (int)Dátum.DayOfWeek;
            switch (hétnapszáma)
            {
                case 0: //Vasárnap
                    {
                        válasz = Dátum;
                        break;
                    }
                default:
                    {
                        válasz = Dátum.AddDays(7 - hétnapszáma);
                        break;
                    }
            }
            return válasz;
        }


        public static int Hét_Melyiknapja(DateTime Dátum)
        {
            int hétnapszáma = Dátum.DayOfWeek == DayOfWeek.Monday
                      ? 1
                      : (int)Dátum.DayOfWeek;
            if (hétnapszáma == 0) hétnapszáma = 7;
            return hétnapszáma;
        }


        public static int Hét_Sorszáma(DateTime dátum)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int válasz = ciCurr.Calendar.GetWeekOfYear(dátum, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return válasz;
        }


        /// <summary>
        /// Hét sorszáma
        /// </summary>
        /// <param name="dátum"></param>
        /// <returns></returns>
        public static int Hét_Ssz(this DateTime dátum)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int válasz = ciCurr.Calendar.GetWeekOfYear(dátum, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return válasz;
        }

        public static DateTime ElsőNap()
        {
            return new DateTime(1900, 1, 1);
        }

        public static DateTime Nap0000(this DateTime dátum)
        {
            DateTime datum = new DateTime(dátum.Year, dátum.Month, dátum.Day, 0, 0, 0);
            return datum;
        }

        public static DateTime Nap2359(this DateTime dátum)
        {
            DateTime datum = new DateTime(dátum.Year, dátum.Month, dátum.Day, 23, 59, 59);
            return datum;
        }

        public static DateTime Nap0600(this DateTime dátum)
        {
            DateTime datum = new DateTime(dátum.Year, dátum.Month, 1, 6, 0, 0);
            return datum;
        }

        public static DateTime Negyedév_elsőnapja(DateTime dateTime)
        {
            int quarter = ((dateTime.Month - 1) / 3) + 1;
            int firstMonth = (quarter - 1) * 3 + 1;
            return new DateTime(dateTime.Year, firstMonth, 1);
        }

        public static DateTime Negyedév_utolsónapja(DateTime dateTime)
        {
            int quarter = ((dateTime.Month - 1) / 3) + 1;
            int lastMonth = quarter * 3;
            int lastDay = DateTime.DaysInMonth(dateTime.Year, lastMonth);
            return new DateTime(dateTime.Year, lastMonth, lastDay);
        }

        public static DateTime Félév_elsőnapja(DateTime dateTime)
        {
            int firstMonth = (dateTime.Month <= 6) ? 1 : 7;
            return new DateTime(dateTime.Year, firstMonth, 1);
        }

        public static DateTime Félév_utolsónapja(DateTime dateTime)
        {
            int lastMonth = (dateTime.Month <= 6) ? 6 : 12;
            int lastDay = DateTime.DaysInMonth(dateTime.Year, lastMonth);
            return new DateTime(dateTime.Year, lastMonth, lastDay);
        }

        public static DateTime Négyhónap_elsőnapja(DateTime dateTime)
        {
            int period = ((dateTime.Month - 1) / 4) + 1;
            int firstMonth = (period - 1) * 4 + 1;
            return new DateTime(dateTime.Year, firstMonth, 1);
        }

        public static DateTime Négyhónap_utolsónapja(DateTime dateTime)
        {
            int period = ((dateTime.Month - 1) / 4) + 1;
            int lastMonth = period * 4;
            int lastDay = DateTime.DaysInMonth(dateTime.Year, lastMonth);
            return new DateTime(dateTime.Year, lastMonth, lastDay);
        }
    }

}