using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Models;

namespace Knapsack.Tests
{
    public class BruteForceAllPermutations : KnapSackTest
    {
        // Possible Solutions = N!  (** OR if number of slots is different than N == Pkn = N!/((N-K)!
        // For brute force we evaluate every possible permutation of N Items
        // 123 != 321 != 132 != 321 ETC (order matters)

        //NOTE: There are duplicate solutions evaluated due to the fact we are including constraints in the acceptable solution
        //IE in the scenario of 1234 vs 1342 for Items[ 10, 15, 1, 2 ] maxWeight = 20, 1234 is really combination 134 (13), 1342 is really combination 134 (13)
        //When solving a knapsack problem consider if the end combinations are acceptable (and order that the item entered the knapsack is irrelevant) then use 
        //combinations instead of permutations
        
        private void Swap(KSItem[] array, int index1, int index2)
        {
            KSItem temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void HandleSolution(KSItem[] items)
        {
            SolutionCount++;
            KnapsackSolution curSolution = new KnapsackSolution(TM);

            //in the permutation scenario the order the items are added to the knapsack are important (the constraints maybe violated)
            for (int s = 0; s < items.Length; s++)
                curSolution.TryAddItem(items[s]);
            
            if (OptimalSolution == null || curSolution.Result.Value > OptimalSolution.Result.Value)
                OptimalSolution = curSolution;

            return;
        }
        
        //The original source for this algorithm is: https://www.geeksforgeeks.org/write-a-c-program-to-print-all-permutations-of-a-given-string/
        private void FindPermutations(KSItem[] items, int left, int right)
        {
            if (left == right)
                HandleSolution(items);
            else
            {
                for(int i =left; i<=right; i++)
                {
                    Swap(items, left, i);
                    FindPermutations(items, left + 1, right);
                    Swap(items, left, i);
                }
            }
        }

        protected override void ProcessTest()
        {
            KSItem[] itemArray = TM.ItemList.ToArray();

            FindPermutations(itemArray, 0, itemArray.Length-1);
        }
    }

}

