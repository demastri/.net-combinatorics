using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPD.Combinatorics;

namespace CombPermTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool printDetail = true;

            Combinations newComb = new Combinations(3, 6);
            int ct;
            for (ct = 0, newComb.First(); !newComb.AtEnd; newComb.Next(), ct++)
                if (printDetail)
                    PrintList(newComb.Current);
            Console.WriteLine(ct.ToString() + " comb for [6,3]");

            newComb = new Combinations(3, 6, true);
            for (ct = 0, newComb.First(); !newComb.AtEnd; newComb.Next(), ct++)
                if (printDetail)
                    PrintList(newComb.Current);
            Console.WriteLine(ct.ToString() + " comb (with dup) for [6,3]");

            Variations newVar = new Variations(3, 6);
            for (ct = 0, newVar.First(); !newVar.AtEnd; newVar.Next(), ct++)
                if (printDetail)
                    PrintList(newVar.Current);
            Console.WriteLine(ct.ToString() + " var for [6,3]");

            newVar = new Variations(3, 6, true);
            for (ct = 0, newVar.First(); !newVar.AtEnd; newVar.Next(), ct++)
                if (printDetail)
                    PrintList(newVar.Current);
            Console.WriteLine(ct.ToString() + " var (with dup) )for [6,3]");

            Permutations newPerm = new Permutations(6);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next(), ct++)
                if (printDetail)
                    PrintList(newPerm.Current);
            Console.WriteLine(ct.ToString() + " perm for [6,3]");

            List<uint> seqDetails = new List<uint>() { 1, 3, 0, 0, 2, 0 };
            newPerm = new Permutations(seqDetails);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next(), ct++)
                if (printDetail)
                    PrintList(newPerm.Current);
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < 6; i++)
                Console.Write(seqDetails[i].ToString() + (i < 5 ? "," : ""));
            Console.WriteLine(">]");

            seqDetails = new List<uint>() { 1, 2, 2, 0, 0 };
            newPerm = new Permutations(seqDetails);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next(), ct++)
                if (printDetail)
                    PrintList(newPerm.Current);
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < 5; i++)
                Console.Write(seqDetails[i].ToString() + (i < 4 ? "," : ""));
            Console.WriteLine(">]");

            List<string> myElts = new List<string>() { "a", "b", "a", "d", "e", "f" };
            Permutations<string> newStringPerm = new Permutations<string>(seqDetails, myElts);
            for (ct = 0, newStringPerm.First(); !newStringPerm.AtEnd; newStringPerm.Next(), ct++)
                if (printDetail)
                    PrintList(newStringPerm.Current);
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < 5; i++)
                Console.Write(myElts[i].ToString() + (i < 4 ? "," : ""));
            Console.WriteLine(">]");

        }

        private static void PrintList<T>(Combinatorics<T>.OutputList b)
        {
            Console.Write("<");
            string sep = "";
            for (int i = 0; i < b.Count; i++)
            {
                Console.Write(sep + b[i].ToString());
                sep = ",";
            }
            Console.WriteLine(">");
        }
    }
}
