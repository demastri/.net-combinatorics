﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Combinatorics
{
    abstract public class Combinatorics<T>
    {
        /// general (re)introducion to this
        /// order doesn't matter
        ///     combinations - abc -> a b c, ab ac bc, abc
        ///      with repetition -> a b c, aa ab ac bb bc cc, aaa aab aac abb abc acc bbb bbc bcc ccc
        /// order matters
        ///     permutations -> all elements used, abc -> abc acb bac bca cab cba
        ///      with repetition (of elements) aabb -> aabb abab abba baab baba bbaa
        ///     variations -> subset of elements abc (pick 2) -> ab ac ba bc ca cb
        ///      with repetition -> aa ab ac ba bb bc ca cb cc
        ///      
        /// there's an additional case that's presented, 
        ///     variation with identical elements
        ///      so for aac (pick 2) -> aa ac aa ac ca ca
        ///      note here that the elements are not unique

        /// rewrote the classes so the interfaces make more intuitive sense
        ///  The templated classes (Combinations<T> Permutations<T> and Variations<T>) now inherit directly 
        ///    from CombinatoricsBase<T> - they handle strongly typed return sets (in T) in Current[]
        ///    you (still) have to provide an object mapping
        ///  The untemplated classes (Combinations Permutations and Variations) now simply instanate the <uint> version
        ///    of their templated superclass, so when you just want to manipulate uints, no special class templating is needed

        #region external interface

        public OutputList Current;
        public bool AtEnd;

        // initializer - set up the sequence of howMany I need to pick from a total of total elts
        public Combinatorics(uint howMany, uint total, bool rep, List<T> eMap)
        {
            BaseInit(howMany, total, rep, eMap);
        }

        public bool Reset()
        {
            InitSequence();
            return !AtEnd;
        }

        public bool First()
        {
            return Reset();
        }

        public bool Next()
        {
            return !(AtEnd = !((internalCurrent == null) ? First() : Increment()));
        }

        #endregion

        #region internal methods

        protected uint from;
        protected uint pick;
        protected bool allowRepetition;
        protected List<uint> internalCurrent;

        protected void BaseInit(uint howMany, uint total, bool rep, List<T> eMap)
        {
            allowRepetition = rep;

            from = total;
            pick = howMany;

            AtEnd = from < pick && !allowRepetition;
            internalCurrent = null;
            Current = new OutputList(this, eMap);
        }
        protected virtual bool InitSequence()
        {
            if (!(AtEnd = (pick > from && !allowRepetition)))
            {
                internalCurrent = new List<uint>();
                for (uint i = 0; i < pick; i++)
                    internalCurrent.Add(allowRepetition ? 0 : i);
            }
            return !AtEnd;
        }

        private bool Increment()
        {
            int curEnd = internalCurrent.Count - 1;
            while (curEnd >= 0)
            {
                while (IncrementSeqItem(curEnd) < from)
                {
                    if (SeqIsOK(curEnd))
                    {
                        for (int resetVal = curEnd + 1; resetVal < pick; resetVal++)
                            InitSeqItem(resetVal);
                        if (SeqIsOK((int)(pick - 1)))
                            return true;
                    }
                }
                curEnd--;
            }
            return (curEnd >= 0);
        }
        abstract protected bool SeqIsOK(int ckItem);
        abstract protected bool InitSeqItem(int seqItem);
        virtual protected uint IncrementSeqItem(int seqItem) { return ++internalCurrent[seqItem]; }

        #endregion

        public class OutputList
        {
            private Combinatorics<T> parent;
            public List<T> ElementMap;

            public OutputList(Combinatorics<T> p, List<T> eMap)
            {
                ElementMap = eMap;
                parent = p;
            }

            virtual public T this[int index]
            {
                get
                {
                    T tv = default(T);
                    if (ElementMap == null)
                        return (T)Convert.ChangeType(parent.internalCurrent[index], tv.GetType());
                    return ElementMap[(int)parent.internalCurrent[index]];
                }
            }
            public int Count { get { return parent.internalCurrent.Count; } }
            public List<T> ToList()
            {
                return parent.internalCurrent.Select( (item, index) => this[index]).ToList();
            }
        }
    }
}
