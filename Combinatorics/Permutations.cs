using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Combinatorics
{
    public class MappedPermutations<T> : Permutations
    {
        new public CombinatoricItemList<T> Current;
        public List<T> ElementMap {
            get { return Current.ElementMap; }
            set { Current.ElementMap = value;  }
        }

        public MappedPermutations(List<uint> howMany, uint total)
            : base(howMany, total)
        {
            Current = new CombinatoricItemList<T>(this);
        }
        public MappedPermutations(uint total)
            : base(total)
        {
            Current = new CombinatoricItemList<T>(this);
        }
    }

    public class Permutations : Variations
    {
        // permutations is a special case of variations where you select all order-dependent arrangements of all characters in a sequence
        public Permutations(List<uint> howMany, uint total)
            : base(total, total, true)
        {
            seqConsituents = howMany;
            // should be able to assert that howMant.Count == total
        }
        public Permutations(uint total)
            : base(total, total, false)
        {
        }

        // internal methods / elements
        private List<uint> seqConsituents;

        private int SwapForNextSmallestSeqConstituent(int curIndex, int swapItem)
        {
            int swapLoc = -1;
            uint curMax = uint.MaxValue;
            for (int i = curIndex + (swapItem >= 0 ? 1 : 0); i < pick; i++)
                if (internalCurrent[i] < curMax && internalCurrent[i] > swapItem)
                {
                    curMax = internalCurrent[i];
                    swapLoc = i;
                }
            if (swapLoc >= 0)
            {
                uint temp = internalCurrent[curIndex];
                internalCurrent[curIndex] = internalCurrent[swapLoc];
                internalCurrent[swapLoc] = temp;
            }
            return swapLoc;

        }

        override protected uint IncrementSeqItem(int seqItem)
        { //seqItem is a no-op here - we are always resetting the sequence
            /// for a sequence like <1,2,2,0,0>
            /// abbcc   - find the first pair in alpha order (bc).  Increment the left one, resequence the rest
            /// abcbc            /// abccb            /// acbbc            /// acbcb            /// accbb
            /// babcc            /// bacbc            /// baccb            /// bbacc            /// bbcac
            /// bbcca            /// bcabc            /// bcacb            /// bcbac            /// bcbca
            /// bccab            /// bccba            /// cabbc            /// cabcb            /// cacbb
            /// cbabc            /// cbacb            /// cbbac            /// cbbca            /// cbcab
            /// cbcba            /// ccabb            /// ccbab            /// ccbba

            for (int i = seqItem - 1; i >= 0; i--)
            {
                if (internalCurrent[i] < internalCurrent[i + 1]) // here's the swap point... 
                {
                    SwapForNextSmallestSeqConstituent(i, (int)internalCurrent[i]);
                    for (int j = i + 1; j < pick - 1; j++)
                        SwapForNextSmallestSeqConstituent(j, -1);
                    return (uint)i;
                }
            }
            return from;
        }

        override protected bool InitSequence()
        {
            if (allowRepetition)
            {
                AtEnd = false;
                internalCurrent = new List<uint>();
                for (uint i = 0; i < pick; i++)
                    internalCurrent.Add(allowRepetition ? 0 : i);
                int thisIndex = 0;
                for (uint i = 0; i < from; i++)
                    for (int j = 0; j < seqConsituents[(int)i]; j++)
                        internalCurrent[thisIndex++] = i;
                return true;
            }
            return base.InitSequence();
        }

        override protected bool SeqIsOK(int ckItem)
        {
            if (allowRepetition)
                return true;    // we started with a valid sequence, and are swapping elements - has to still be ok...
            return base.SeqIsOK(ckItem);
        }

        override protected bool InitSeqItem(int seqItem)
        {
            if (allowRepetition)
                return true;    // there is no "init" of items here - we're swapping, everything is updated in IncrementSeqItem
            return base.InitSeqItem(seqItem);
        }
    }

}
