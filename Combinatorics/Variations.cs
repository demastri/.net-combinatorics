using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Combinatorics
{
    public class MappedVariations<T> : Variations
    {
        new public CombinatoricItemList<T> Current;
        public List<T> ElementMap
        {
            get { return Current.ElementMap; }
            set { Current.ElementMap = value; }
        }
        
        public MappedVariations(uint howMany, uint total)
            : base(howMany, total)
        {
            Current = new CombinatoricItemList<T>(this);
        }
        public MappedVariations(uint howMany, uint total, bool dup)
            : base(howMany, total, dup)
        {
            Current = new CombinatoricItemList<T>(this);
        }
    }

    public class Variations : CombinatoricsBase
    {
        // variatons is a selection of all order-dependent arrangements of the given number of characters in a sequence
        public Variations(uint howMany, uint total, bool dup)
            : base(howMany, total, dup)
        {
        }
        public Variations(uint howMany, uint total)
            : base(howMany, total, false)
        {
        }
        // internal methods
        override protected bool SeqIsOK(int ckItem)
        {
            // all that we need here is that no items are >= total and no dups if not allowed
            for (int i = 0; i <= ckItem; i++)
            {
                if (!(internalCurrent[i] < from))
                    return false;
                for (int j = i+1; !allowRepetition && j <= ckItem; j++)
                    if( internalCurrent[i] == internalCurrent[j] )
                        return false;
            }
            return true;
        }
        override protected bool InitSeqItem(int seqItem)
        {
            // should be able to assert that seqItem is at least 1 here...
            // we need either 0 (allow duplicates), or the smallest unseen element
            if (allowRepetition)
                internalCurrent[seqItem] = 0;
            else
            {
                for (uint ckElt = 0; ckElt < from; ckElt++)
                {
                    bool seen = false;
                    for (int i = 0; i < seqItem; i++)
                        seen |= (internalCurrent[i] == ckElt);
                    if (!seen)
                    {
                        internalCurrent[seqItem] = ckElt;
                        break;
                    }
                }
            }
            return internalCurrent[seqItem] < from;
        }
    }
}
