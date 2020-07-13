using System;

namespace UTILITY.MetodosExtencion
{
    public static class FechaExtension
    {
        public static DateTime PrimerDiaMes(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime UltimoDiaMes(this DateTime date)
        {
            return date.PrimerDiaMes().AddMonths(1).AddDays(-1);
        }
    }
}
