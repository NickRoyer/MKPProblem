using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Models;

namespace Knapsack.Tests
{
    public class BruteForceAllCombinations : KnapSackTest
    {
        // For brute force we evaluate every possible combination of N Items (in ALL K Slots)

        // Possible Solutions = 2^N  (when including all possible values for K)
        // 1234 == 00,1,2,3,4,12,13,14,23,24,34,123,124,134,234
        // 24 / (24 * 
        // N! / (K! * (N -K)!)

        // 123 == 321 == 132 == 321 ETC (order does not matter)
        // 1234 (N=4, K=2) 12,13,14,23,24,34,
        // 4! / (2! * (4-2)!) == 24 / ( 2 * 2) == 24/4 == 6
        
        private void HandleSolution(KSItem[] solution)
        {
            SolutionCount++;
            KnapsackSolution curSolution = new KnapsackSolution(TM);

            for (int s = 0; s < solution.Length; s++)
                curSolution.TryAddItem(solution[s]);

            if (OptimalSolution == null || curSolution.Result.Value > OptimalSolution.Result.Value)
                OptimalSolution = curSolution;
        }

        private void FindAllCombinations(KSItem[] items)
        {
            // The base case is that all the items are used.
            HandleSolution(items);
            //Otherwise we have to find all the remaining possibilities
            //NOTE There is only ever one combination with items.length all other possible combinations are at most length-1
            FindCombinations(items, new KSItem[items.Length - 1]);
        }

        private void FindCombinations(KSItem[] items, KSItem[] tmp, int k=1, int itemIndex=0)
        {
            HandleSolution(tmp);

            if (k == items.Length)
                return;
                
            for (int i = itemIndex; i <items.Length; i++)
            {
                tmp[k-1] = items[i];
                FindCombinations(items, tmp, k + 1, i + 1);
                tmp[k-1] = null;
            }            
        }

        protected override void ProcessTest()
        {
            KSItem[] items = TM.ItemList.ToArray();
            
            FindAllCombinations(items);            
         }     
    }
}

