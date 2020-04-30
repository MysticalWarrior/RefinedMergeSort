using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefinedMergeSort
{
    class Program
    {
        public static int comparisons;
        public static int merges;
        public static int arrayAccesssss;
        static void Main(string[] args)
        {
            Random random = new Random();
            int numberOfItems = 1000000;
            int[] data = new int[numberOfItems];
            for (int i = 0; i < numberOfItems; i++) { data[i] = random.Next(); }

            merges = 0; comparisons = 0;
            data = MergeSort(data);
            /*
            Console.WriteLine("The sorted numbers:");
            foreach (int i in data)
            {
                Console.WriteLine(i);
            }*/
            Console.WriteLine("The algorithim moved items {0} times for {1} items", merges , numberOfItems);
            Console.WriteLine("The algorithim compared items {0} times when sorting {1} items", comparisons, numberOfItems);

            System.IO.StreamWriter sw = new System.IO.StreamWriter("C:/Users/Aidan/Desktop/MergeSortData/Tracking.txt", true);
            sw.WriteLine("The algorithim moved items {0} times for {1} items", merges, numberOfItems);
            sw.WriteLine("\tThe algorithim compared items {0} times when sorting {1} items", comparisons, numberOfItems);
            sw.WriteLine("\tThe agorithim moved items in the array {0} times", arrayAccesssss);
            sw.Flush();
            sw.Close();

            System.IO.StreamWriter sw2 = new System.IO.StreamWriter("C:/Users/Aidan/Desktop/MergeSortData/Data.txt", true);
            sw2.WriteLine(numberOfItems + "," + merges + "," + comparisons + "," + arrayAccesssss);
            sw2.Flush();
            sw2.Close();

            //end program
            //Console.ReadLine();
        }

        public static int[] MergeSort(int[] array)
        {
            if (array.Length == 1) { return array; }

            int indexOfMiddle = array.Length / 2;

            ArraySegment<int> left = new ArraySegment<int>( array, 0, indexOfMiddle);
            ArraySegment<int> right = new ArraySegment<int>(array, indexOfMiddle, (array.Length - indexOfMiddle));

            return Merge(MergeSort(left.ToArray()), MergeSort(right.ToArray()));
        }

        public static int[] Merge(int[] left, int[] right)
        {
            int[] merged = new int[left.Length + right.Length];
            int MergeIndex = 0, LeftIndex = 0, RightIndex = 0;

            for (; MergeIndex < merged.Length; MergeIndex++ )
            {
                if (LeftIndex >= left.Length)
                {
                    merged[MergeIndex] = right[RightIndex];
                    RightIndex++;
                }
                else if (RightIndex >= right.Length)
                {
                    merged[MergeIndex] = left[LeftIndex];
                    LeftIndex++;
                } 
                else if (left[LeftIndex] < right[RightIndex])
                {
                    merged[MergeIndex] = left[LeftIndex];
                    LeftIndex++; comparisons++;
                }
                else if (right[RightIndex] < left[LeftIndex])
                {
                    merged[MergeIndex] = right[RightIndex];
                    RightIndex++; comparisons++;
                }
            }

            //updates the global data with this round's info.
            arrayAccesssss += MergeIndex;
            merges++;
            return merged;
        }
    }
}
