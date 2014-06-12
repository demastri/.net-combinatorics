using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Combinatorics
{
    public class Combinations : Combinations<uint>
    {
        public Combinations(uint howMany, uint total)
            : base(howMany, total, false, null)
        {
        }
        public Combinations(uint howMany, uint total, bool dup)
            : base(howMany, total, dup, null)
        {
        }
    }
    public class Combinations<T> : Combinatorics<T>
    {
        // combinations is a selection of all order-independent arrangements of the given number of characters in a sequence
        public Combinations(uint howMany, uint total, List<T> eMap)
            : base(howMany, total, false, eMap)
        {
        }
        public Combinations(uint howMany, uint total, bool dup, List<T> eMap)
            : base(howMany, total, dup, eMap)
        {
        }

        override protected bool SeqIsOK(int ckItem)
        {
            // all that we need here is that no items are >= total and that it's increasing to the right
            int last = -1;
            for( int i=0; i<=ckItem; i++ )
                if( !( internalCurrent[i] < from && ( (internalCurrent[i] > last) || (allowRepetition && internalCurrent[i] == last) ) ) )
                    return false;
            return true;
        }
        override protected bool InitSeqItem(int seqItem)
        {
            // should be able to assert that seqItem is at least 1 here...
            // we need either the largest (allow duplicates, or one more than the largest)
            internalCurrent[seqItem] = internalCurrent[seqItem - 1] + (uint)(allowRepetition ? 0 : 1);
            return false;
        }
    }
}
