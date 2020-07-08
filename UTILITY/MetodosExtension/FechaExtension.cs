using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTILITY.MetodosExtension
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
