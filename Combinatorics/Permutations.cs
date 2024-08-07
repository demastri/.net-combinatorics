﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Combinatorics
{
    public class Permutations : Permutations<uint>
    {
        public Permutations(List<uint> howMany)
            : base(howMany, null)
        {
            PermBaseInit(howMany, null);
        }
        public Permutations(uint total)
            : base(total, null)
        {
        }
    }

    public class Permutations<T> : Variations<T>
    {
        // permutations is a special case of variations where you select all order-dependent arrangements of all characters in a sequence
        public Permutations(List<uint> howMany, List<T> eMap)
            : base((uint)howMany.Count, (uint)howMany.Count, true, eMap)
        {
            PermBaseInit(howMany, eMap);
            seqConsituents = howMany;
            // should be able to assert that howMant.Count == total
        }
        public Permutations(uint total, List<T> eMap)
            : base(total, total, false, eMap)
        {
        }

        // internal methods / elements
        protected void PermBaseInit(List<uint> howMany, List<T> eMap)
        {
            uint totalCount = 0;
            foreach (uint i in howMany)
                totalCount += i;

            BaseInit(totalCount, totalCount, true, eMap);
        }

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
                    if( i < seqConsituents.Count )
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
