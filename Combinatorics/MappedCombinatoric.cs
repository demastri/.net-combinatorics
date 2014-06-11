using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Combinatorics
{
    #region helper class
    public class CombinatoricItemList<T> : CombinatoricBaseItemList
    {
        CombinatoricsBase parent;
        public List<T> ElementMap;

        public CombinatoricItemList(CombinatoricsBase p) : base( p )
        {
            parent = p;
            ElementMap = new List<T>();
        }

        new public T this[int index]
        {
            get
            {
                //if (parent.ElementMap == null)
                //    return parent.internalCurrent[index];
                return ElementMap[(int)parent.internalCurrent[index]];
            }
        }
    }
    #endregion

}
