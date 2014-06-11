using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.CommonTools;
using JPD.Combinatorics;

namespace CombPermTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool printDetail = false;

            Combinations newComb = new Combinations(3, 6);
            int ct;
            for (ct = 0, newComb.First(); !newComb.AtEnd; newComb.Next())
            {
                if( printDetail )
                    Console.WriteLine("<" + newComb.Current[0] + "," + newComb.Current[1] + "," + newComb.Current[2] + ">");
                ct++;
            }
            Console.WriteLine(ct.ToString() + " comb for [6,3]");

            newComb = new Combinations(3, 6, true);
            for (ct = 0, newComb.First(); !newComb.AtEnd; newComb.Next())
            {
                if (printDetail)
                    Console.WriteLine("<" + newComb.Current[0] + "," + newComb.Current[1] + "," + newComb.Current[2] + ">");
                ct++;
            }
            Console.WriteLine(ct.ToString() + " comb (with dup) for [6,3]");

            Variations newVar = new Variations(3, 6);
            for (ct = 0, newVar.First(); !newVar.AtEnd; newVar.Next())
            {
                if (printDetail)
                    Console.WriteLine("<" + newComb.Current[0] + "," + newComb.Current[1] + "," + newComb.Current[2] + ">");
                ct++;
            }
            Console.WriteLine(ct.ToString() + " var for [6,3]");

            newVar = new Variations(3, 6, true);
            for (ct = 0, newVar.First(); !newVar.AtEnd; newVar.Next())
            {
                if (printDetail)
                    Console.WriteLine("<" + newComb.Current[0] + "," + newComb.Current[1] + "," + newComb.Current[2] + ">");
                ct++;
            }
            Console.WriteLine(ct.ToString() + " var (with dup) )for [6,3]");

            Permutations newPerm = new Permutations(6);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next())
            {
                if (printDetail)
                {
                    Console.Write("<");
                    for (int i = 0; i < 6; i++)
                    {
                        Console.Write(newPerm.Current[i] + (i < 5 ? "," : ""));
                    }
                    Console.WriteLine(">");
                }
                ct++;
            }
            Console.WriteLine(ct.ToString() + " perm for [6,3]");

            List<uint> seqDetails = new List<uint>() { 1,3,0,0,2,0 };
            newPerm = new Permutations(seqDetails, 6);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next())
            {
                if (printDetail)
                {
                    Console.Write("<");
                    for (int i = 0; i < 6; i++)
                    {
                        Console.Write(newPerm.Current[i] + (i < 5 ? "," : ""));
                    }
                    Console.WriteLine(">");
                }
                ct++;
            }
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < 6; i++)
            {
                Console.Write(seqDetails[i].ToString() + (i < 5 ? "," : ""));
            }
            Console.WriteLine(">]");

            seqDetails = new List<uint>() { 1, 2, 2, 0, 0 };
            newPerm = new Permutations(seqDetails, 5);
            for (ct = 0, newPerm.First(); !newPerm.AtEnd; newPerm.Next())
            {
                if (true || printDetail)
                {
                    Console.Write("<");
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write(newPerm.Current[i] + (i < 4 ? "," : ""));
                    }
                    Console.WriteLine(">");
                }
                ct++;
            }
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(seqDetails[i].ToString() + (i < 4 ? "," : ""));
            }
            Console.WriteLine(">]");

            MappedPermutations<string> newStringPerm = new MappedPermutations<string>(seqDetails, 5);
            newStringPerm.ElementMap = new List<string>() { "a", "b", "a", "d", "e", "f" };
            for (ct = 0, newStringPerm.First(); !newStringPerm.AtEnd; newStringPerm.Next())
            {
                if (true || printDetail)
                {
                    Console.Write("<");
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write(newStringPerm.Current[i] + (i < 4 ? "," : ""));
                    }
                    Console.WriteLine(">");
                }
                ct++;
            }
            Console.Write(ct.ToString() + " perm (with dup) )for [6,<");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(newStringPerm.ElementMap[i].ToString() + (i < 4 ? "," : ""));
            }
            Console.WriteLine(">]");

        }
    }
}
