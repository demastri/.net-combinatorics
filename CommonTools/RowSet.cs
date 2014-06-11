using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.CommonTools
{
    public class RowSet
    {
        public int[] list = { 1, 2, 3 };
        public RowSet(int i1, int i2, int i3)
        {
            list[0] = i1;
            list[1] = i2;
            list[2] = i3;
        }
        public static RowSet BuildRemaining(RowSet r1, RowSet r2)
        {
            List<int> ckList = new List<int>() { 1,2,3,4,5,6,7,8,9};
            for (int i = 0; i < 3; i++)
            {
                ckList.Remove(r1.list[i]);
                ckList.Remove(r2.list[i]);
            }
            // assumption (for now) is that there are 3 elts left here...
            System.Diagnostics.Debug.Assert(ckList.Count >= 3);
            if (ckList.Count == 3)
                return new RowSet(ckList[0], ckList[1], ckList[2]);
            return null;
        }
        public bool Contains(int elt)
        {
            return list.Contains(elt);
        }
        public bool HasOverlap(RowSet r)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (list[i] == r.list[j])
                        return true;
            return false;
        }
    }
}
