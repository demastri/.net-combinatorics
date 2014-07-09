using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPD.Primes
{
    public class Sieve
    {
        // we have 3 different implementations below
        // it's interesting to see the differences in performance between them.
        // the list and linked list implementations are accurate
        //  list moves remaining elements on evey remove(), but linkedList is slower... 
        //  (why? - memory alloc on each insert / free on each remove - the list only reallocs on insert log 2 n times...)
        // the marked list is so much faster - 1 alloc on insert, random access thereafter, that even wrapping it with a function to get a "final" list is much preferred
        public static List<int> Generate(int max)
        {
            List<int> outList = new List<int>(max);
            List<bool> ml = GenerateByMarkedList(max);
            for (int i = 2; i <= max; i++)
                if (!ml[i])
                    outList.Add(i);
            return outList;
        }

        private static List<int> GenerateByList(int max)
        {
            List<int> outList = new List<int>(max);

            for (int i = 2; i <= max; i++)
                outList.Add(i);
            for (int thisIndex = 0; thisIndex < outList.Count; thisIndex++)
                for (int val = 2 * outList[thisIndex]; val <= max; val += outList[thisIndex])
                    outList.Remove(val);
            return outList;
        }
        private static LinkedList<int> GenerateByLinkedList(int max)
        {
            LinkedList<int> outList = new LinkedList<int>();

            for (int i = 2; i <= max; i++)
                outList.AddLast(i);
            for (int thisIndex = 0; thisIndex < outList.Count; thisIndex++)
                for (int val = 2 * outList.ElementAt(thisIndex); val <= max; val += outList.ElementAt(thisIndex))
                    outList.Remove(val);
            return outList;
        }
        private static List<bool> GenerateByMarkedList(int max)
        {
            List<bool> outList = new List<bool>(max);

            for (int i = 0; i <= max; i++)
                outList.Add(false);
            for (int thisIndex = 2; thisIndex < outList.Count; thisIndex++)
                for (int val = 2 * thisIndex; val <= max; val += thisIndex) {
                    outList[val] = true;
                }
            return outList;
        }
        public static void Test() // change the access level as needed to show/hide this method..
        {
            PrintList(Generate(50));
            PrintList(GenerateByLinkedList(50));

            PrintList(Generate(100));
            PrintList(GenerateByLinkedList(100));

            PrintList(Generate(500));
            PrintList(GenerateByLinkedList(500));

            PrintList(Generate(20000));
            PrintList(GenerateByLinkedList(20000));

            PrintList(Generate(50000));
            PrintList(GenerateByList(50000));
            PrintList(GenerateByLinkedList(50000));

            PrintList(Generate(100000));

            PrintList(Generate(1000000));

            PrintList(Generate(2000000));
            
        }
        private static void PrintList<T>(IEnumerable<T> l)
        {
            Console.Write("<");
            string sep = "";
            for (int i = 0; i < l.Count(); i++)
            {
                Console.Write(sep + l.ElementAt(i).ToString());
                sep = ",";
            }
            Console.WriteLine(">");
        }
    }
}
