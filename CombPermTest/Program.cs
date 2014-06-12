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

            Combinations newComb = new Combinations(3, 5);
            int ct;
            for (ct = 0, newComb.First(); !newComb.AtEnd; newComb.Next(), ct++)
                if (printDetail)
                    PrintList(newComb.Current);
            Console.WriteLine(ct.ToString() + " comb for [5,3]");

            newComb = new Combinations(3, 5, true);
            for (ct = 0, newComb.First(); !newComb.AtEnd; newComb.Next(), ct++)
                if (printDetail)
                    PrintList(newComb.Current);
            Console.WriteLine(ct.ToString() + " comb for [5,3]");

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

            List<uint> seqDetails = new List<uint>() { 1, 3, 0, 0, 2 };
            newPerm = new Permutations(seqDetails);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next(), ct++)
                if (printDetail)
                    PrintList(newPerm.Current);
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < seqDetails.Count; i++)
                Console.Write(seqDetails[i].ToString() + (i < seqDetails.Count - 1 ? "," : ""));
            Console.WriteLine(">]");

            seqDetails = new List<uint>() { 1, 2, 2 };
            newPerm = new Permutations(seqDetails);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next(), ct++)
                if (printDetail)
                    PrintList(newPerm.Current);
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < seqDetails.Count; i++)
                Console.Write(seqDetails[i].ToString() + (i < seqDetails.Count - 1 ? "," : ""));
            Console.WriteLine(">]");

            List<string> myElts = new List<string>() { "a", "b", "a", "d", "e", "f" };
            Permutations<string> newStringPerm = new Permutations<string>(seqDetails, myElts);
            for (ct = 0, newStringPerm.First(); !newStringPerm.AtEnd; newStringPerm.Next(), ct++)
                if (printDetail)
                    PrintList(newStringPerm.Current);
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < seqDetails.Count; i++)
                Console.Write(seqDetails[i].ToString() + (i < seqDetails.Count - 1 ? "," : ""));
            Console.Write("> over <");
            for (int i = 0; i < myElts.Count; i++)
                Console.Write(myElts[i].ToString() + (i < myElts.Count - 1 ? "," : ""));
            Console.WriteLine(">]");

            List<string> mySeq = new List<string>() { "a", "b", "c", "d", "e", "f" };
            Combinations<string> newStringComb = new Combinations<string>(3, 5, true,  mySeq);
            for (ct = 0, newStringComb.First(); !newStringComb.AtEnd; newStringComb.Next(), ct++)
                if (printDetail)
                    PrintList(newStringComb.Current);
            Console.Write(ct.ToString() + " comb for [5,3]");
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
