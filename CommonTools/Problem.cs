using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPD.Combinatorics;

namespace Sudoku.CommonTools
{
    public class Problem
    {
        public Problem()
        {
            nbrOfEntries = nbrOfSymbols = 0;

            givens = new Dictionary<int, int>();
            constraints = new List<List<int>>();

            solution = null;
        }
        public Problem(uint sym, uint ent)
        {
            nbrOfSymbols = sym;
            nbrOfEntries = ent;

            givens = new Dictionary<int, int>();
            constraints = new List<List<int>>();

            solution = new List<int>((int)ent);
            for (int i = 0; i < ent; i++)
                solution.Add(-1);
        }

        protected uint nbrOfSymbols;
        private uint nbrOfEntries;

        Dictionary<int, int> m_Givens;
        public Dictionary<int, int> givens
        {
            get { return m_Givens; }
            set
            {
                m_Givens = value;
                foreach (int index in givens.Keys)
                    solution[index] = (int)(givens[index]);
            }
        }
        List<int> solution;

        protected List<List<int>> constraints;

        bool isPossible
        {
            get
            {
                foreach (List<int> c in constraints)
                {
                    List<bool> seen = new List<bool>();
                    for (int i = 0; i < nbrOfEntries; i++)
                        seen.Add(false);
                    foreach (int entry in c)
                    {
                        // have I already seen this one?
                        if (solution[entry] >= 0)
                            if (seen[solution[entry]]) // yup...
                                return false;
                            else
                                seen[solution[entry]] = true;
                    }
                }
                return true;
            }
        }
        bool isComplete
        {
            get
            {
                foreach (List<int> c in constraints)
                {
                    List<bool> seen = new List<bool>();
                    for (int i = 0; i < nbrOfEntries; i++)
                        seen.Add(false);
                    foreach (int entry in c)
                    {
                        // have I already seen this one?
                        if (solution[entry] < 0)    // can't be complete if it isn't filled in...
                            return false;
                        if (seen[solution[entry]]) // yup...
                            return false;
                        else
                            seen[solution[entry]] = true;
                    }
                }
                return true;
            }
        }
        /// <summary>
        ///  SolvePartial
        ///     partial is a list of squares to resolve given the current state of the "problem"
        ///     
        ///     for the "reference" test, S11 is configured with 1-9, partial is defined as the squares in S12
        ///     and the solution space should be 12096
        /// </summary>
        /// <param name="partial"></param>
        /// <returns></returns>
        public List<Dictionary<int, int>> SolvePartial(List<uint> partial)
        {
            List<Dictionary<int, int>> outDict = new List<Dictionary<int, int>>();

            // this is a decoder ring thing that i've written a million times...
            // should work around any existing entries / givens in the solution
            List<int> open = new List<int>();
            List<int> avail = new List<int>();
            Dictionary<int, int> refSoln = new Dictionary<int, int>();
            for (int i = 0; i < nbrOfSymbols; i++)
                avail.Add( i );
            foreach (int i in partial)
            {
                refSoln.Add( i, solution[ i ] );
                if (solution[i] >= 0)
                    avail.Remove(solution[i]);
                else
                    open.Add(i);
            }

            Variations possible = new Variations((uint)open.Count, (uint)open.Count);          
            for (possible.First(); !possible.AtEnd;  possible.Next())
            {
                // merge current comb with partial
                Dictionary<int, int> thisPossible = new Dictionary<int, int>();
                for (int i = 0; i < open.Count; i++)
                    thisPossible.Add(open[i], avail[ Convert.ToInt32(possible.Current[i]) ]);
                givens = thisPossible;

                if (isPossible)
                    outDict.Add(thisPossible);

                // pop back to current solution
                givens = refSoln;
            }
            return outDict;
        }
    }
}
