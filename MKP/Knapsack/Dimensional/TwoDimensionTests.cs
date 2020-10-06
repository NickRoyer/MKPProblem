using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Knapsack.Models;
using Knapsack.Tests;

namespace MKP_Test.Knapsack
{
    [Collection("Knapsack Test Collection")]
    public class TwoDimensionTests : KnapsackTests
    {
        private KnapsackTestData TestData { get; set; }

        public TwoDimensionTests()
        {
            TestData = new KnapsackTestData();
            TestData.LoadTestData("Knapsack/30ItemTestFile.xml");
        }

        [Theory]
        [InlineData(12)]
        [InlineData(5)]
        [InlineData(11)]
        [InlineData(1)]
        [InlineData(30)]
        [Trait("Category", "Init")]
        public void ValidateTestManagerVariableItemCount(int itemCnt)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, itemCnt, 1000);
            Assert.Equal(itemCnt, TM.ItemList.Count);
        }

        [Theory]
        [InlineData(35)]
        [InlineData(125)]
        [Trait("Category", "Init")]
        public void ValidateTestManagerVariableItemCountTooLarge(int itemCnt)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, itemCnt, 1000);
            Assert.Empty(TM.ItemList);
        }

        [Fact]
        [Trait("Category", "Init")]
        public void ValidateTestManagerItemAttributes()
        {
            //<Item ID="1" Value="20" Weight="22" Volume="13"></Item>
            KSItem item = CreateTestManager(TestData.KSItemList, 10, 100).ItemList.Find(i => i.Id == 1);
            Assert.Equal(20, item.Value);
            Assert.Equal(22, item.Weight);
            Assert.Equal(13, item.Volume);
        }

        [Fact]
        [Trait("Category", "Init")]
        public void ValidateSolutionCounts()
        {
            int MaxWeight = 425;
            int MaxVolume = 550;
            int N = 25;

            Assert.Equal(3628800, TestSolutions(TestType.BruteForcePermutations, 10, MaxWeight, MaxVolume));
            Assert.Equal(33554432, TestSolutions(TestType.BruteForceCombinations, N, MaxWeight, MaxVolume));
            Assert.Equal(5843750, TestSolutions(TestType.DynamicProgramming, N, MaxWeight, MaxVolume));
        }


        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Zero Weight")]
        public void TwoD_Zero_10Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 10, 0, 0);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(0, test.OptimalSolution.Result.Value);
            Assert.Equal(0, test.OptimalSolution.Result.Weight);
            Assert.Equal(0, test.OptimalSolution.Result.Volume);
            Assert.Empty(test.OptimalSolution.Solution);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Mid Weight")]
        public void TwoD_Mid_05_Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 5, 100, 80);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(90, test.OptimalSolution.Result.Value);
            Assert.Equal(67, test.OptimalSolution.Result.Weight);
            Assert.Equal(72, test.OptimalSolution.Result.Volume);
            Assert.Equal(3, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Mid Weight")]
        public void TwoD_Mid_10_Items(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 10, 100, 150);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = tm.RunTest(test);

            Assert.Equal(208, test.OptimalSolution.Result.Value);
            Assert.Equal(87, test.OptimalSolution.Result.Weight);
            Assert.Equal(135, test.OptimalSolution.Result.Volume);
            Assert.Equal(6, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, tm);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Large Weight")]
        public void TwoD_Large_10_Items(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 10, 500, 220);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = tm.RunTest(test);

            Assert.Equal(290, test.OptimalSolution.Result.Value);
            Assert.Equal(156, test.OptimalSolution.Result.Weight);
            Assert.Equal(209, test.OptimalSolution.Result.Volume);
            Assert.Equal(9, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, tm);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        //[InlineData(TestType.BruteForcePermutations)] //Takes too long
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Mid Weight")]
        [Trait("Category", "11 Items")]
        public void TwoD_Mid_11_Items(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 11, 100, 150);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = tm.RunTest(test);

            Assert.Equal(208, test.OptimalSolution.Result.Value);
            Assert.Equal(87, test.OptimalSolution.Result.Weight);
            Assert.Equal(135, test.OptimalSolution.Result.Volume);
            Assert.Equal(6, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, tm);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        ////[InlineData(TestType.BruteForcePermutations)] //Takes too long
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "20 Items")]
        public void TwoD_Large_20_Items(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 20, 1000, 440);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = tm.RunTest(test);

            Assert.Equal(554, test.OptimalSolution.Result.Value);
            Assert.Equal(426, test.OptimalSolution.Result.Weight);
            Assert.Equal(437, test.OptimalSolution.Result.Volume);
            Assert.Equal(18, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, tm);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        //[InlineData(TestType.BruteForceCombinations)] //Takes too long (1 min)
        //[InlineData(TestType.BruteForcePermutations)] //Takes WAY too long
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Mid Weight")]
        [Trait("Category", "25 Items")]
        public void TwoD_Mid_25_Items(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 25, 350, 375);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = tm.RunTest(test);

            Assert.Equal(577, test.OptimalSolution.Result.Value);
            Assert.Equal(343, test.OptimalSolution.Result.Weight);
            Assert.Equal(366, test.OptimalSolution.Result.Volume);
            Assert.Equal(16, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, tm);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        //[InlineData(TestType.BruteForceCombinations)] //Takes too long (1 min)
        //[InlineData(TestType.BruteForcePermutations)] //Takes WAY too long
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Mid Weight")]
        [Trait("Category", "30 Items")]
        public void TwoD_Mid_30_Items(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 30, 425, 550);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = tm.RunTest(test);

            Assert.Equal(728, test.OptimalSolution.Result.Value);
            Assert.Equal(425, test.OptimalSolution.Result.Weight);
            Assert.Equal(481, test.OptimalSolution.Result.Volume);
            Assert.Equal(22, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, tm);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        //[InlineData(TestType.BruteForceCombinations)] //Takes too long (1 min)
        [InlineData(TestType.DynamicProgramming)]
        [Trait("Category", "Mid Weight")]
        [Trait("Category", "25 Items")]
        public void TwoD_Mid_2_25_Items(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 25, 425, 550);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = tm.RunTest(test);

            Assert.Equal(664, test.OptimalSolution.Result.Value);
            Assert.Equal(422, test.OptimalSolution.Result.Weight);
            Assert.Equal(517, test.OptimalSolution.Result.Volume);
            Assert.Equal(21, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, tm);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }
    }
}
