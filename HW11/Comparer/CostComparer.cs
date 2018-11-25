using HW11.FilmClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11.Comparer
{
    class CostComparer : IComparer<Film>
    {
        public int Compare(Film x, Film y)
        {
            if (x == null && x == null)
                return 0;
            else if (x == null)
                return -1;
            else if (y == null)
                return 1;
            else 
                return (x.GetFilmCost().CompareTo(y.GetFilmCost()));
        }
    }
}
