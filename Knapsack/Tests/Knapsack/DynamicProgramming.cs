using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Models;

namespace Knapsack.Tests
{
    public class DynamicProgramming : KnapSackTest
    {
        /* 
         *    //Bottom up Dynamic Programming approach to Knapsack
              * Description / Algorithm from here:
              * https://github.com/ayzahmt/Knapsack-Problem
             Solution of knapsack problem using dynamic programming

             Purpose
             To get as much value into the knapsack as possible given the weight constraint of the knapsack.

             Solution Approach
             Evaluate the values of the items iteratively.
             For example, put the first item and select second item. Then evaluate first and second item. Make the most appropriate choice according to the value and weight of the items. And then evaluate the all item.
             In order to get rid of the recalculation, the calculation for the items are kept on a table at each step.
             After the items have been evaluated, the value of V[ItemCount,MaximumWeight] shows the maximum value we can get to the knapsack.
             Example
             Item Count = 4
             Max Weight = 5
             Item	1	2	3	4
             Value	100	20	60	40
             Weight	3	2	4	1
             Value Matrix

             V[i,w]	w=0	1	2	3	4	5
             i=0	0	0	0	0	0	0
             1	0	0	0	100	100	100
             2	0	0	20	100	100	120
             3	0	0	20	100	100	120
             4	0	40	40	100	140	140
             Maximum value we can put the knapsack is V[4,5] = 140
             
            // Processing requirements
             O(N*W) ** Requires int array of size [N,M] to store previous results
        */

        //private bool IncludeVolume { get; set; } = false;

        //private string FormatKey(int index, int weight, int volume)
        //{
        //    if(!IncludeVolume)
        //        return "K" + index + "_W" + weight;
        //    else
        //        return "K" + index + "_W" + weight + "_V"+volume;
        //}

        private void DetermineOptimalSolution(KSItem[] items)
        {
            int n = items.Length;
            bool includeVolume = (TM.MaxVolume != null);

            //Hard Constraints
            int maxWeight = TM.MaxWeight;
            int maxVolume = TM.MaxVolume.GetValueOrDefault(0);

            int curWeight, curVolume;

            //Value array is multidimensional (Number of Items, maxWeight, maxVolume)
            int[,,] valueArray = new int[n + 1, maxWeight + 1, maxVolume + 1];

            for (int i = 0; i <= n; i++)
            {
                for (curWeight = 0; curWeight <= maxWeight; curWeight++)
                {
                    for (curVolume = 0; curVolume <= maxVolume; curVolume++)
                    {
                        //Base case
                        if (i == 0 || curWeight == 0 || (includeVolume && curVolume == 0))
                            valueArray[i, curWeight, curVolume] = 0;

                        //If the item Can be added
                        else if (items[i - 1].Weight <= curWeight && (!includeVolume || (includeVolume && (items[i - 1].Volume <= curVolume))))
                        {
                            int nextItem = items[i - 1].Value;
                            int prevState = valueArray[i - 1, curWeight - items[i - 1].Weight, includeVolume ? curVolume - items[i - 1].Volume : 0];

                            //Value of adding the next item VS Value of NOT adding the item
                            valueArray[i, curWeight, curVolume] = Math.Max((nextItem + prevState), valueArray[i - 1, curWeight, curVolume]);
                        }
                        //If the next item cannot be added set it to the previous state
                        else
                            valueArray[i, curWeight, curVolume] = valueArray[i - 1, curWeight, curVolume];
                    }
                }
            }

            int maxValue = valueArray[n, maxWeight, maxVolume];
            //Next we need to "Trace Back" to get the actual solution
            //Start at the max K[n,W]
            int remWeight = maxWeight;
            int remVolume = maxVolume;

            for (int i = n; i > 0; i--)
            {
                //IF moving from i[index-1] -> i[index-2] is the same than
                // i[index-1] is NOT part of the solution
                // however if they are different than it is. 
                if (valueArray[i, remWeight, remVolume] != valueArray[(i - 1), remWeight, remVolume])
                {
                    KSItem choosenItem = items[i - 1];
                    OptimalSolution.TryAddItem(choosenItem);
                    remWeight -= choosenItem.Weight;
                    
                    if(includeVolume)
                        remVolume -= choosenItem.Volume;
                }
            }

            //If we are including Volume than use it else use 1
            SolutionCount = n * maxWeight * (includeVolume ? maxVolume : 1);
        }


        protected override void ProcessTest()
        {
            KSItem[] items = TM.ItemList.ToArray();

            DetermineOptimalSolution(items);
        }
    }
}




/*
 * 
 * 
 *         private void DetermineOptimalSolution(KSItem[] items)
        {
            int n = items.Length;
            IncludeVolume = (TM.MaxVolume != null);

            //Hard Constraints
            int maxWeight = TM.MaxWeight;
            int maxVolume = TM.MaxVolume.GetValueOrDefault(0);

            int curWeight, curVolume;
            //Dictionary allows a "virtual" multi dimensional int array at runtime
            //HOWEVER it incurs
            //Dictionary<string, int> K = new Dictionary<string, int>();
            int[,,] valueArray = new int[n, maxWeight, maxVolume];

            for (int i = 0; i <= n; i++)
            {
                for (curWeight = 0; curWeight <= maxWeight; curWeight++)
                {
                    for (curVolume = 0; curVolume <= maxVolume; curVolume++)
                    {

                        //Base case
                        if (i == 0 || curWeight == 0 || (IncludeVolume && curVolume == 0))
                            //K[FormatKey(i, curWeight, curVolume)] = 0;
                            valueArray[i, curWeight, curVolume] = 0;

                        //If the item Can be added
                        else if (items[i - 1].Weight <= curWeight && (!IncludeVolume || (IncludeVolume && (items[i - 1].Volume <= curVolume))))
                        {
                            int nextItem = items[i - 1].Value;
                            //int prevState = K[FormatKey(i - 1, curWeight - items[i - 1].Weight, curVolume - items[i - 1].Volume)];

                            //Value of adding the next item VS Value of NOT adding the item
                            K[FormatKey(i, curWeight, curVolume)] = Math.Max((nextItem + prevState), K[FormatKey(i - 1, curWeight, curVolume)]);
                        }
                        //If the next item cannot be added set it to the previous state
                        else
                            K[FormatKey(i, curWeight, curVolume)] = K[FormatKey(i - 1, curWeight, curVolume)];
                    }
                }
            }

            int maxValue = K[FormatKey(n, maxWeight, maxVolume)];
            //Next we need to "Trace Back" to get the actual solution
            //Start at the max K[n,W]
            int remWeight = maxWeight;
            int remVolume = maxVolume;

            for (int i = n; i > 0; i--)
            {
                //IF moving from i[index-1] -> i[index-2] is the same than
                // i[index-1] is NOT part of the solution
                // however if they are different than it is. 
                if (K[FormatKey(i, remWeight, remVolume)] != K[FormatKey((i - 1), remWeight, remVolume)])
                {
                    KSItem choosenItem = items[i - 1];
                    OptimalSolution.TryAddItem(choosenItem);
                    remWeight -= choosenItem.Weight;
                    remVolume -= choosenItem.Volume;
                }
            }

            //If we are including Volume than use it else use 1
            SolutionCount = n * maxWeight * (IncludeVolume ? maxVolume : 1);
        }

*/