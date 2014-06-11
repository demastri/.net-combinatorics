using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Combinatorics
{
    public static class Helpers
    {
        public static List<T> Clone<T>(this List<T> listToClone) 
        {
            // if T is cloneable, I'd like to do a deep copy
                // xxx
            // else, simpoly copy the items by value here
            return listToClone.Select(item => (T)item).ToList();
        }
    }
}
