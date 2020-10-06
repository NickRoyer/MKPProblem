using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Models;
using Knapsack.Tests;

namespace MKP_Test
{
    public class KnapsackTests
    {
        public enum TestType { BruteForcePermutations, BruteForceCombinations, DynamicProgramming};
        public MKPTest CreateTest(TestType type)
        {
            switch (type)
            {
                case TestType.BruteForcePermutations:
                    return new BruteForceAllPermutations();
                case TestType.BruteForceCombinations:
                    return new BruteForceAllCombinations();
                case TestType.DynamicProgramming:
                    return new DynamicProgramming();
            }

            throw new Exception("Invalid Test Type");
        }

        public KnapsackTestManager CreateTestManager(List<KSItem> ksItemlist, int cnt, int maxWeight, int? maxVolume = null)
        {
            List<KSItem> tmItemList;
            if (ksItemlist == null || ksItemlist.Count < cnt)
                tmItemList = new List<KSItem>();
            else
                tmItemList = ksItemlist.GetRange(0, cnt);

            return new KnapsackTestManager()
            {
                ItemList = tmItemList,
                MaxWeight = maxWeight,
                MaxVolume = maxVolume
            };
        }

        //Surpising that this isn't built in
        public long CalculateFactorial(int N)
        {
            long returnVal = 1;
            for (int i = 1; i <= N; i++)
                returnVal *= i;

            return returnVal;
        }

        public long TestSolutions(TestType type, KnapsackTestManager tm)
        {
            return TestSolutions(type, tm.ItemList.Count, tm.MaxWeight, tm.MaxVolume);
        }

        public long TestSolutions(TestType type, int n, int maxWeight, int? maxVolume)
        {
            switch (type)
            {
                case TestType.BruteForceCombinations:
                    return (long)Math.Pow(2, n);
                case TestType.BruteForcePermutations:
                    return CalculateFactorial(n);
                case TestType.DynamicProgramming:
                    return n * maxWeight * ((maxVolume == null) ? 1 : (int)maxVolume);
            }

            return 0;
        }
    }
}
